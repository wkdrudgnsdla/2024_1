using UnityEngine;
using System.Collections.Generic;

public class SplineEnemyMover : MonoBehaviour
{
    public GameManager GM;

    // 스플라인 경로를 구성할 컨트롤 포인트 (최소 2개 이상; Catmull-Rom 보간은 4개 이상 권장)
    public List<Transform> controlPoints;
    // 오브젝트의 실제 이동 속도 (유니티 거리/초)
    public float speed = 5f;
    // 한 세그먼트 당 arc length 테이블을 만들 때 샘플링할 구간 개수 (정밀도와 성능의 trade-off)
    public int samplesPerSegment = 50;

    // 현재 세그먼트 인덱스 (유효 범위: 0 ~ controlPoints.Count - 2)
    private int segmentIndex = 0;
    // 현재 세그먼트에서 이동한 누적 거리
    private float segmentDistanceTraveled = 0f;
    // 각 세그먼트별 arc length 데이터를 저장 (세그먼트 수 = controlPoints.Count - 1)
    private List<SegmentArcData> arcLengthTables = new List<SegmentArcData>();

    // 오브젝트가 가진 원래 회전 상태와 곡선 회전 간의 오프셋을 저장
    private Quaternion initialRotationOffset;
    private bool initialOffsetSet = false;

    // arc length 테이블용 데이터 구조
    class SegmentArcData
    {
        public float[] tSamples;   // 각 샘플의 t 값
        public float[] arcLengths; // 해당 t까지의 누적 거리
        public float totalLength;  // 세그먼트 전체 길이
    }

    private void Awake()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        if (controlPoints == null || controlPoints.Count < 2)
            return;

        // 각 세그먼트(컨트롤 포인트 0 ~ controlPoints.Count - 2)에 대해 arc length 테이블 계산
        arcLengthTables.Clear();
        int numSegments = controlPoints.Count - 1;
        for (int i = 0; i < numSegments; i++)
        {
            SegmentArcData data = ComputeArcLengthDataForSegment(i, samplesPerSegment);
            arcLengthTables.Add(data);
        }

