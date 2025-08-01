using System;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public class SheepMove : SheepBaseState
{
    public Vector3 targetPos;
    public float speed;
    private Rigidbody rb;
    public override void EnterState(SheepStateManager manager) {
        if(!rb) rb = manager.GetComponent<Rigidbody>();
        Vector2 randomPos = Random.insideUnitCircle * manager.stats.walkRadius;
        targetPos = new Vector3(randomPos.x + manager.transform.position.x, manager.transform.position.y,  randomPos.y + manager.transform.position.z );
        speed = manager.stats.walkSpeed.RandomValue();
        Debug.Log("Enter -- SheepMove");
    }

    public override void UpdateState(SheepStateManager manager) {
        Vector3 direction = (targetPos - manager.transform.position).normalized;
        
        float distanceToTarget = Vector3.Distance(manager.transform.position, targetPos);
        if (distanceToTarget <= 0.1f) {
            rb.linearVelocity = Vector3.zero;
            manager.SwitchState(manager.GetRandomState());
            return;
        }
        
        Vector3 moveForce = direction * speed;
        rb.linearVelocity = new Vector3(moveForce.x, rb.linearVelocity.y, moveForce.z);
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exit -- SheepMove");
    }
}
