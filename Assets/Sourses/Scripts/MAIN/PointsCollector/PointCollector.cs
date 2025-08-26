using System.Collections.Generic;
using UnityEngine;

public class PointCollector : MonoBehaviour
{
    [SerializeField] private List<Transform> _targetPoints = new List<Transform>();

    private int _maxColumnCount = GameStaticData.MaxColumnCount;

    public IReadOnlyList<Transform> TargetPoints => _targetPoints;

    public IReadOnlyList<Transform> GetPoints()
    {
        return TargetPoints;
    }

    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;

        if (pointCount > _maxColumnCount)
        {
            throw new System.Exception(" оличество точек превышает максимально допустимое количество");
        }

        if (_targetPoints.Count != 0)
        {
            _targetPoints.Clear();
        }

        if (pointCount == 0)
        {
            throw new System.Exception("ќтсутствуют точки");
        }

        for (int i = 0; i < pointCount; i++)
        {
            Transform point = transform.GetChild(i);
            _targetPoints.Add(point);
        }
    }
}
