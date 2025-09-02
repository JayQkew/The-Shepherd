using System;
using _Scripts.Creatures.Sheep.StateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SheepGUI : MonoBehaviour
{
    [SerializeField] private GameObject[] allWool;

    [SerializeField] private GameObject[] smallWool;
    [SerializeField] private GameObject[] mediumWool;
    [SerializeField] private GameObject[] largeWool;

    [SerializeField] private Animator anim;
    [SerializeField] private Timer suppAnimTimer;
    [SerializeField] private string[] suppAnims;
    
    private SheepStateManager stateManager;

    private void Awake() {
        stateManager = GetComponent<SheepStateManager>();
    }

    public void ChangeWool(int woolLength) {
        switch (woolLength) {
            case 0:
                ActivateWool(smallWool);
                break;
            case 1:
                ActivateWool(mediumWool);
                break;
            case 2:
                ActivateWool(largeWool);
                break;
        }
    }

    private void ActivateWool(GameObject[] wool) {
        foreach (GameObject w in allWool) {
            w.SetActive(false);
        }

        foreach (GameObject w in wool) {
            w.SetActive(true);
        }
    }

    public void PlayAnim(string state) => anim.SetTrigger(state);

    public void UpdateSuppAnims() {
        suppAnimTimer.Update();
        if (suppAnimTimer.IsFinished) {
            // choose a random chill animation to play
            anim.SetTrigger(suppAnims[Random.Range(0, suppAnims.Length)]);
            suppAnimTimer.SetMaxTime(stateManager.stats.suppAnimTimer.RandomValue());
        }
    }
    
    public enum WoolLength
    {
        Small,
        Medium,
        Large
    }
}