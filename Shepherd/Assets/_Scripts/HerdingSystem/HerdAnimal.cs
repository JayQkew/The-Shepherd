using System;
using UnityEngine;

public class HerdAnimal : MonoBehaviour
{
    public HerdAnimalName animalName;
    public HerdAreaName currHerdArea;

    private void OnEnable() {
        HerdManager.Instance.AddHerdAnimal(this);
    }

    private void OnDisable() {
        HerdManager.Instance.RemoveHerdAnimal(this);
    }
}
