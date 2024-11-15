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

        SetBetweenSession();
        AddEventListeners();
        CloseSettings();
    }

    public void AddEventListeners()
    {
        _openSettingsButton.onClick.RemoveAllListeners();
        _closeSettingsButton.onClick.RemoveAllListeners();
        _volumeSlider.onValueChanged.RemoveAllListeners();
        _openSettingsButton.onClick.AddListener(OpenSettings);
        _closeSettingsButton.onClick.AddListener(CloseSettings);
        
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        
        _qualityDropdown.onValueChanged.AddListener(SetQuality);
        _qualityDropdown.onValueChanged.AddListener(SetQuality);
        
        _fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetBetweenSession()
    {
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            int savedQualityLevel = PlayerPrefs.GetInt("QualityLevel");
            _qualityDropdown.value = savedQualityLevel;
            QualitySettings.SetQualityLevel(savedQualityLevel);
        }
        if (PlayerPrefs.HasKey("FullScreen"))
        {
            bool isFullScreen = PlayerPrefs.GetInt("FullScreen") == 1;
            _fullScreenToggle.isOn = isFullScreen;
            Screen.fullScreen = isFullScreen;
        }
        if (PlayerPrefs.HasKey("Resolution"))
        {
            int savedResolutionIndex = PlayerPrefs.GetInt("Resolution");
            _resolutionDropdown.value = savedResolutionIndex;
            Resolution savedResolution = resolutions[savedResolutionIndex];
            Screen.SetResolution(savedResolution.width, savedResolution.height, Screen.fullScreen);
        }
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            _volumeSlider.value = savedVolume;
            _audioMixer.SetFloat("Volume", savedVolume);
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
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private AudioMixer _audioMixer;

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        _audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    [SerializeField] private TMP_Dropdown _qualityDropdown;

    public void SetQuality(int qualityLevel)
    {
        Debug.Log(qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);
        PlayerPrefs.SetInt("QualityLevel", qualityLevel);
        PlayerPrefs.Save();
    }

    [SerializeField] private Toggle _fullScreenToggle;

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("FullScreen", isFullScreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    public void SetResolution(int resolutionIndex)
    {
        Debug.Log(resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
        PlayerPrefs.Save();
    }
}
