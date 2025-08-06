using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Sheep : MonoBehaviour, IBarkable
{
    private Rigidbody rb;
    private SphereCollider col;
    private SheepStateManager sheepStateManager;
    [SerializeField] private float barkForce;
    
    [Header("Wool")]
    [SerializeField] private float wool;
    [SerializeField] private float woolCurrTime;
    [SerializeField] private float woolGrowTime;
    [SerializeField] private float currWeight;
    [SerializeField] private MinMax weight;

    [Header("Explosion")]
    [SerializeField] private float radius;
    [SerializeField] private float forceMult;

    private float prevWool;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        sheepStateManager = GetComponent<SheepStateManager>();
        prevWool = wool;
    }

    public void BarkedAt(Vector3 sourcePosition) {
        Vector3 dir = (transform.position - sourcePosition).normalized;
        rb.AddForce(dir * 10, ForceMode.Impulse);
        sheepStateManager.SwitchState(sheepStateManager.sheepRun);
        Debug.Log("Barked At");
    }

    private void Update() {
        GrowWool();
        WoolCheck();
    }
    
    private void GrowWool() {
        woolCurrTime += Time.deltaTime;
        woolCurrTime = Mathf.Clamp(woolCurrTime, 0, woolGrowTime);
        wool = woolCurrTime / woolGrowTime;
        rb.mass = weight.Lerp(wool);
    }

    /// <summary>
    /// Checks for when the Sheep wool meets a threshold
    /// </summary>
    private void WoolCheck() {
        if (prevWool < 0.3f && wool >= 0.3f) PuffExplosion();
        if (prevWool < 0.6f && wool >= 0.6f) PuffExplosion();
        prevWool = wool;
    }

    private void PuffExplosion() {
        Vector3 origin = transform.position + new Vector3(0, -col.radius, 0);
        Collider[] cols = Physics.OverlapSphere(origin, radius);
        if (cols.Length == 0) return;

        foreach (Collider c in cols) {
            if (c.gameObject == gameObject) continue;
            Rigidbody targetRb = c.GetComponent<Rigidbody>();
            if (targetRb != null) {
                Vector3 dir = (c.transform.position - origin).normalized;
                float distance = Vector3.Distance(origin, targetRb.transform.position);
                float fallOff = Mathf.Clamp01(1 - distance / radius);

                float force = forceMult * fallOff;
                targetRb.AddForce(dir * force, ForceMode.Impulse);
                Debug.Log("HELLOOOOOO");
            }
        }
    }

    private void OnDrawGizmos() {
        col = GetComponent<SphereCollider>();
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, -col.radius, 0), radius);
    }
}
