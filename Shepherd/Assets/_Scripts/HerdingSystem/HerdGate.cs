using OffScreenIndicator;
using UnityEngine;
using UnityEngine.Splines;

namespace HerdingSystem
{
    public class HerdGate : MonoBehaviour
    {
        [SerializeField] private GameObject gate;

        public void GateControl(bool open) {
            gate.SetActive(!open);
        }
    }
}
