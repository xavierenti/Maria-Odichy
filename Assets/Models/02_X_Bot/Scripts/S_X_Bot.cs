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
    Camera camera;

    private bool jump = false;
    private float currentSpeed = 0f;
    

    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
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

        controller.Move(finalVelocity * Time.deltaTime);

        currentSpeed = finalVelocity.magnitude;

        //currentSpeed = Mathf.Clamp(currentSpeed, 0f, ); 

        //transform.position += transform.forward * currentSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Z))
        {
            jump = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (jump) {
            jump = false;
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
