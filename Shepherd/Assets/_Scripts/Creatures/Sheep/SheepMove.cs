using System;
using UnityEngine;

[Serializable]
public class SheepMove : SheepParentState
{
    public SheepWalk WalkState = new SheepWalk();
    public SheepRun RunState = new SheepRun();
    protected override void HandleParentEnterLogic(SheepStateManager manager) {
        Debug.Log("Sheep Move Parent Enter");
    }
    
    protected override void HandleParenUpdateLogic(SheepStateManager manager) {
        Debug.Log("Sheep Move Parent Update");
    }
    
    protected override void HandleParentExitLogic(SheepStateManager manager) {
        Debug.Log("Sheep Move Parent Exit");
    }
}
