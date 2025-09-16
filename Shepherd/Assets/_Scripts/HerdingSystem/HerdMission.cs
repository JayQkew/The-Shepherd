using System;

namespace HerdingSystem
{
    [Serializable]
    public class HerdMission
    {
        public HerdDestination herdDestination;
        public Destination destination;
        public Animal animal;
        public int target;
        public int curr;
        public bool TargetMet => curr >= target;

        public HerdMission(HerdDestination herdDestination, Animal animal, int target) {
            this.herdDestination = herdDestination;
            destination = herdDestination.destination;
            this.animal = animal;
            this.target = target;
        }
    }
}
