using System;
using System.Collections.Generic;
using UnityEngine;

public class HerdArea : MonoBehaviour
{
    public HerdAreaName areaName;
    public List<HerdAnimal> animalsIn = new List<HerdAnimal>();

    private void OnTriggerEnter(Collider other) {
        HerdAnimal animal = other.GetComponent<HerdAnimal>();
        if (animal) {
            animalsIn.Add(other.GetComponent<HerdAnimal>());
            animal.currHerdArea = areaName;
        }
    }

    private void OnTriggerExit(Collider other) {
        HerdAnimal animal = other.GetComponent<HerdAnimal>();
        if (animal) {
            animalsIn.Remove(animal);
            animal.currHerdArea = HerdAreaName.None;
        }
    }
}
