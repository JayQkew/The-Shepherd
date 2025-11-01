using System;

namespace HerdingSystem
{
    [Serializable]
    public class HerdMission
    {
        public HerdDestination herdDestination;
        public Destination destination;
        public int target;
        public int curr;
        public bool TargetMet => curr >= target;

        public HerdMission(HerdDestination herdDestination, int target) {
            this.herdDestination = herdDestination;
            destination = herdDestination.destination;
            this.target = target;
        }
    }
}
