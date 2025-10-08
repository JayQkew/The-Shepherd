using UnityEngine;

namespace Climate
{
    [CreateAssetMenu(fileName = "NewSeasonData", menuName = "Climate/Season")]
    public class SeasonData : ScriptableObject
    {
        public Season[] seasons;
    }
}
