using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRacingCarMove : MonoBehaviour
{
    public Transform _target;
    public Transform _p1, _p2, _p3;
    public float _gizmoDetail = 10;
    public float _speed = 0.2f;

    private List<Vector3> _gizmoPoints = new List<Vector3>();
    private float t = 0f;

    private void OnDrawGizmos()
    {
        _gizmoPoints.Clear();

        if (_p1 != null && _p2 != null && _p3 != null && _gizmoDetail > 0)
        {
            for (int i = 0; i <= _gizmoDetail; i++)
            {
                float t = (float)i / _gizmoDetail;
                Vector3 p4 = Vector3.Lerp(_p1.position, _p2.position, t);
                Vector3 p5 = Vector3.Lerp(_p2.position, _p3.position, t);
                _gizmoPoints.Add(Vector3.Lerp(p4, p5, t));
            }
        }

        for (int i = 0; i < _gizmoPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(_gizmoPoints[i], _gizmoPoints[i + 1]);
        }
    }

    private void Update()
    {
        if (_p1 != null && _p2 != null && _p3 != null)
        {
            t += Time.deltaTime * _speed;
            if (t > 1f) t = 0f;

            Vector3 p4 = Vector3.Lerp(_p1.position, _p2.position, t);
            Vector3 p5 = Vector3.Lerp(_p2.position, _p3.position, t);
            _target.position = Vector3.Lerp(p4, p5, t);
        }
    }
}
