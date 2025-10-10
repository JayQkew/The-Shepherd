using UnityEngine;

namespace Ambience
{
    public abstract class ProfileData
    {
        public bool Use;

        public void Process(ProfileData tempData) {
            if (!ValidateType(tempData)) {
                Debug.LogWarning($"Cannot Process {GetType().Name}");
                return;
            }
            
            ProcessInternal(tempData);
        }

        protected virtual bool ValidateType(ProfileData tempData) {
            return tempData.GetType() == GetType();
        }
        
        protected abstract void ProcessInternal(ProfileData tempData);
    }
}
