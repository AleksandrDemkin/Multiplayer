using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    private GameObject _player;
    [SerializeField] private GameObject _playerPrefab;

    private void Start()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        _player = PhotonNetwork.Instantiate(_playerPrefab.name, Vector3.zero, Quaternion.identity);
    }

    public void DestroyPlayer()
    {
        PhotonNetwork.Destroy(_player.gameObject);
    }

}
