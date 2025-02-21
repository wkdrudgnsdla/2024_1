using UnityEngine;

public class PaintDetailChecker : MonoBehaviour
{
    [Header("플레이어가 페인트 디테일(풀 등) 위에 있으면 true")]
    public bool isOnPaintDetail = false;

    [Header("현재 플레이어 아래에 있는 Terrain")]
    public Terrain terrainUnderneath;

    void Update()
    {
        // 플레이어 위치 아래의 Terrain을 가져옵니다.
        terrainUnderneath = GetTerrainUnderneath(transform.position);
        if (terrainUnderneath != null)
        {
            // 플레이어 위치에 Detail(풀 등)이 하나라도 있으면 true
            isOnPaintDetail = CheckPaintDetails(transform.position, terrainUnderneath);
        }
        else
        {
            isOnPaintDetail = false;
        }
    }

    /// <summary>
    /// 주어진 월드 좌표를 포함하는 Terrain을 반환합니다.
    /// </summary>
    /// <param name="worldPos">월드 좌표</param>
    /// <returns>해당 좌표를 포함하는 Terrain, 없으면 null</returns>
    Terrain GetTerrainUnderneath(Vector3 worldPos)
    {
        Terrain[] terrains = Terrain.activeTerrains;
        foreach (Terrain terrain in terrains)
        {
            Vector3 terrainPos = terrain.transform.position;
            Vector3 terrainSize = terrain.terrainData.size;
            if (worldPos.x >= terrainPos.x && worldPos.x <= terrainPos.x + terrainSize.x &&
                worldPos.z >= terrainPos.z && worldPos.z <= terrainPos.z + terrainSize.z)
            {
                return terrain;
            }
        }
        return null;
    }

    /// <summary>
    /// 주어진 월드 좌표가 속한 Terrain 영역에서 Detail(풀 등)이 존재하면 true를 반환합니다.
    /// </summary>
    /// <param name="worldPos">월드 좌표</param>
    /// <param name="terrain">체크할 Terrain</param>
    /// <returns>해당 위치에 Detail이 있으면 true, 없으면 false</returns>
    bool CheckPaintDetails(Vector3 worldPos, Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;
        // 월드 좌표를 Terrain 로컬 좌표로 변환
        Vector3 terrainLocalPos = worldPos - terrain.transform.position;
        float normalizedX = Mathf.InverseLerp(0, terrainData.size.x, terrainLocalPos.x);
        float normalizedZ = Mathf.InverseLerp(0, terrainData.size.z, terrainLocalPos.z);

        // Detail map의 해상도에 맞게 좌표 변환 (인덱스 범위: 0 ~ detailWidth-1, 0 ~ detailHeight-1)
        int detailX = Mathf.Clamp(Mathf.FloorToInt(normalizedX * terrainData.detailWidth), 0, terrainData.detailWidth - 1);
        int detailY = Mathf.Clamp(Mathf.FloorToInt(normalizedZ * terrainData.detailHeight), 0, terrainData.detailHeight - 1);

        // Terrain에 설정된 모든 Detail 레이어를 순회합니다.
        int detailLayerCount = terrainData.detailPrototypes.Length;
        for (int i = 0; i < detailLayerCount; i++)
        {
            int[,] detailLayer = terrainData.GetDetailLayer(detailX, detailY, 1, 1, i);
            // 해당 Detail 레이어에서 값이 0보다 크면 Detail이 존재한다는 의미입니다.
            if (detailLayer != null && detailLayer[0, 0] > 0)
            {
                return true;
            }
        }
        return false;
    }
}
