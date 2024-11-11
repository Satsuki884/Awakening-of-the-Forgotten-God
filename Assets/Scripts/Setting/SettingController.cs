using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    private void Start()
    {
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
    }

    private void OpenSettings()
    {
        _settingPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        _settingPanel.SetActive(false);
    }
}
