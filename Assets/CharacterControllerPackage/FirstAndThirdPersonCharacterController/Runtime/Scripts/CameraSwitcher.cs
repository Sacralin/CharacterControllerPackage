using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    void Start()
    {
        // Enable only the third-person camera initially
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        // Example: Switch cameras on a key press (you can customize this part)
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Toggle camera activation
        thirdPersonCamera.gameObject.SetActive(!thirdPersonCamera.gameObject.activeSelf);
        firstPersonCamera.gameObject.SetActive(!firstPersonCamera.gameObject.activeSelf);
    }
}
