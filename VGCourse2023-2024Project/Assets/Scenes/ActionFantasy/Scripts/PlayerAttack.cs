using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetTrigger("MeleeAttack");
        }
    }

    public void ResetMelee()
    {
        animator.ResetTrigger("MeleeAttack");
    }
}
