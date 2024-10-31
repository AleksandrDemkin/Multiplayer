using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class MyPlayerUi : MonoBehaviour
{
	[SerializeField]
	private TMP_Text playerNameText;
	[SerializeField]
	private Slider playerHealthSlider;
    private GameObject _playerUiCanvas;
    [SerializeField] private GameObject _playerUiCanvasPrefab;
    [SerializeField] private GameObject _myPlayerUiTarget;

    public float Health = 1f;
	private void Start()
    {
            playerNameText.text = PhotonNetwork.NickName;

            _playerUiCanvas = this.GetComponent<GameObject>();
            //this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            _playerUiCanvas = PhotonNetwork.Instantiate(_playerUiCanvasPrefab.name, _myPlayerUiTarget.gameObject.transform.position, Quaternion.identity);
        
        //if (_playerManager != null)
        //{
        //    MyPlayerUi _uiGo = GetComponent<MyPlayerUi>();
        //    _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        //}
        //else
        //{
        //    Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
        //}
    }

    private void Update()
	{
        // Reflect the Player Health
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = Health;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // We are only interested in Beamers
        // we should be using tags but for the sake of distribution, let's simply check by name.
        if (!other.name.Contains("Beam"))
        {
            return;
        }

        this.Health -= 0.1f;
    }

    /// <summary>
    /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
    /// We're going to affect health while the beams are interesting the player
    /// </summary>
    /// <param name="other">Other.</param>
    public void OnTriggerStay(Collider other)
    {

        // We are only interested in Beamers
        // we should be using tags but for the sake of distribution, let's simply check by name.
        if (!other.name.Contains("Beam"))
        {
            return;
        }

        // we slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
        this.Health -= 0.1f * Time.deltaTime;
    }

    public void DestroyPlayer()
    {
        PhotonNetwork.Destroy(_playerUiCanvas.gameObject);
    }
}
