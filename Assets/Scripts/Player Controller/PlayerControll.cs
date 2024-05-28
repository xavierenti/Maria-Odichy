using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float desaceleration;
    [SerializeField]
    private float maxSpeed = 8;
    [SerializeField]
    private float maxSpeedCrouch = 5;
    [SerializeField, Range(2.0f, 10.0f)]
    private float rotationSpeed;
    [SerializeField]
    private float gravity = 20f;
    [SerializeField]
    private Camera thecamera;

    [Header ("Jump")]
    [SerializeField]
    private float jumpForce = 8f;
    [SerializeField]
    private float maxNextJumpTimer = 0.3f;

    Input_Manager input_Manager;

    private float directionY = -1f;
    private bool jump = false;
    private bool Crunch = false;
    private float CrunchHeight = 1;
    private float CrunchCenter = 0.1f;
    private float heightDefault = 0;
    private float CenterDefault = 0;
    private int jumpCount = 0;
    private float nextJumpTimer = 0.0f;

    private float longJumpAngle = 20f * Mathf.PI / 180;
    private float mortalJumpAngle = 60f * Mathf.PI / 180;
    private float mortallJumpAngle = 45f * Mathf.PI / 180;

    private List<float> forceAdded = 
        new List<float>()
            {1.0f, 1.15f, 1.4f};
    private float alternativeJumpForce = 1.65f;
    private float accelerateZ;
    private float currentSpeed = 0f;

    private CharacterController controller;


    private Vector3 finalVelocity = Vector3.zero;
    private float velocityXZ = 5f;

    private float coyoteTime = 1f;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Vector3 direction = Vector3.zero;

        if (!jump)
        {
            //Calcular direccion XZ
            direction = Quaternion.Euler(0f, thecamera.transform.eulerAngles.y, 0f) * new Vector3(Input_Manager._INPUT_MANAGER.GetLeftAxisValue().x, 0f, Input_Manager._INPUT_MANAGER.GetLeftAxisValue().y);
            direction.Normalize();

            // Calcular la aceleración en XZ
            // Nos guardamos la direccíón en la que estábamos yendo para
            // poder continuar el movimento en caso que dejemos de pulsar
            // o mover el joystick y no se frene en seco (direction = Vector3.zero)

            // Limitamos la velocidad a la máxima indicada en el inspector
            // Diferenciaremos entre la velocidad agachado o normal

            if (Crunch)
            {
                velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxSpeedCrouch);
            }
            else
            {
                velocityXZ = Mathf.Clamp(velocityXZ, 0f, maxSpeed);
            }

            // Calculamos y aplicamos la rotación del personaje
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            this.transform.forward = direction;

            //Calcular velocidad XZ
            finalVelocity.x = direction.x * velocityXZ;
            finalVelocity.z = direction.z * velocityXZ;

            controller.Move(finalVelocity * Time.deltaTime);
        }
        else
        {
            Vector3 directiony = Vector3.up * directionY;
            directiony.Normalize();

            if (controller.isGrounded)
            {
                nextJumpTimer += Time.deltaTime;
                if (Input_Manager._INPUT_MANAGER.GetButtonSouthValue())
                {
                    if (Crunch)
                    {
                        if (velocityXZ > 0)
                        {
                            LongJump();
                        }
                        else if (velocityXZ == 0)
                        {
                            MortalJump();
                        }

                    }
                    else
                    {
                        jumpCount++;

                        if (nextJumpTimer <= maxNextJumpTimer)
                        {
                            if (jumpCount >= forceAdded.Count)
                            {
                                jumpCount = 0;
                            }
                        }
                        else
                        {
                            jumpCount = 0;

                        }

                        finalVelocity.y = jumpForce * forceAdded[jumpCount];
                        nextJumpTimer = 0f;
                    }
                    jump = true;
                }
                else
                {
                    finalVelocity.y = direction.y * gravity * Time.deltaTime;
                    DeccelerateXZ();
                    jump = false;
                }
            }
        }
    }
    private void DeccelerateXZ()
    {
        // Vamos decelerando al jugador
        finalVelocity.x -= desaceleration * Time.deltaTime;
        finalVelocity.z -= desaceleration * Time.deltaTime;

        finalVelocity.x = Mathf.Clamp(finalVelocity.x, 0f, finalVelocity.x);
        finalVelocity.z = Mathf.Clamp(finalVelocity.z, 0f, finalVelocity.z);

        accelerateZ = 0.0f;
    }

    private void LongJump()
    {
        // DIRECTION = (FORWARD * COS(20) * FORCE) + (UP * SIN(20) * FORCE)
        Vector3 jumpDirection = (controller.transform.forward * Mathf.Cos(longJumpAngle)) + (controller.transform.up * Mathf.Sin(longJumpAngle));
        jumpDirection.Normalize();

        jumpDirection *= jumpForce * alternativeJumpForce;

        finalVelocity = jumpDirection;
    }

    private void MortalJump()
    {
        // DIRECTION = (FORWARD * COS(60) * -1 * FORCE) + (UP * SIN(60) * FORCE)
        Vector3 jumpDirection = (controller.transform.forward * -1f * Mathf.Cos(mortallJumpAngle)) + (controller.transform.up * Mathf.Sin(mortallJumpAngle));
        jumpDirection.Normalize();

        accelerateZ = jumpDirection.z * jumpForce * alternativeJumpForce;
        jumpDirection.y *= jumpForce * alternativeJumpForce;

        finalVelocity = jumpDirection;
    }

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
