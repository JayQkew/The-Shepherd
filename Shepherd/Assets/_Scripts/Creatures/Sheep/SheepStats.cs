using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SheepStats", menuName = "Creatures/SheepStats")]
public class SheepStats : ScriptableObject
{
    public MinMax idleTime;
    public MinMax sleepTime;
    [Space(10)]
    public MinMax runTime;
    public float walkRadius;
    public MinMax walkSpeed;
}
