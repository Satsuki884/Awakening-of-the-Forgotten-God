using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKey("w"))
        {
            animator.SetBool("melee", true);
        }
        if (!Input.GetKey("w"))
        {
            animator.SetBool("melee", false);
        }

        if (Input.GetKey("q"))
        {
            animator.SetBool("area", true);
        }
        if (!Input.GetKey("q"))
        {
            animator.SetBool("area", false);
        }

        if (Input.GetKey("e"))
        {
            animator.SetBool("range", true);
        }
        if (!Input.GetKey("e"))
        {
            animator.SetBool("range", false);
        }*/
        
        animator.SetBool("melee", Input.GetKeyDown(KeyCode.W));
        animator.SetBool("area", Input.GetKeyDown(KeyCode.Q));
        animator.SetBool("range", Input.GetKeyDown(KeyCode.E));
    }
}
