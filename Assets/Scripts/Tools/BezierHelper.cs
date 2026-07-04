using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezierHelper
{
    /// 二次贝塞尔点位
    public static Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        float u = 1 - t;
        float u2 = u * u;
        float t2 = t * t;
        return u2 * p0 + 2 * u * t * p1 + t2 * p2;
    }

    /// 批量生成曲线采样点（轨迹预览）
    public static Vector2[] GetBezierPoints(Vector2 start, Vector2 control, Vector2 end, int sampleCount)
    {
        Vector2[] points = new Vector2[sampleCount];
        for (int i = 0; i < sampleCount; i++)
        {
            float t = (float)i / (sampleCount - 1);
            points[i] = QuadraticBezier(start, control, end, t);
        }
        return points;
    }

    /// 获取贝塞尔起点切线方向（用于投掷力）
    public static Vector2 GetStartTangent(Vector2 p0, Vector2 p1)
    {
        return (p1 - p0).normalized;
    }
}