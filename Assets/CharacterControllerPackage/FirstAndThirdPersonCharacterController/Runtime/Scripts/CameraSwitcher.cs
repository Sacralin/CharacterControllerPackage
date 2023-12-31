using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;
    FirstAndThirdPersonCharacterInputs inputActions;

    void Start()
    {
        inputActions = new FirstAndThirdPersonCharacterInputs();
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            thirdPersonCamera.gameObject.SetActive(!thirdPersonCamera.gameObject.activeSelf);
            firstPersonCamera.gameObject.SetActive(!firstPersonCamera.gameObject.activeSelf);
        }
    }

   


}
