using UnityEngine;

public class _SheepStateManager : MonoBehaviour
{
    public _SheepBaseState currState;
    public SheepStats stats;
    
    public _SheepIdle SheepIdle = new _SheepIdle();
    public _SheepMove SheepMove = new _SheepMove();
    private void Start() {
        currState = SheepIdle;
        currState.EnterState(this);
    }

    private void Update() {
        currState.UpdateState(this);
    }

    public void SwitchState(_SheepBaseState newState) {
        currState.ExitState(this);
        currState = newState;
        currState.EnterState(this);
    }
    
    public _SheepBaseState RandomState() {
        _SheepBaseState[] states =
        {
            SheepIdle.EatState,
            SheepIdle.ChillState,
            SheepIdle.SleepState,
            SheepMove.WalkState
        };
        
        return states[Random.Range(0, states.Length)];
    }
}

public abstract class _SheepBaseState
{
    public abstract void EnterState(_SheepStateManager manager);
    public abstract void UpdateState(_SheepStateManager manager);
    public abstract void ExitState(_SheepStateManager manager);
}

public abstract class _SheepParentState : _SheepBaseState
{
    protected _SheepBaseState currSubstate;

    public virtual void SwitchSubstate(_SheepBaseState newSubstate, _SheepStateManager manager) {
        currSubstate?.ExitState(manager);
        currSubstate = newSubstate;
        currSubstate?.EnterState(manager);
    }

    public override void EnterState(_SheepStateManager manager) {
        HandleParentEnterLogic(manager);
        currSubstate?.EnterState(manager);
    }

    public override void ExitState(_SheepStateManager manager) {
        HandleParentExitLogic(manager);
        currSubstate?.ExitState(manager);
    }

    public override void UpdateState(_SheepStateManager manager) {
        HandleParenUpdateLogic(manager);
        currSubstate?.UpdateState(manager);
    }
    
    protected abstract void HandleParentEnterLogic(_SheepStateManager manager);
    protected abstract void HandleParenUpdateLogic(_SheepStateManager manager);
    protected abstract void HandleParentExitLogic(_SheepStateManager manager);
}