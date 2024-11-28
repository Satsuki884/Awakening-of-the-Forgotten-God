using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingController : MonoBehaviour
{
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private Button _openSettingsButton;
    [SerializeField] private Button _closeSettingsButton;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
            resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        AddEventListeners();
        SetBetweenSession();
        CloseSettings();
    }

    public void AddEventListeners()
    {
        _openSettingsButton.onClick.RemoveAllListeners();
        _closeSettingsButton.onClick.RemoveAllListeners();
        _volumeSliderMusic.onValueChanged.RemoveAllListeners();
        _volumeSliderSFX.onValueChanged.RemoveAllListeners();
        _openSettingsButton.onClick.AddListener(OpenSettings);
        _closeSettingsButton.onClick.AddListener(CloseSettings);
        
        _volumeSliderMusic.onValueChanged.AddListener(SetVolumeMusic);
        _volumeSliderSFX.onValueChanged.AddListener(SetVolumeSFP);
        
        _qualityDropdown.onValueChanged.AddListener(SetQuality);
        _qualityDropdown.onValueChanged.AddListener(SetQuality);
        
        _fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private string QualityLevel = "QualityLevel";
    private string FullScreen = "FullScreen";
    private string Resolution = "Resolution";
    private string Music = "Music";
    private string SFX = "SFX";

    public void SetBetweenSession()
    {
        if (PlayerPrefs.HasKey(QualityLevel))
        {
            int savedQualityLevel = PlayerPrefs.GetInt(QualityLevel);
            _qualityDropdown.value = savedQualityLevel;
            QualitySettings.SetQualityLevel(savedQualityLevel);
        }
        if (PlayerPrefs.HasKey(FullScreen))
        {
            bool isFullScreen = PlayerPrefs.GetInt(FullScreen) == 1;
            _fullScreenToggle.isOn = isFullScreen;
            Screen.fullScreen = isFullScreen;
        }
        if (PlayerPrefs.HasKey(Resolution))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt(Resolution);
            _resolutionDropdown.value = savedResolutionIndex;
            Resolution savedResolution = resolutions[savedResolutionIndex];
            Screen.SetResolution(savedResolution.width, savedResolution.height, Screen.fullScreen);
        }
        if (PlayerPrefs.HasKey(Music))
        {
            Debug.Log(Music);
            float savedVolume = PlayerPrefs.GetFloat(Music);
            _volumeSliderMusic.value = savedVolume;
            _audioMixer.SetFloat(Music, savedVolume);
        }
        if (PlayerPrefs.HasKey(SFX))
        {
            Debug.Log("VolumeSFX");
            float savedVolume = PlayerPrefs.GetFloat(SFX);
            _volumeSliderSFX.value = savedVolume;
            _audioMixer.SetFloat(SFX, savedVolume);
        }

    }

    private void OpenSettings()
    {
        _settingPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        _settingPanel.SetActive(false);
    }

    [Header("Audio")]
    [SerializeField] private Slider _volumeSliderMusic;
    [SerializeField] private Slider _volumeSliderSFX;
    [SerializeField] private AudioMixer _audioMixer;

    public void SetVolumeMusic(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat(Music, volume);
        PlayerPrefs.SetFloat(Music, volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeSFP(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat(SFX, volume);
        PlayerPrefs.SetFloat(SFX, volume);
        PlayerPrefs.Save();
    }

    [SerializeField] private TMP_Dropdown _qualityDropdown;

    public void SetQuality(int qualityLevel)
    {
        // Debug.Log(qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);
        PlayerPrefs.SetInt(QualityLevel, qualityLevel);
        PlayerPrefs.Save();
    }

    [SerializeField] private Toggle _fullScreenToggle;

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt(FullScreen, isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    public void SetResolution(int resolutionIndex)
    {
        Debug.Log(resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt(Resolution, resolutionIndex);
        PlayerPrefs.Save();
    }
}
