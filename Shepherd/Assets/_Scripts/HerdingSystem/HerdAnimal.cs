using System;
using _Scripts.Creatures;
using UnityEngine;

public class HerdAnimal : Animal
{
    public HerdAreaName currHerdArea;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        base.Start();
        HerdManager.Instance.AddHerdAnimal(this);
    }

    private void OnDisable() {
        HerdManager.Instance.RemoveHerdAnimal(this);
    }
}
