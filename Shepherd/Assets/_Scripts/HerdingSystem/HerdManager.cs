using System;
using System.Collections.Generic;
using UnityEngine;

public class HerdManager : MonoBehaviour
{
    public static HerdManager Instance { get; private set; }
    
    public List<HerdTarget> targets;
    
    public List<HerdAnimal> allHerdAnimals = new List<HerdAnimal>();
    
    public Dictionary<HerdAnimalName, List<HerdAnimal>> animalsByType = new Dictionary<HerdAnimalName, List<HerdAnimal>>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void AddHerdAnimal(HerdAnimal herdAnimal) {
        if (herdAnimal == null) return;
    
        allHerdAnimals.Add(herdAnimal);

        if (!animalsByType.ContainsKey(herdAnimal.animalName)) {
            animalsByType[herdAnimal.animalName] = new List<HerdAnimal>();
        }
        animalsByType[herdAnimal.animalName].Add(herdAnimal);
    }

    public void RemoveHerdAnimal(HerdAnimal herdAnimal) {
        if (herdAnimal == null) return;

        allHerdAnimals.Remove(herdAnimal);

        if (animalsByType.TryGetValue(herdAnimal.animalName, out var list)) {
            list.Remove(herdAnimal);
        }
    }

    public void CheckTargets(HerdArea herdArea) {
        for (int i = 0; i < targets.Count; i++) {
            HerdTarget herdTarget = targets[i];
            if (herdTarget.destination == herdArea.areaName) {
                herdTarget.curr = herdArea.animalsIn.Count;
            }
        }
    }
}

public enum HerdAreaName
{
    None,
    Pen,
    Field
}

public enum HerdAnimalName
{
    Sheep,
    Ducken
}
