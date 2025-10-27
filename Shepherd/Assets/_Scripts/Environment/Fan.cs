using UnityEngine;

namespace Environment
{
    public class Fan : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Update() {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
        }
    }
}
