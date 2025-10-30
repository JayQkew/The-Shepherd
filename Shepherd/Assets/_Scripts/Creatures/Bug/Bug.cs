using UnityEngine;

namespace Creatures
{
    public class Bug : Animal
    {
        [Space(25)]
        [Header("Bug")]
        [Space(10)]
        public BugGUI gui;
        [HideInInspector] public BugData bugData;

        protected override void Awake() {
            base.Awake();
            bugData = data as BugData;

            if (bugData == null) {
                Debug.LogWarning("Bug Data was not a Bug Data");
            }
        }
    }
}
