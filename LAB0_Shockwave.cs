using UnityEngine;

public class LAB0_Shockwave : MonoBehaviour
{
    public float shockwaveRadius = 5.0f;
    public float shockwaveForce = 500.0f;
    public float explosionLift = 1.0f;
    public GameObject shockwaveEffect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerShockwave();
        }
    }

    public void TriggerShockwave()
    {
        if (shockwaveEffect != null)
        {
            Instantiate(shockwaveEffect, transform.position, Quaternion.identity);
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, shockwaveRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(shockwaveForce, explosionPos, shockwaveRadius, explosionLift);
            }
        }
    }
}
