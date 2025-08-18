using System;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class SheepMove : SheepBaseState
{
    public Vector3 dir;
    public float speed;
    private Rigidbody rb;
    public Timer moveTimer;
    
    public override void EnterState(SheepStateManager manager) {
        if(!rb) rb = manager.GetComponent<Rigidbody>();
        
        dir = Random.insideUnitCircle.normalized;
        speed = manager.stats.walkSpeed.RandomValue();
        
        moveTimer.maxTime = manager.stats.walkTime.RandomValue();
        moveTimer.Reset();
    }

    public override void UpdateState(SheepStateManager manager) {
        moveTimer.Update();
        Vector3 moveForce = new Vector3(dir.x, 0, dir.z) * speed;
        if (moveTimer.IsFinished) {
            manager.SwitchState(manager.GetRandomState());
            return;
        }
        rb.AddForce(moveForce, ForceMode.Acceleration);
    }

    public override void ExitState(SheepStateManager manager) {
    }
}
