using UnityEngine;
using UnityEngine.Splines;
using System;

public class HerdGate : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private BezierKnot meetingKnot;
    [SerializeField] private BezierKnot[] openKnots;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private GameObject herdAssist;

    private Timer gateTimer;
    private BezierKnot start0, start1;   // starting knots for animation
    private BezierKnot target0, target1; // target knots for animation
    private bool isAnimating;
    [SerializeField] private SplineAreaGenerator splineAreaGenerator;

    private void Awake() {
        splineAreaGenerator = GetComponentInChildren<SplineAreaGenerator>();
        splineContainer = GetComponent<SplineContainer>();
    }

    private void Start() {
        openKnots = new BezierKnot[2];
        openKnots[0] = splineContainer.Spline[0];
        openKnots[1] = splineContainer.Spline[^1];
        CalculateMeetingKnot();
    }

    private void Update() {
        if (isAnimating) {
            gateTimer.Update();

            float t = gateTimer.Progress;
            BezierKnot lerped0 = LerpKnot(start0, target0, t);
            BezierKnot lerped1 = LerpKnot(start1, target1, t);

            splineContainer.Spline[0] = lerped0;
            splineContainer.Spline[^1] = lerped1;

            if (gateTimer.IsFinished) {
                // snap to target when done
                splineContainer.Spline[0] = target0;
                splineContainer.Spline[^1] = target1;
                splineAreaGenerator.CopyScaledSpline();
                isAnimating = false;
            }
        }
    }

    private void CalculateMeetingKnot() {
        meetingKnot.Position = (splineContainer.Spline[0].Position + splineContainer.Spline[^1].Position) / 2f;
        meetingKnot.TangentIn = (splineContainer.Spline[0].TangentIn + splineContainer.Spline[^1].TangentIn) / 2f;
        meetingKnot.TangentOut = (splineContainer.Spline[0].TangentOut + splineContainer.Spline[^1].TangentOut) / 2f;
        meetingKnot.Rotation = Quaternion.Slerp(
            splineContainer.Spline[0].Rotation,
            splineContainer.Spline[^1].Rotation,
            0.5f
        );
        
        herdAssist.transform.localPosition = meetingKnot.Position;
    }

    [ContextMenu("OpenGate")]
    private void OpenGate() {
        StartGateAnimation(openKnots[0], openKnots[1]);
    }

    [ContextMenu("CloseGate")]
    private void CloseGate() {
        StartGateAnimation(meetingKnot, meetingKnot);
    }

    private void StartGateAnimation(BezierKnot newTarget0, BezierKnot newTarget1) {
        start0 = splineContainer.Spline[0];
        start1 = splineContainer.Spline[^1];
        target0 = newTarget0;
        target1 = newTarget1;

        gateTimer.SetMaxTime(animationDuration);
        isAnimating = true;
    }

    private BezierKnot LerpKnot(BezierKnot a, BezierKnot b, float t) {
        BezierKnot result = new BezierKnot {
            Position = Vector3.Lerp(a.Position, b.Position, t),
            TangentIn = Vector3.Lerp(a.TangentIn, b.TangentIn, t),
            TangentOut = Vector3.Lerp(a.TangentOut, b.TangentOut, t),
            Rotation = Quaternion.Slerp(a.Rotation, b.Rotation, t)
        };
        return result;
    }
}
