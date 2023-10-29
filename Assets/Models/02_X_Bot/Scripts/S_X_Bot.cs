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
    Camera camera;

    Input_Manager input_Manager;

    private bool jump = false;
    private float currentSpeed = 0f;

    private float jumpadForce = 100f;
    

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
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
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
            if(Input.GetKey(KeyCode.Space))
            {
                finalVelocity.y = JumpForce;
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
            }
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;

            if(Input.GetKey(KeyCode.Space)&& coyoteTime >= 0f)
            {
                finalVelocity.y = JumpForce;
                coyoteTime = 0f;
            }
        }

        



        controller.Move(finalVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (jump) {
            jump = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Jumpad")
        {
            finalVelocity.y = jumpadForce;
        }
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
