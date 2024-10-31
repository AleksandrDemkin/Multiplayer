using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Photon.Pun;
using UnityEngine.Events;
using TMPro;

public class MainGameMenu : MonoBehaviour
{
    #region MenuCanvas
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _settingsMenuButton;
    [SerializeField] private Button _hideMenuButton;
    [SerializeField] private Canvas _menuCanvas;
    #endregion

    #region SettingsCanvas
    [SerializeField] private float _volume = 0;
    [SerializeField] private int _quality = 0;
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private bool _isFullscreen = false;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _buttonClickAudioSource;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Resolution[] _resolutions;
    [SerializeField] private int _currResolutionIndex = 0;
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Canvas _settingsCanvas;
    [SerializeField] private Button _saveSettingsButton;
    [SerializeField] private Button _cancelSaveSettingsButton;
    [SerializeField] private Button _backToMenuButton;
    #endregion

    private string _menuScene = "MenuScene";


    private void Start()
    {
        _mainMenuButton.onClick.AddListener(BackToMainMenu);
        _settingsMenuButton.onClick.AddListener(ShowSettingsCanvas);
        _backToMenuButton.onClick.AddListener(HideSettingsCanvas);
        _hideMenuButton.onClick.AddListener(HideMenuCanvas);
        _saveSettingsButton.onClick.AddListener(SaveSettings);
        _cancelSaveSettingsButton.onClick.AddListener(CancelSaveSettings);
        _settingsCanvas.gameObject.SetActive(false);
        ChangeVolume(_buttonClickAudioSource.volume);
        _fullScreenToggle.onValueChanged.AddListener(ChangeFullscreenMode);

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ShowMenuCanvas(_menuCanvas);
        }
    }
    
    private void ShowMenuCanvas(Canvas canvas)
    {
        _menuCanvas.gameObject.SetActive(true);
        ButtonClick();
    }

    private void HideMenuCanvas()
    {
        _menuCanvas.gameObject.SetActive(false);
        ButtonClick();
    }
    
    private void ShowSettingsCanvas()
    {
        _settingsCanvas.gameObject.SetActive(true);
        ButtonClick();
    }

    private void HideSettingsCanvas()
    {
        _settingsCanvas.gameObject.SetActive(false);
        ButtonClick();
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(_menuScene);
    }

    public void ButtonClick()
    {
        _buttonClickAudioSource.Play();
    }

    public void ChangeVolume(float val)
    {
        _volume = val;
    }

    public void ChangeResolution(int index)
    {
        _currResolutionIndex = index;
        _resolutionDropdown.ClearOptions();
        _resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].Equals(Screen.currentResolution))
            {
                _currResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void ChangeFullscreenMode(bool val)
    {
        _isFullscreen = val;
    }

    public void ChangeQuality(int index)
    {
        _quality = index;
    }

    public void SaveSettings()
    {
        ButtonClick();
        _audioMixer.SetFloat("Master", _volume);
        QualitySettings.SetQualityLevel(_quality);
        Screen.fullScreen = _isFullscreen;
        Screen.SetResolution(Screen.resolutions[_currResolutionIndex].width, Screen.resolutions[_currResolutionIndex].height, _isFullscreen);
    }

    public void CancelSaveSettings()
    {
        ButtonClick();
    }


    private void OnDestroy()
    {
        _mainMenuButton.onClick.RemoveAllListeners();
        _settingsMenuButton.onClick.RemoveAllListeners();
        _backToMenuButton.onClick.RemoveAllListeners();
        _hideMenuButton.onClick.RemoveAllListeners();
        _saveSettingsButton.onClick.RemoveAllListeners();
        _cancelSaveSettingsButton.onClick.RemoveAllListeners();
    }
}
