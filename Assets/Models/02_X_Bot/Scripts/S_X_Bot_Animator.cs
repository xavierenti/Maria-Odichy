using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_X_Bot_Animator : MonoBehaviour
{
    private S_X_Bot x_Bot;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        x_Bot = GetComponent<S_X_Bot>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", x_Bot.GetCurrentSpeed());
        animator.SetFloat("velocityy", x_Bot.GetCurrentSpeedy());
        animator.SetBool("Jump", x_Bot.Jump());
    }
}
