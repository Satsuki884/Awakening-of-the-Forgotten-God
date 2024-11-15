using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _levelVictory;
    [SerializeField] private AudioClip _levelDefeat;
    [SerializeField] private AudioClip _combatMusic;

    private void Start()
    {
        _musicSource.clip = _backgroundMusic;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
}
