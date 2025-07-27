using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SheepIdle : SheepParentState
{
    public SheepEat EatState = new SheepEat();
    public SheepChill ChillState = new SheepChill();
    public SheepSleep SleepState = new SheepSleep();
    
    protected override void HandleParentEnterLogic(SheepStateManager manager) {
        currSubstate = ChillState;
        Debug.Log("Sheep Idle Parent Enter");
    }
    
    protected override void HandleParenUpdateLogic(SheepStateManager manager) {
    }
    
    protected override void HandleParentExitLogic(SheepStateManager manager) {
        Debug.Log("Sheep Idle Parent Exit");
    }
}
