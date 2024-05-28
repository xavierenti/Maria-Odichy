using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_X_Bot : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float gravity = 20f;
    [SerializeField]
    private Camera thecamera;


    Input_Manager input_Manager;

    //target 
    private GameObject Jumi;

    private bool jump = false;
    private bool Crunch = false;
    private float CrunchHeight = 1;
    private float CrunchCenter = 0.1f;
    private float heightDefault = 0;
    private float CenterDefault = 0;


    private float currentSpeed = 0f;

    private CharacterController controller;


    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;

    private float coyoteTime = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
        input_Manager = GetComponent<Input_Manager>();

        Jumi = GameObject.FindGameObjectWithTag("Jumpad");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Quaternion.Euler(0f, thecamera.transform.eulerAngles.y, 0f) * new Vector3(Input_Manager._INPUT_MANAGER.GetLeftAxisValue().x, 0f, Input_Manager._INPUT_MANAGER.GetLeftAxisValue().y);
        direction.Normalize();

        //Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        currentSpeed = finalVelocity.magnitude;

        //currentSpeed = Mathf.Clamp(currentSpeed, 0f, ); 

        //transform.position += transform.forward * currentSpeed * Time.deltaTime;

        //Gravity
        //Salto

        direction.y = -1f;

        if (controller.isGrounded)
        {
            jump = false;
            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
            {
                finalVelocity.y = JumpForce;
                jump = true;
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
            }
        }
        else
        {
            jump = false;
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;

            if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue() && coyoteTime >= 0f)
            {
                finalVelocity.y = JumpForce;
                jump = true;
                coyoteTime = 0f;
            }
        }

        if (Input_Manager._INPUT_MANAGER.GetLeftShoulderValue() && controller.isGrounded)
        {
            Crunch = !Crunch;

            if (Crunch)
            {
                controller.center = new Vector3(controller.center.x, CrunchCenter, controller.center.z);
                controller.height = CrunchHeight;
            }
            else
            {
                controller.center = new Vector3(controller.center.x, CenterDefault, controller.center.z);
                controller.height = heightDefault;
            }
        }



        controller.Move(finalVelocity * Time.deltaTime);
    }

    //Rebotar
    public void Rebotar(Vector3 jumpDirecton, float jumpForce)
    {
        finalVelocity = jumpDirecton * jumpForce;
    }

    public void Die()
    {
        GameManager._GAME_MANAGER.PlayerHasDied();
    }

    public float GetCurrentSpeed()
    {
        return this.currentSpeed;
    }

    public float GetCurrentSpeedy()
    {
        return this.controller.velocity.y;
    }
    public bool Jump()
    {
        return this.jump;
    }
}
