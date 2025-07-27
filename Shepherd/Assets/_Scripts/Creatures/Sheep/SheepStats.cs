using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SheepStats", menuName = "Creatures/SheepStats")]
public class SheepStats : ScriptableObject
{
    public MinMax idleTime;
    public MinMax chillTime;
    public MinMax sleepTime;
    [Space(10)]
    public float runTime;
    public float walkRadius;
}
