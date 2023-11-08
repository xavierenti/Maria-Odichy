using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    private PlayerInputActions playerInputs;
    public static Input_Manager _INPUT_MANAGER;


    private float timeSinceJumpPressed = 0f;
    private Vector2 leftAxisValue = Vector2.zero;
    private bool southButton = false;
    private Vector2 rightAxisValue = Vector2.zero;


    public Vector3 puto;


    private void Awake()
    {
        if( _INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {

            playerInputs = new PlayerInputActions();
            playerInputs.Character.Enable();
            
            playerInputs.Character.Jump.performed += JumpButtonPressed;
            playerInputs.Character.Move.performed += LeftAxisUpdate;
            playerInputs.Character.RotateCamera.performed += RightAxisUpdate;


            _INPUT_MANAGER = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
        

    // Update is called once per frame
    void Update()
    {
        timeSinceJumpPressed += Time.deltaTime;

        InputSystem.Update();
    }

    private void JumpButtonPressed(InputAction.CallbackContext context)
    {
        timeSinceJumpPressed = 0f;
        this.southButton = true;

    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        leftAxisValue = context.ReadValue<Vector2>();

        Debug.Log("Magnitude" + leftAxisValue.magnitude);
        Debug.Log("Magnitude" + leftAxisValue.normalized);


    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        this.rightAxisValue = context.ReadValue<Vector2>();
    }

    public Vector2 GetLeftAxisValue()
    {
        return leftAxisValue;
    }

    public bool GetButtonSouthValue()
    {
        return southButton;
    }

    public Vector2 GetRightAxisValue()
    {
        return rightAxisValue;
    }
}
