using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SheepStats", menuName = "Creatures/SheepStats")]
public class SheepStats : ScriptableObject
{
    public MinMax idleTime;
    public MinMax eatTime;
    public MinMax sleepTime;
    public MinMax suppAnimTimer;
    [Space(10)]
    public MinMax runTime;
    public MinMax walkTime;
    public MinMax walkSpeed;
}
