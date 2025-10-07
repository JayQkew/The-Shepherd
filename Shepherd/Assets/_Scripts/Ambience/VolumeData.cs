using UnityEngine;

namespace Ambience
{
    [CreateAssetMenu(fileName = "NewVolumeData", menuName = "Ambience/Volume")]
    public class VolumeData : ScriptableObject
    {
        public AnimationCurve hueShiftCurve;
    }
}
