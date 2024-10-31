using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private Canvas _chatCanvas;
    [SerializeField] private TMP_Text _lastMessageText;
    [SerializeField] private TMP_InputField _messageInputText;
    [SerializeField] private Button _sendMessageButton;
    [SerializeField] private Button _showChatButton;
    [SerializeField] private Button _hideChatButton;
    [SerializeField] private AudioSource _buttonClickAudioSource;
    [SerializeField] private AudioSource _gameMusicAudioSource;

    private PhotonView _photonView;

    private void Start()
    {
        _chatCanvas.gameObject.SetActive(false);
        _photonView = GetComponent<PhotonView>();

        _sendMessageButton.onClick.AddListener(SendMessageButton);
        _showChatButton.onClick.AddListener(ShowChatButton);
        _hideChatButton.onClick.AddListener(HideChatButton);

        GameMusicPlay();
    }

    public void SendMessageButton()
    {
        _photonView.RPC("SendMessage", RpcTarget.AllBuffered, PhotonNetwork.NickName, _messageInputText.text);
        ButtonClick();
    }

    public void ShowChatButton()
    {
        _chatCanvas.gameObject.SetActive(true);
        ButtonClick();
    }

    public void HideChatButton()
    {
        _chatCanvas.gameObject.SetActive(false);
        ButtonClick();
    }

    public void ButtonClick()
    {
        _buttonClickAudioSource.Play();
    }

    public void GameMusicPlay()
    {
        _gameMusicAudioSource.Play();
    }

    [PunRPC]
    private void SendMessage(string nick, string message)
    {
        _lastMessageText.text = nick + " : " + message;
    }

    private void OnDestroy()
    {
        _sendMessageButton.onClick.RemoveAllListeners();
        _showChatButton.onClick.RemoveAllListeners();
        _hideChatButton.onClick.RemoveAllListeners();
    }
}
