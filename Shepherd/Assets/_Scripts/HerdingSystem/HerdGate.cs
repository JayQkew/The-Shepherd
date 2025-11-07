using OffScreenIndicator;
using UnityEngine;
using UnityEngine.Splines;

namespace HerdingSystem
{
    public class HerdGate : MonoBehaviour
    {
        private static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator anim;

        public void GateControl(bool open) {
            string trigger = open ? "Open" : "Close";
            anim.SetTrigger(trigger);
        }
    }
}
