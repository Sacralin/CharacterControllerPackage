using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    FirstAndThirdPersonCharacterInputs inputActions;
    CharacterController characterController;
    Animator animator;
    
    Vector3 moveDirection;
    Vector2 currentInput;
    float walkSpeed = 2f;
    float runSpeed = 4f;
    float currentSpeed;
    float gravity = -9.81f;
    float jumpForce = 2;

    //float rotationFactorPerFrame = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new FirstAndThirdPersonCharacterInputs();
        inputActions.CharacterControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleJump();
        Crouch();
    }

    private void HandleJump()
    {
        if (inputActions.CharacterControls.SpaceBar.triggered && characterController.isGrounded)
        {
            animator.SetBool("isJumping", true);
            moveDirection.y = jumpForce;
            
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void HandleMovementInput()
    {
        bool runPressed = inputActions.CharacterControls.Run.ReadValue<float>() > 0;

        currentSpeed = runPressed ? runSpeed : walkSpeed;
        if(animator.GetBool("isCrouched") == true) { currentSpeed = walkSpeed; }
        currentInput = inputActions.CharacterControls.Walk.ReadValue<Vector2>();

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.y) + (transform.TransformDirection(Vector3.right) * currentInput.x);
        moveDirection.y = moveDirectionY;
        if (!characterController.isGrounded) { moveDirection.y += gravity * Time.deltaTime; }
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
    }

    private void Crouch()
    {
        bool isCrouched = animator.GetBool("isCrouched");
        float standingHeight = 1.8f;
        float crouchedHeight = 1.3f;
        float standingCenter = 0.98f;
        float crouchedCenter = 0.7f;


        if (inputActions.CharacterControls.Crouch.triggered)
        {
            animator.SetBool("isCrouched", !isCrouched);
            characterController.height = isCrouched ? standingHeight : crouchedHeight;
            characterController.center = isCrouched ? new Vector3(0, standingCenter, 0) : new Vector3(0, crouchedCenter, 0);

        }
    }
    



}
