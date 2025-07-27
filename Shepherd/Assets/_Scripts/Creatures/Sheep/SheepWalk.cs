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
        
        // Check if we've reached the target
        float distanceToTarget = Vector3.Distance(manager.transform.position, targetPos);
        if (distanceToTarget <= stoppingDistance) {
            // Stop the sheep
            rb.linearVelocity = Vector3.zero;
            // Could transition to idle here or pick a new target
            Debug.Log("Reached target position");
            return;
        }
        
        // Move towards target using Rigidbody
        Vector3 moveForce = direction * manager.stats.walkSpeed.RandomValue();
        
        rb.linearVelocity = new Vector3(moveForce.x, rb.linearVelocity.y, moveForce.z);
        
        // Option 2: Add force (more physics-based, uncomment to use instead)
        // rb.AddForce(moveForce, ForceMode.Force);
        
        // Option 3: Move position (kinematic-like but still uses Rigidbody)
        // rb.MovePosition(manager.transform.position + moveForce * Time.fixedDeltaTime);
        
        // Rotate to face movement direction (optional)
        if (direction.magnitude > 0.1f) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, targetRotation, manager.stats.rotationSpeed * Time.deltaTime);
        }
        Debug.Log("Updating Sheep Walk");
    }

    public override void ExitState(SheepStateManager manager) {
        Debug.Log("Exiting Sheep Walk");
    }
}
