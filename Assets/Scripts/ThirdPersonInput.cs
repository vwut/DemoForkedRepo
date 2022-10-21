using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonInput : MonoBehaviour
{

    public InputActionReference cameraToggle;
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject Crosshair;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (cameraToggle.action.WasPressedThisFrame() && mainCamera.activeInHierarchy) {
            aimCamera.SetActive(true);
            Crosshair.SetActive(true);
            mainCamera.SetActive(false);
        } else if (cameraToggle.action.WasPressedThisFrame() && aimCamera.activeInHierarchy) {
            mainCamera.SetActive(true);
            Crosshair.SetActive(false);
            aimCamera.SetActive(false);
        }
    }
}
