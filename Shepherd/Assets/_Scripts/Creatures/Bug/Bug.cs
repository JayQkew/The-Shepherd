using System;
using Climate;
using Unity.VisualScripting;
using UnityEngine;

namespace Creatures
{
    public class Bug : Animal
    {
        [Space(25)]
        [Header("Bug")]
        [Space(10)]
        
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
