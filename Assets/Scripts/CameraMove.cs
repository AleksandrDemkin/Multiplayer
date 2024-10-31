using UnityEngine;
using Photon.Pun;

public class CameraMove : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _player;

    private float _mouseX;
    private float _mouseY;
    private float _sensitivity = 200f;

    private void Start()
    {
        CursorLocked();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CursorUnlocked();
        }


    }

    private void MouseInput()
    {
        _mouseX = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
    }


    #region CursorLocked

    private void CursorLocked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CursorUnlocked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    # endregion
}