        // 시작 시점에서 곡선의 초기 접선 방향(수평)을 구한 후, 오브젝트의 현재 회전과의 차이를 저장
        Vector3 p0 = GetControlPoint(0 - 1);
        Vector3 p1 = GetControlPoint(0);
        Vector3 p2 = GetControlPoint(1);
        Vector3 p3 = GetControlPoint(2);
        Vector3 tangent = CatmullRomTangent(p0, p1, p2, p3, 0f);
        // 수평 방향만 사용 (Y축은 고정)
        Vector3 tangentHorizontal = new Vector3(tangent.x, 0, tangent.z);
        if (tangentHorizontal == Vector3.zero)
            tangentHorizontal = Vector3.forward;
        Quaternion curveRotation = Quaternion.LookRotation(tangentHorizontal, Vector3.up);
        initialRotationOffset = Quaternion.Inverse(curveRotation) * transform.rotation;
        initialOffsetSet = true;
    }

    void Update()
    {
        if(GM.Finish)
        {
            return;
        }

        // 게임 매니저의 StartCountdown이 1보다 클 경우 (아직 카운트다운 중이면)
        // 적이 움직이지 않고 내부 이동 상태를 초기화하여 게임 재시작 시 처음부터 진행하도록 함.
        if (GM.StartCountdown > 1)
        {
            segmentIndex = 0;
            segmentDistanceTraveled = 0f;
            return;
        }

        if (controlPoints == null || controlPoints.Count < 2 || arcLengthTables.Count == 0)
            return;
        if (segmentIndex >= arcLengthTables.Count)
            return; // 모든 세그먼트를 완료한 경우

        // 현재 세그먼트의 arc length 데이터 가져오기
        SegmentArcData currentData = arcLengthTables[segmentIndex];

        // 실제 이동 거리 업데이트 (일정한 속도로 진행)
        segmentDistanceTraveled += speed * Time.deltaTime;

        // 현재 세그먼트의 총 길이를 초과하면 다음 세그먼트로 전환
        while (segmentDistanceTraveled > currentData.totalLength && segmentIndex < arcLengthTables.Count - 1)
        {
            segmentDistanceTraveled -= currentData.totalLength;
            segmentIndex++;
            currentData = arcLengthTables[segmentIndex];
        }

        // 현재 세그먼트에서 누적 거리(distance)에 따른 t 값을 arc length 테이블에서 보간하여 구함
        float t = GetTForDistance(currentData, segmentDistanceTraveled);

        // 현재 세그먼트에 해당하는 4개의 컨트롤 포인트 (경계는 클램핑 처리)
        Vector3 p0 = GetControlPoint(segmentIndex - 1);
        Vector3 p1 = GetControlPoint(segmentIndex);
        Vector3 p2 = GetControlPoint(segmentIndex + 1);
        Vector3 p3 = GetControlPoint(segmentIndex + 2);

        // Catmull-Rom 보간으로 새로운 위치 계산
        Vector3 newPosition = CatmullRom(p0, p1, p2, p3, t);
        transform.position = newPosition;

        // 곡선의 접선 벡터를 이용해 회전 계산 (수평 방향만 적용)
        Vector3 rawTangent = CatmullRomTangent(p0, p1, p2, p3, t).normalized;
        Vector3 tangentHorizontal = new Vector3(rawTangent.x, 0, rawTangent.z);
        if (tangentHorizontal == Vector3.zero)
            tangentHorizontal = Vector3.forward;
        Quaternion curveRotation = Quaternion.LookRotation(tangentHorizontal, Vector3.up);
        // 기존 오브젝트의 회전 오프셋을 유지하면서 곡선 회전을 적용
        transform.rotation = curveRotation * initialRotationOffset;
    }

    // 인덱스가 범위를 벗어나면 첫 번째 혹은 마지막 포인트 반환 (열린 경로)
    Vector3 GetControlPoint(int index)
    {
        if (index < 0)
            return controlPoints[0].position;
        if (index >= controlPoints.Count)
            return controlPoints[controlPoints.Count - 1].position;
        return controlPoints[index].position;
    }

    // Catmull-Rom 보간 함수
    Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;
        return 0.5f * ((2f * p1) +
                       (-p0 + p2) * t +
                       (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
                       (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }

    // Catmull-Rom 보간 곡선의 미분(접선) 함수
    Vector3 CatmullRomTangent(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;
        return 0.5f * ((-p0 + p2) +
                       2f * (2f * p0 - 5f * p1 + 4f * p2 - p3) * t +
                       3f * (-p0 + 3f * p1 - 3f * p2 + p3) * t2);
    }

    // 현재 세그먼트의 arc length 데이터를 계산 (샘플링을 통해 근사)
    SegmentArcData ComputeArcLengthDataForSegment(int segIndex, int sampleCount)
    {
        SegmentArcData data = new SegmentArcData();
        data.tSamples = new float[sampleCount + 1];
        data.arcLengths = new float[sampleCount + 1];

        // 해당 세그먼트의 4개 컨트롤 포인트 (경계는 클램핑)
        Vector3 p0 = GetControlPoint(segIndex - 1);
        Vector3 p1 = GetControlPoint(segIndex);
        Vector3 p2 = GetControlPoint(segIndex + 1);
        Vector3 p3 = GetControlPoint(segIndex + 2);

        data.tSamples[0] = 0f;
        data.arcLengths[0] = 0f;
        Vector3 prevPoint = CatmullRom(p0, p1, p2, p3, 0f);
        float cumulativeLength = 0f;
        for (int i = 1; i <= sampleCount; i++)
        {
            float t = i / (float)sampleCount;
            data.tSamples[i] = t;
            Vector3 point = CatmullRom(p0, p1, p2, p3, t);
            cumulativeLength += Vector3.Distance(prevPoint, point);
            data.arcLengths[i] = cumulativeLength;
            prevPoint = point;
        }
        data.totalLength = cumulativeLength;
        return data;
    }

    // 주어진 누적 거리(distance)에 해당하는 t 값을 arc length 테이블에서 선형 보간으로 찾음
    float GetTForDistance(SegmentArcData data, float distance)
    {
        if (distance <= 0f)
            return 0f;
        if (distance >= data.totalLength)
            return 1f;

        for (int i = 1; i < data.arcLengths.Length; i++)
        {
            if (data.arcLengths[i] >= distance)
            {
                float t0 = data.tSamples[i - 1];
                float t1 = data.tSamples[i];
                float d0 = data.arcLengths[i - 1];
                float d1 = data.arcLengths[i];
                float factor = (distance - d0) / (d1 - d0);
                return Mathf.Lerp(t0, t1, factor);
            }
        }
        return 1f;
    }

    // 에디터에서 경로를 미리 볼 수 있도록 기즈모로 빨간색 선을 그림 (t 값을 균등 분할)
    void OnDrawGizmos()
    {
        if (controlPoints == null || controlPoints.Count < 2)
            return;
        Gizmos.color = Color.red;
        int segmentsPerCurve = 20;
        int numSegments = controlPoints.Count - 1;
        for (int i = 0; i < numSegments; i++)
        {
            Vector3 p0 = GetControlPoint(i - 1);
            Vector3 p1 = GetControlPoint(i);
            Vector3 p2 = GetControlPoint(i + 1);
            Vector3 p3 = GetControlPoint(i + 2);
            Vector3 prevPoint = CatmullRom(p0, p1, p2, p3, 0f);
            for (int j = 1; j <= segmentsPerCurve; j++)
            {
                float t = j / (float)segmentsPerCurve;
                Vector3 point = CatmullRom(p0, p1, p2, p3, t);
                Gizmos.DrawLine(prevPoint, point);
                prevPoint = point;
            }
        }
    }
}
