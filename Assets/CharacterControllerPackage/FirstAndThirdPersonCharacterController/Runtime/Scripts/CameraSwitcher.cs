using UnityEngine;
using Cinemachine;
using System;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineVirtualCamera firstPersonCrouchedCamera;
    public Camera mainCamera;
    Animator animator;
    FirstAndThirdPersonCharacterInputs inputActions;
    bool isFirstPerson;
    bool isCrouched;
    bool hasSwitchedRecently = false;
    
    void Start()
    {
        inputActions = new FirstAndThirdPersonCharacterInputs();
        animator = GetComponent<Animator>();
        thirdPersonCamera.gameObject.SetActive(true);
        isFirstPerson = false;
        firstPersonCamera.gameObject.SetActive(false);
        firstPersonCrouchedCamera.gameObject.SetActive(false);
        
    }

    void Update()
    {
        SwitchPersonPerspective();
        SwitchCrouchedPerspective();


    }

    

    private void SwitchPersonPerspective()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hasSwitchedRecently = true;
            isCrouched = animator.GetBool("isCrouched");
            isFirstPerson = !isFirstPerson;
            if (isFirstPerson)
            {
                thirdPersonCamera.gameObject.SetActive(false); // disable third person camera
                firstPersonCrouchedCamera.gameObject.SetActive(isCrouched);
                firstPersonCamera.gameObject.SetActive(!isCrouched);

                
            }
            else
            {
                thirdPersonCamera.gameObject.SetActive(true);
                firstPersonCrouchedCamera.gameObject.SetActive(false);
                firstPersonCamera.gameObject.SetActive(false);
                mainCamera.cullingMask |= LayerMask.GetMask("FirstPersonHidden"); //restores player character 
            }

        }
        if(hasSwitchedRecently && isFirstPerson)
        {
            if (mainCamera.transform.position == firstPersonCamera.transform.position              //check if camera view has reached target camera view
                || mainCamera.transform.position == firstPersonCrouchedCamera.transform.position)  //gives the effect of the camera zooming into the player
            {
                mainCamera.cullingMask &= ~LayerMask.GetMask("FirstPersonHidden"); //then this culls player character in first person to avoid clipping
                hasSwitchedRecently = false;
            }
        }
    }

    private void SwitchCrouchedPerspective()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isFirstPerson)
            {
                isCrouched = animator.GetBool("isCrouched");
                Debug.Log($"isCrouched {isCrouched}");
                firstPersonCrouchedCamera.gameObject.SetActive(!isCrouched); //these appear backwards because when they are called the isCrouched
                firstPersonCamera.gameObject.SetActive(isCrouched);          //variable hasnt changed, not an ideal solution but it works for now
            }
        }
    }
}
