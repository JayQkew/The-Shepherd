using System;
using UnityEngine;

[Serializable]
public struct HerdTarget
{
    public HerdAreaName destination;
    public HerdAnimalName animal;
    public int target;
    public int curr;
    public bool TargetMet => curr >= target;
}
