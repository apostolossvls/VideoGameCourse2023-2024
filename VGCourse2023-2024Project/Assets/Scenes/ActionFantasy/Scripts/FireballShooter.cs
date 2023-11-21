using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public Fireball fireballPrebab;
    public Transform castingPoint;
    public Animator animator;
    public float shootForce = 10;
    public float preparingTime = 1;
    public float rechargeTime = 1f;
    private float rechargeTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rechargeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (rechargeTimer > 0) rechargeTimer -= Time.deltaTime;

        if (Input.GetMouseButton(1) && rechargeTimer <= 0)
        {
            rechargeTimer = rechargeTime;

            Shoot();
        }
    }

    void Shoot()
    {
        animator.SetTrigger("FireballAttack");
        Fireball fireball = Instantiate(fireballPrebab, castingPoint.position, castingPoint.rotation);
        fireball.transform.SetParent(castingPoint);
        fireball.Activate(castingPoint, shootForce, preparingTime);
    }
}
