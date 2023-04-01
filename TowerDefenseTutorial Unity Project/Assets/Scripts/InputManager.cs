using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //private PlayerInput playerInput;
    //public PlayerInput.ActionEvent level;

    public InputAction playerControls;
    
   private void Awake()
    {
        //playerInput = new PlayerInput();
        /*
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        onFoot.Jump.performed += ctx => motor.Jump();
        look = GetComponent<PlayerLook>();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        */
        
    }
    
    
    void FixedUpdate()
    {
        //tell playermotor to move using value from movement action
        //motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        
        
    }

    private void LateUpdate()
    {
        //look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

}
