using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Unity.VisualScripting;

public class ListItem : MonoBehaviour
{

    [SerializeField] private TMP_Text _textPlayersCount;
    [SerializeField] private TMP_Text _roomInfoName;
    [SerializeField] private Button _showChatButton;

    private void Start()
    {
        _showChatButton.onClick.AddListener(JoinToListRoom);
    }

    public void SetRooInfo(RoomInfo roomInfo)
    {
        _roomInfoName.text = roomInfo.Name;
        _textPlayersCount.text = $"{roomInfo.PlayerCount} / { roomInfo.MaxPlayers}";
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(_roomInfoName.text);
    }

    private void OnDestroy()
    {
        _showChatButton.onClick.RemoveAllListeners();
    }
}
