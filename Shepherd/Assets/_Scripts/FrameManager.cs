using System;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    private void Awake() {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;     // Disable VSync so FPS cap works
        Application.targetFrameRate = 60;   // Cap Editor FPS to 60
#else
        QualitySettings.vSyncCount = 1;     // Use VSync in builds
#endif
    }
}
