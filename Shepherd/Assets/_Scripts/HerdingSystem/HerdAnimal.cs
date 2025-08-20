using System;
using UnityEngine;

public class HerdAnimal : MonoBehaviour
{
    public HerdAnimalName animalName;
    public HerdAreaName currHerdArea;

    private void Start() {
        HerdManager.Instance.AddHerdAnimal(this);
    }

    private void OnDisable() {
        HerdManager.Instance.RemoveHerdAnimal(this);
    }
}
