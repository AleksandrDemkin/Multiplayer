using UnityEngine;
using Photon.Pun;

public class CharacterControllerMove : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// MovePlayer
    /// </summary>
    [SerializeField] private CharacterController _characterController;

    private Vector3 _velocity;
    private Vector3 _moveVector;
    private Vector3 _playerInput;
    private bool _jumpInput;
    private bool _accelerationInput;
    private bool _slowdownInput;
    private float _speed = 4f;
    private float _acceleration = 3f;
    private float _slowdown = 2f;
    private float _jumpForce = 10f;
    private float _gravity = -9.8f;
    private float _constantGravity = -2f;
    private float _zRotation;

    /// <summary>
    /// MoveCamera
    /// </summary>
    //[SerializeField] private Transform _cameraTransform;

    private Vector2 _mouseInput;
    private float _xRotation;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _sensitivity = 3.0f;
    [Range(-90.0f, .0f)]
    [SerializeField] private float _minVert = -60.0f;
    [Range(0.0f, 90.0f)]
    [SerializeField] private float _maxVert = 60.0f;


    private void Start()
    {
        //GetCharacterController();
        //LockCursor();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            GetPlayerInput();
            GetMouseInput();
            MovePlayer();
            MoveCamera();
        }
    }

    private void GetPlayerInput()
    {
        _playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _jumpInput = Input.GetKey(KeyCode.Space);
        _accelerationInput = Input.GetKey(KeyCode.LeftShift);
        _slowdownInput = Input.GetKey(KeyCode.LeftControl);
    }

    private void GetMouseInput()
    {
        _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    private void MovePlayer()
    {
        _moveVector = transform.TransformDirection(_playerInput);

        if (_characterController.isGrounded)
        {
            _velocity.y = _gravity;

            if (_jumpInput)
            {
                Jump();
            }
            if (_accelerationInput)
            {
                Acceleration();
            }
            if (_slowdownInput)
            {
                Slowdown();
            }
        }
        else
        {
            _velocity.y -= _gravity * _constantGravity * Time.deltaTime;
        }

        _characterController.Move(_moveVector * _speed * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
    }


    private void MoveCamera()
    {
        _xRotation -= _mouseInput.y * _sensitivity;
        _xRotation = Mathf.Clamp(_xRotation, _minVert, _maxVert);
        transform.Rotate(0f, _mouseInput.x * _sensitivity, 0f);
        //_cameraTransform.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    private static void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Jump()
    {
        _velocity.y = _jumpForce;
    }

    private void Acceleration()
    {
        _moveVector *= _acceleration;
    }

    private void Slowdown()
    {
        _moveVector /= _slowdown;
    }

    private void GetCharacterController()
    {
        _characterController = GetComponent<CharacterController>();
    }
}
