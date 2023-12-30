using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    FirstAndThirdPersonCharacterInputs inputActions;
    CharacterController characterController;
    Animator animator;
    Vector3 moveDirection;
    Vector2 currentInput;
    int moveSpeed = 2;
    
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
        //handleRotation();
        HandleAnimation();
    }

    private void HandleMovementInput()
    {
        currentInput = inputActions.CharacterControls.Walk.ReadValue<Vector2>();

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.y) + (transform.TransformDirection(Vector3.right) * currentInput.x);
        moveDirection.y = moveDirectionY;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    private void HandleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");
        bool runKeyPressed = inputActions.CharacterControls.Run.ReadValue<float>() != 0;
        bool isWalkingBackward = animator.GetBool("isWalkingBackward");
        bool isWalkingBackwardRight = animator.GetBool("isWalkingBackwardRight");

        //walking forward
        if (currentInput.y > 0 && !isWalking) 
        {
            animator.SetBool("isWalking", true);
        }
        if (currentInput.y == 0 && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        //running forward
        if ((isWalking && runKeyPressed) && !isRunning)
        {
            animator.SetBool("isRunning", true);
            
        }
        if (!runKeyPressed && isRunning)
        {
            animator.SetBool("isRunning", false);
        }

        //walking backward
        if (currentInput.y < 0 && !isWalkingBackward)
        {
            animator.SetBool("isWalkingBackward", true);
        }
        if (currentInput.y == 0 && isWalkingBackward)
        {
            animator.SetBool("isWalkingBackward", false);
        }

        //walking backward right
        if (currentInput.y < 0 && currentInput.x > 0 && !isWalkingBackwardRight)
        {
            animator.SetBool("isWalkingBackwardRight", true);
        }


        //set speed
        //if (isWalking || isWalkingBackward)
        //{
        //    speed = moveSpeed;
        //}
        //if (isRunning)
        //{
        //    speed = runSpeed;
        //}


    }

}
