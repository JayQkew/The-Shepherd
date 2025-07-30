using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class _SheepIdle : _SheepParentState
{
    public _SheepEat EatState = new _SheepEat();
    public _SheepChill ChillState = new _SheepChill();
    public _SheepSleep SleepState = new _SheepSleep();
    
    protected override void HandleParentEnterLogic(_SheepStateManager manager) {
        currSubstate = ChillState;
        Debug.Log("Sheep Idle Parent Enter");
    }
    
    protected override void HandleParenUpdateLogic(_SheepStateManager manager) {
    }
    
    protected override void HandleParentExitLogic(_SheepStateManager manager) {
        Debug.Log("Sheep Idle Parent Exit");
    }
}
