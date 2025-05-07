using UnityEngine;
[CreateAssetMenu(fileName = "BoidData", menuName = "Creatures/BoidData")]
public class BoidData : ScriptableObject
{
    public float cohesion;
    public float separation;
    public float alignment;
    public float minSeparation;
}
