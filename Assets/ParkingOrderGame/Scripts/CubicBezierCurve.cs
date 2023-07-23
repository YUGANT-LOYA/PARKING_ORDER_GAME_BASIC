using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace YugantLibrary.ParkingOrderGame
{

    [RequireComponent(typeof(LineRenderer))]
    public class CubicBezierCurve : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        [SerializeField] int SEGMENT_COUNT = 25;
        public Transform[] controlPoints;
        private int curveCount = 0;
        private int layerOrder = 0;
        Vector3[] lineRendererPoints;

        void Start()
        {
            if (GameController.Instance.isLevelDesigningInProgress)
            {
                if (!lineRenderer)
                {
                    lineRenderer = GetComponent<LineRenderer>();
                }
                lineRenderer.sortingLayerID = layerOrder;
                curveCount = (int)controlPoints.Length / 3;
            }
        }

        void Update()
        {
            if (GameController.Instance.isLevelDesigningInProgress)
            {
                DrawCurve();
            }
        }

        void DrawCurve()
        {
            for (int j = 0; j < curveCount; j++)
            {
                for (int i = 1; i <= SEGMENT_COUNT; i++)
                {
                    float t = i / (float)SEGMENT_COUNT;
                    int nodeIndex = j * 3;
                    Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].position, controlPoints[nodeIndex + 1].position, controlPoints[nodeIndex + 2].position, controlPoints[nodeIndex + 3].position);
                    lineRenderer.positionCount = ((j * SEGMENT_COUNT) + i);
                    lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);
                }

            }
        }

        Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
        }
    }
}
