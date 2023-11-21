using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Setup")]
    public Rigidbody rb;
    public Collider col;
    public Vector3 finalScale = Vector3.one;
    [Header("Properties")]
    public List<string> targetTags = new List<string>();
    public float damage = 1;

    public void Activate(Transform castingPoint, float force, float preparingTime)
    {
        StartCoroutine(Shoot(castingPoint, force, preparingTime));
    }

    IEnumerator Shoot(Transform castingPoint, float force, float preparingTime)
    {
        float timer = 0;
        
        if (preparingTime <= 0) preparingTime = 0.00001f;

        transform.localScale = Vector3.one * 0.01f;
        while (timer < preparingTime)
        {
            Vector3 scaleAmount = Vector3.Lerp(Vector3.zero, finalScale, timer / preparingTime);
            transform.localScale = scaleAmount;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.SetParent(null);

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(castingPoint.forward * force, ForceMode.VelocityChange);
            rb.useGravity = true;
        }
        if (col != null)
        {
            col.enabled = true;
        }
    }

    //Damage targets
    private void OnCollisionEnter(Collision collision)
    {
        //if other has a targettable tag
        if (targetTags.Contains(collision.gameObject.tag))
        {
            //search for health script and deal damage
            Health otherHealth = collision.gameObject.GetComponentInChildren<Health>();
            otherHealth?.TakeDamage(damage);
        }
    }
}
