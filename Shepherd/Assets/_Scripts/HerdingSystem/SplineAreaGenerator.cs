using System;
using UnityEngine;
using UnityEngine.Splines;

public class SplineAreaGenerator : MonoBehaviour
{
    [SerializeField] private SplineContainer perimeterSpline;
    [SerializeField] private SplineContainer areaSpline;

    [SerializeField] private Collider col;

    [SerializeField, Range(0f, 1f)] private float scale = 0.9f;

    private void Awake() {
        perimeterSpline = transform.parent.GetComponent<SplineContainer>();
        areaSpline = GetComponent<SplineContainer>();
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnValidate() {
        if (perimeterSpline != null && areaSpline != null)
            CopyScaledSpline(perimeterSpline.Spline, areaSpline.Spline, scale);
    }

    private void CopyScaledSpline(Spline source, Spline target, float scale) {
        target.Clear();

        foreach (BezierKnot knot in source) {
            Vector3 scaledPos = knot.Position * scale;
            Vector3 scaledTangentIn = knot.TangentIn * scale;
            Vector3 scaledTangentOut = knot.TangentOut * scale;

            BezierKnot newKnot = new BezierKnot(scaledPos, scaledTangentIn, scaledTangentOut, knot.Rotation);

            target.Add(newKnot);
        }

        target.Closed = source.Closed;
    }
    
    public void CopyScaledSpline() {
        areaSpline.Spline.Clear();

        foreach (BezierKnot knot in perimeterSpline.Spline) {
            Vector3 scaledPos = knot.Position * scale;
            Vector3 scaledTangentIn = knot.TangentIn * scale;
            Vector3 scaledTangentOut = knot.TangentOut * scale;

            BezierKnot newKnot = new BezierKnot(scaledPos, scaledTangentIn, scaledTangentOut, knot.Rotation);

            areaSpline.Spline.Add(newKnot);
        }

        areaSpline.Spline.Closed = perimeterSpline.Spline.Closed;
    }
}