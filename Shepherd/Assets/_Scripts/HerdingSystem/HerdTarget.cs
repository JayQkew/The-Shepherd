using System;

namespace _Scripts.HerdingSystem
{
    [Serializable]
    public class HerdTarget
    {
        public HerdAreaName destination;
        public AnimalName animal;
        public int target;
        public int curr;
        public bool TargetMet => curr >= target;
    }
}
