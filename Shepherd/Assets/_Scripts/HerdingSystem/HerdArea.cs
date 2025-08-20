using System;
using System.Collections.Generic;
using UnityEngine;

public class HerdArea : MonoBehaviour
{
    public HerdAreaName areaName;
    public List<HerdAnimal> animalsIn = new List<HerdAnimal>();
    public Dictionary<HerdAnimalName, List<HerdAnimal>> animalsByType = new Dictionary<HerdAnimalName, List<HerdAnimal>>();

    private void OnTriggerEnter(Collider other) {
        HerdAnimal animal = other.GetComponent<HerdAnimal>();
        if (animal) {
            animalsIn.Add(other.GetComponent<HerdAnimal>());
            animal.currHerdArea = areaName;
            if (!animalsByType.ContainsKey(animal.animalName)) {
                animalsByType[animal.animalName] = new List<HerdAnimal>();
            }
            animalsByType[animal.animalName].Add(animal);
        }
    }

    private void OnTriggerExit(Collider other) {
        HerdAnimal animal = other.GetComponent<HerdAnimal>();
        if (animal) {
            animalsIn.Remove(animal);
            animal.currHerdArea = HerdAreaName.None;
            if (animalsByType.TryGetValue(animal.animalName, out var list)) {
                list.Remove(animal);
            }
        }
    }
}
