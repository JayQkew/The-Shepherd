using System;
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
        openKnots[0] = splineContainer.Spline[0];
        openKnots[1] = splineContainer.Spline[^1];
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
