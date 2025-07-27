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

public abstract class SheepParentState : SheepBaseState
{
    protected SheepBaseState currSubstate;

    public virtual void SwitchSubstate(SheepBaseState newSubstate, SheepStateManager manager) {
        currSubstate?.ExitState(manager);
        currSubstate = newSubstate;
        currSubstate?.EnterState(manager);
    }

    public override void EnterState(SheepStateManager manager) {
        HandleParentEnterLogic(manager);
        currSubstate?.EnterState(manager);
    }

    public override void ExitState(SheepStateManager manager) {
        HandleParentExitLogic(manager);
        currSubstate?.ExitState(manager);
    }

    public override void UpdateState(SheepStateManager manager) {
        HandleParentStateLogic(manager);
        currSubstate?.UpdateState(manager);
    }
    
    protected abstract void HandleParentStateLogic(SheepStateManager manager);
    protected virtual void HandleParentEnterLogic(SheepStateManager manager) {}
    protected virtual void HandleParentExitLogic(SheepStateManager manager) {}
}