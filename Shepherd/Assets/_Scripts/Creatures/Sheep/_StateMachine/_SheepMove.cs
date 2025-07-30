using System;
using UnityEngine;

[Serializable]
public class _SheepMove : _SheepParentState
{
    public _SheepWalk WalkState = new _SheepWalk();
    public _SheepRun RunState = new _SheepRun();
    protected override void HandleParentEnterLogic(_SheepStateManager manager) {
        Debug.Log("Sheep Move Parent Enter");
    }
    
    protected override void HandleParenUpdateLogic(_SheepStateManager manager) {
    }
    
    protected override void HandleParentExitLogic(_SheepStateManager manager) {
        Debug.Log("Sheep Move Parent Exit");
    }
}
