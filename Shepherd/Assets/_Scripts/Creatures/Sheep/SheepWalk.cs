using UnityEngine;

public class SheepWalk : SheepBaseState
{
    public Vector3 targetPos;
    private Rigidbody rb;
    public override void EnterState(SheepStateManager manager) {
        Debug.Log("Entering Sheep Walk");
        
        if(!rb) manager.GetComponent<Rigidbody>();
        Vector2 randomPos = Random.insideUnitCircle * manager.stats.walkRadius;
        targetPos = new Vector3(randomPos.x + manager.transform.position.x, manager.transform.position.y,  randomPos.y + manager.transform.position.z );
    }

    public override void UpdateState(SheepStateManager manager) {
        Vector3 direction = (targetPos - manager.transform.position).normalized;
        
        float distanceToTarget = Vector3.Distance(manager.transform.position, targetPos);
        if (distanceToTarget <= 0.5f) {
            rb.linearVelocity = Vector3.zero;
            Debug.Log("Reached target position");
            return;
        }
        
        Vector3 moveForce = direction * manager.stats.walkSpeed.RandomValue();
        rb.linearVelocity = new Vector3(moveForce.x, rb.linearVelocity.y, moveForce.z);
        
        Debug.Log("Updating Sheep Walk");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Walk");
    }
}
