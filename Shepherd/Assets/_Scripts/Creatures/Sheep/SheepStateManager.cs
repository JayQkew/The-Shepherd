using UnityEngine;

public class SheepStateManager : MonoBehaviour
{
    private SheepBaseState currState;
    
    public SheepIdle sheepIdle = new SheepIdle();
    private void Start() {
        currState = sheepIdle;
        currState.EnterState(this);
    }

    private void Update() {
        currState.UpdateState(this);
    }

    public void SwitchState(SheepBaseState newState) {
        currState.ExitState(this);
        currState = newState;
        currState.EnterState(this);
    }
}

public abstract class SheepBaseState
{
    public abstract void EnterState(SheepStateManager manager);
    public abstract void UpdateState(SheepStateManager manager);
    public abstract void ExitState(SheepStateManager manager);
}