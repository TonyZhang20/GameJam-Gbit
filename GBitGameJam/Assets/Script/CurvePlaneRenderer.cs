using UnityEngine;

public class CurvePlaneRenderer : MonoBehaviour
{
    public Transform startPoint;  // 曲线起点
    public Transform endPoint;    // 曲线终点
    public float curveHeight;     // 曲线高度
    public int numPoints;         // 曲线上的点数量

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;

        // 计算曲线上的点位置
        Vector3[] points = CalculateCurvePoints();

        // 设置线段的位置
        lineRenderer.SetPositions(points);
    }

    private Vector3[] CalculateCurvePoints()
    {
        Vector3[] points = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            float x = Mathf.Lerp(startPoint.position.x, endPoint.position.x, t);
            float y = Mathf.Lerp(startPoint.position.y, endPoint.position.y, t) + Mathf.Sin(t * Mathf.PI) * curveHeight;
            float z = startPoint.position.z;
            points[i] = new Vector3(x, y, z);
        }

        return points;
    }
}