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
    [SerializeField] private Slider _volumeSlider;

    [SerializeField] private AudioMixer _audioMixer;

    public void SetVolume()
    {
        Debug.Log(_volumeSlider.value);
        _audioMixer.SetFloat("Volume", _volumeSlider.value);
    }

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
        CloseSettings();
    }

    public void AddEventListeners()
    {
        _openSettingsButton.onClick.RemoveAllListeners();
        _closeSettingsButton.onClick.RemoveAllListeners();
        _volumeSlider.onValueChanged.RemoveAllListeners();
        _openSettingsButton.onClick.AddListener(OpenSettings);
        _closeSettingsButton.onClick.AddListener(CloseSettings);
        _volumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
        _qualityDropdown.onValueChanged.AddListener(delegate { SetQuality(); });
        _fullScreenToggle.onValueChanged.AddListener(delegate { SetFullScreen(); });
        _resolutionDropdown.onValueChanged.AddListener(delegate { SetResolution(); });
    }

    private void OpenSettings()
    {
        _settingPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        _settingPanel.SetActive(false);
    }

    [SerializeField] private TMP_Dropdown _qualityDropdown;

    public void SetQuality()
    {
        Debug.Log(_qualityDropdown.value);
        QualitySettings.SetQualityLevel(_qualityDropdown.value);
    }

    [SerializeField] private Toggle _fullScreenToggle;

    public void SetFullScreen()
    {
        Screen.fullScreen = _fullScreenToggle.isOn;
    }

    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    public void SetResolution()
    {
        Debug.Log(_resolutionDropdown.value);
        Resolution resolution = resolutions[_resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
