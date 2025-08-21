using System;
using UnityEngine;

public class HerdAnimal : MonoBehaviour
{
    public Rigidbody rb;
    public HerdAnimalName animalName;
    public HerdAreaName currHerdArea;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        HerdManager.Instance.AddHerdAnimal(this);
    }

    private void OnDisable() {
        HerdManager.Instance.RemoveHerdAnimal(this);
    }
}
