using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class _SheepWalk : _SheepBaseState
{
    public Vector3 targetPos;
    public float speed;
    private Rigidbody rb;
    public override void EnterState(_SheepStateManager manager) {
        Debug.Log("Entering Sheep Walk");
        
        if(!rb) rb = manager.GetComponent<Rigidbody>();
        Vector2 randomPos = Random.insideUnitCircle * manager.stats.walkRadius;
        targetPos = new Vector3(randomPos.x + manager.transform.position.x, manager.transform.position.y,  randomPos.y + manager.transform.position.z );
        speed = manager.stats.walkSpeed.RandomValue();
    }

    public override void UpdateState(_SheepStateManager manager) {
        Vector3 direction = (targetPos - manager.transform.position).normalized;
        
        float distanceToTarget = Vector3.Distance(manager.transform.position, targetPos);
        if (distanceToTarget <= 0.1f) {
            rb.linearVelocity = Vector3.zero;
            manager.SwitchState(manager.RandomState());
            return;
        }
        
        Vector3 moveForce = direction * speed;
        rb.linearVelocity = new Vector3(moveForce.x, rb.linearVelocity.y, moveForce.z);
    }

    public override void ExitState(_SheepStateManager manager) {
        Debug.Log("Exiting Sheep Walk");
    }
}
