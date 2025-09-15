using UnityEngine;

namespace HerdingSystem
{
    [CreateAssetMenu(menuName = "Herding System/Herding Data", fileName = "New Herding Assist Data")]
    public class HerdAssistData : ScriptableObject
    {
        public float centeringForce;
        public float pushforce;
    }
}
