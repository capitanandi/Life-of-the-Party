using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalkingAnim = animator.GetBool("isWalking");
        bool isJumpingAnim = animator.GetBool("isJumping");

        if(!isWalkingAnim && player.isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        if (!player.isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if (!player.playerGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }
}
