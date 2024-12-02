using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AFG
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip _backgroundMusic;
        [SerializeField] private AudioClip _buttonClick;
        [SerializeField] private AudioClip _levelVictory;
        [SerializeField] private AudioClip _levelUp;
        [SerializeField] private AudioClip _levelDefeat;
        [SerializeField] private AudioClip _combatMusic;
        [SerializeField] private AudioClip _saleMusic;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name != GameController.Instance.LevelModel.MenuSquadScene &&
                SceneManager.GetActiveScene().name != GameController.Instance.LevelModel.InventoryScene &&
                SceneManager.GetActiveScene().name != GameController.Instance.LevelModel.MainMenuScene)
            {
                _musicSource.clip = _combatMusic;
            }
            else
            {
                _musicSource.clip = _backgroundMusic;
            }
            _musicSource.Play();
        }

        public string ButtonClick = "buttonClick";
        public string LevelVictory = "levelVictory";
        public string LevelUp = "levelUp";
        public string LevelDefeat = "levelDefeat";
        public string SaleMusic = "saleMusic";

        public void PlaySFX(string clipName)
        {
            // Debug.Log("Playing SFX: " + clipName);
            switch (clipName)
            {
                case "buttonClick":
                    _sfxSource.PlayOneShot(_buttonClick);
                    break;
                case "levelVictory":
                    _sfxSource.PlayOneShot(_levelVictory);
                    break;
                case "levelUp":
                    _sfxSource.PlayOneShot(_levelUp);
                    break;
                case "levelDefeat":
                    _sfxSource.PlayOneShot(_levelDefeat);
                    break;
                case "saleMusic":
                    _sfxSource.PlayOneShot(_saleMusic);
                    break;
            }
        }
    }
}