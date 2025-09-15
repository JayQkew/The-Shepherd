using System;

namespace HerdingSystem
{
    [Serializable]
    public class HerdMission
    {
        public Destination destination;
        public Animal animal;
        public int target;
        public int curr;
        public bool TargetMet => curr >= target;

        public HerdMission(Destination destination, Animal animal, int target) {
            this.destination = destination;
            this.animal = animal;
            this.target = target;
        }
    }
}
