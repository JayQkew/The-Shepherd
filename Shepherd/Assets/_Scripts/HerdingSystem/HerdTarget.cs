using System;
using UnityEngine;

[Serializable]
public struct HerdTarget
{
    public HerdAreaName destination;
    public AnimalName animal;
    public int target;
    public int curr;
    public bool TargetMet => curr >= target;
}
