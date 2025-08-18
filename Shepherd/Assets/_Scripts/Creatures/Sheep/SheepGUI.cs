using UnityEngine;

public class SheepGUI : MonoBehaviour
{
    [SerializeField] private GameObject[] allWool;

    [SerializeField] private GameObject[] smallWool;
    [SerializeField] private GameObject[] mediumWool;
    [SerializeField] private GameObject[] largeWool;

    [SerializeField] private Animator anim;

    public void ChangeWool(WoolLength woolLength) {
        switch (woolLength) {
            case WoolLength.Small:
                ActivateWool(smallWool);
                break;
            case WoolLength.Medium:
                ActivateWool(mediumWool);
                break;
            case WoolLength.Large:
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
    public enum WoolLength
    {
        Small,
        Medium,
        Large
    }
}