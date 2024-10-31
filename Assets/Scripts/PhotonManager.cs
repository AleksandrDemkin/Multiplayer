using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _region;
    [SerializeField] private TMP_InputField _nickName;
    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private TMP_InputField _maxPlayers;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _joinCreateRandomRoomButton;
    [SerializeField] private Button _leaveRoomButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Transform _content;
    [SerializeField] private ListItem _listItemPrefab;
    [SerializeField] private AudioSource _mainMenuAudioSource;

    private List<RoomInfo> _allRoomsInfo = new List<RoomInfo>();

    private string _regionName = "ru";
    private string _gameScene = "GameScene";
    private string _joinRoomText = "Join room: ";
    private string _nickNameText = "TestNick";
    private string _roomNameText = "TestRoom";
    private string _maxPlayersText = "12";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _region.text = _regionName;
        _nickName.text = _nickNameText;
        _roomName.text = _roomNameText;
        _maxPlayers.text = _maxPlayersText;

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_region.text);

        _createRoomButton.onClick.AddListener(RoomCreate);
        _joinCreateRandomRoomButton.onClick.AddListener(RandomRoomJoinCreate);
        _leaveRoomButton.onClick.AddListener(RoomLeave);
        _quitButton.onClick.AddListener(GameQuit);

        _mainMenuAudioSource.Play();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected from the server.");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.NickName = _nickName.text == null ? "User" : _nickName.text;
        
        if (!PhotonNetwork.InLobby) { }
        PhotonNetwork.JoinLobby();
        Debug.Log($"Connected to: {PhotonNetwork.CloudRegion}");
    }

    private void RoomCreate()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = int.Parse(_maxPlayers.text);
        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        
        if (!PhotonNetwork.InRoom)
        {
            return;
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Created room, name: {PhotonNetwork.CurrentRoom.Name}");
        _roomName.text = _joinRoomText + PhotonNetwork.CurrentRoom.Name;
        OnRoomListUpdate(_allRoomsInfo);
    }

    public override void OnJoinedRoom()
    {
        SceneLoad();
        Debug.Log($"Joined room, name: {PhotonNetwork.CurrentRoom.Name}");
    }

    private void SceneLoad()
    {
        PhotonNetwork.LoadLevel(_gameScene);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room was not created, name: {PhotonNetwork.CurrentRoom.Name}");
    }

    private void RandomRoomJoinCreate()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
        SceneLoad();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            for (int i = 0; i < _allRoomsInfo.Count; i++)
            {
                if (_allRoomsInfo[i].masterClientId == info.masterClientId)
                {
                    return;
                }
            }

            ListItem listItem = Instantiate(_listItemPrefab, _content);

            if (listItem != null)
            {
                listItem.SetRooInfo(info);
                _allRoomsInfo.Add(info);
            }
        }
    }

    private void RoomLeave()
    {
        PhotonNetwork.LeaveRoom();
    }

    private void GameQuit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        _createRoomButton.onClick.RemoveAllListeners();
        _joinCreateRandomRoomButton.onClick.RemoveAllListeners();
        _leaveRoomButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }
}
