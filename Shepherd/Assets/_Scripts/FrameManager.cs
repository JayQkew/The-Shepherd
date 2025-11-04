using System;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public int targetFrameRate = 60;
    private void Awake() {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;     // Disable VSync so FPS cap works
        Application.targetFrameRate = targetFrameRate;   // Cap Editor FPS to 60
#else
        QualitySettings.vSyncCount = 1;     // Use VSync in builds
#endif
    }
}
