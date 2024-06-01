using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{

public Animator animator;
    void Start()
    {
    animator.SetBool("isWalking", false);
    animator.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void Update()
    {
    /*if(self.attackDetection != None) {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }
    else if(self.currentSpeed > 0.3) {
        animator.SetBool("isAttacking",false);
        animator.SetBool("isWalking", true);
    }
    else {
        animator.SetBool("isWalking",false);
        animator.SetBool("isAttacking", false);
        }*/
        
    }
}
