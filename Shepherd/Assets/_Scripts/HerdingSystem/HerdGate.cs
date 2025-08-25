using UnityEngine;
using UnityEngine.Splines;

public class HerdGate : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private BezierKnot meetingKnot;
    [SerializeField] private BezierKnot[] openKnots;

    private void Awake() {
        splineContainer = GetComponent<SplineContainer>();
    }

    private void Start() {
        openKnots = new BezierKnot[2];
        openKnots[0] = splineContainer.Spline[0];
        openKnots[1] = splineContainer.Spline[^1];
        CalculateMeetingKnot();
    }

    private void OnValidate() {
        if (splineContainer != null && splineContainer.Spline.Count > 1) {
            CalculateMeetingKnot();
        }
    }

    private void CalculateMeetingKnot() {
        meetingKnot.Position = (splineContainer.Spline[0].Position + splineContainer.Spline[^1].Position) / 2f;
        meetingKnot.TangentIn = (splineContainer.Spline[0].TangentIn + splineContainer.Spline[^1].TangentIn) / 2f;
        meetingKnot.TangentOut = (splineContainer.Spline[0].TangentOut + splineContainer.Spline[^1].TangentOut) / 2f;
        meetingKnot.Rotation = Quaternion.Slerp(splineContainer.Spline[0].Rotation, splineContainer.Spline[^1].Rotation, 0.5f);
    }

    [ContextMenu("OpenGate")]
    private void OpenGate() {
        splineContainer.Spline[0] = openKnots[0];
        splineContainer.Spline[^1] = openKnots[1];
    }

    [ContextMenu("CloseGate")]
    private void CloseGate() {
        splineContainer.Spline[0] = meetingKnot;
        splineContainer.Spline[^1] = meetingKnot;
    }
}