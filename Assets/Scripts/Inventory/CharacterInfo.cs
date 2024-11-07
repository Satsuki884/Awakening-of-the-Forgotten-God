using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{

    public class CharacterInfo : MonoBehaviour
    {
        [SerializeField] private CharacterInfoNextLevel _nextLevelInfo;
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private GameObject _levelUpPanel;
        [SerializeField] private TMP_Text _characcterName;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _atk;
        [SerializeField] private TMP_Text _def;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _characterImage;
        [SerializeField] private Button _closeButton;

        private PlayerDataWrapper PlayerData { get; set; }
        private List<CharacterDataWrapper> PlayerCharacters { get; set; } =
            new List<CharacterDataWrapper>();

        private void Start()
        {
            PlayerCharacters = GameController.Instance.SaveManager.PlayerCharacters;
            PlayerData = GameController.Instance.SaveManager.PlayerData;
            _levelUpButton.onClick.RemoveAllListeners();
            _levelUpButton.onClick.AddListener(LevelUp);
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private CharacterDataWrapper _character;

        public void Initialize(CharacterDataWrapper character)
        {
            _character = character;
            _characcterName.text = character.CharacterName;
            _hp.text = character.Health.ToString();
            _atk.text = character.Atk.ToString();
            _def.text = character.Def.ToString();
            _speed.text = character.Speed.ToString();
            _level.text = "Level " + character.Level;
            _characterImage.sprite = character.Icon;

            
        }

        private void LevelUp()
        {
            if (PlayerData.BooksData.BooksDataWrapper.BooksCount <= 0)
            {
                _nextLevelInfo.SetButtonUnEnable();
            }
            _nextLevelInfo.Initialize(PlayerCharacters, _character, PlayerData);
            _levelUpPanel.SetActive(true);
        }

       
        
    }

}
