using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0.0f, 1.8f, 3.0f);
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.position = _target.position + _offset;
    }
}
