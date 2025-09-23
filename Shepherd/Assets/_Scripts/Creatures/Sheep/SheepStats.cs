using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

[CreateAssetMenu(fileName = "SheepStats", menuName = "Creatures/SheepStats")]
public class SheepStats : ScriptableObject
{
    public MinMax idleTime;
    public MinMax eatTime;
    public MinMax sleepTime;
    public MinMax suppAnimTimer;
    
    [Space(20)]
    public MinMax runTime;
    public MinMax walkTime;
    public MinMax walkSpeed;

    [Space(20)]
    public MinMax woolTime;
}
