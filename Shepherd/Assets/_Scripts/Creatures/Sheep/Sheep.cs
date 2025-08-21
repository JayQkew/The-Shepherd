using UnityEngine;
using UnityEngine.Events;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    private SphereCollider col;
    private SheepStateManager sheepStateManager;
    private SheepGUI gui;
    [SerializeField] private float barkForce;
    
    [Header("Wool")]
    [SerializeField] private float wool;
    
    [SerializeField] private Timer woolTimer;
    [SerializeField] private float currWeight;
    [SerializeField] private MinMax weight;

    [Header("Explosion")]
    [SerializeField] private float radius;
    [SerializeField] private float forceMult;
    [SerializeField] private UnityEvent onExplode;
    [SerializeField]private bool showGizmos;

    private float prevWool;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        sheepStateManager = GetComponent<SheepStateManager>();
        gui = GetComponent<SheepGUI>();
        woolTimer.SetMaxTime(sheepStateManager.stats.woolTime.RandomValue());
        prevWool = wool;
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        rb.AddForce(dir * barkForce, ForceMode.Impulse);
        sheepStateManager.SwitchState(sheepStateManager.sheepRun);
        Debug.Log("Barked At");
    }

    private void Update() {
        GrowWool();
        WoolCheck();
    }
    
    private void GrowWool() {
        woolTimer.Update();
        wool = woolTimer.Progress;
        rb.mass = weight.Lerp(wool);
    }

    /// <summary>
    /// Checks for when the Sheep wool meets a threshold
    /// </summary>
    private void WoolCheck() {
        if (wool <= 0.1f) gui.ChangeWool(SheepGUI.WoolLength.Small);
        if (prevWool < 0.3f && wool >= 0.3f) {
            gui.ChangeWool(SheepGUI.WoolLength.Medium);
            PuffExplosion();
        }
        if (prevWool < 0.6f && wool >= 0.6f) {
            gui.ChangeWool(SheepGUI.WoolLength.Large);
            PuffExplosion();
        }
        
        prevWool = wool;
    }

    private void PuffExplosion() {
        Vector3 origin = transform.position + new Vector3(0, -col.radius, 0);
        Collider[] cols = Physics.OverlapSphere(origin, radius);
        if (cols.Length == 0) return;

        foreach (Collider c in cols) {
            IBarkable barkable = c.GetComponent<IBarkable>();
            if (barkable != null) {
                barkable.BarkedAt(origin);
            }
            else {
                Rigidbody targetRb = c.GetComponent<Rigidbody>();
                if (targetRb != null) {
                    Vector3 dir = (c.transform.position - origin).normalized;
                    float distance = Vector3.Distance(origin, targetRb.transform.position);
                    float fallOff = Mathf.Clamp01(1 - distance / radius);

                    float force = forceMult * fallOff;
                    targetRb.AddForce(dir * force, ForceMode.Impulse);
                }
            }
        }
        
        onExplode?.Invoke();
    }

    private void OnDrawGizmos() {
        if (showGizmos) {
            col = GetComponent<SphereCollider>();
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, -col.radius, 0), radius);
        }
    }
}
