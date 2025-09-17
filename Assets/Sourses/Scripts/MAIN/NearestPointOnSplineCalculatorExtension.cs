using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public static class NearestPointOnSplineCalculatorExtension
{
    public static float GetNearestPointOnPercent(SplineContainer splineContainer, Transform point)
    {
        if (splineContainer == null || point == null)
            return default;

        ISpline spline = splineContainer.Spline;

        SplineUtility.GetNearestPoint(spline, (float3)point.transform.position, out float3 currentPoint, out float currentPercent);

        return currentPercent;
    }
}
