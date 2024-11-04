using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{

    public class CharacterInfoNextLevel : MonoBehaviour
    {
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private TMP_Text _characcterName;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _atk;
        [SerializeField] private TMP_Text _def;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _level;

        [SerializeField] private Button _closeButton;

        [SerializeField] private BooksCoinController _booksCoinController;

        [SerializeField] private GameObject _booksBuyPanel;

        private List<CharacterDataWrapper> Characters { get; set; } =
            new List<CharacterDataWrapper>();

        private void Start()
        {
            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Initialize(List<CharacterDataWrapper> playerCharacters,
        CharacterDataWrapper character, PlayerDataWrapper playerData)
        {

            _characcterName.text = character.CharacterName;
            _hp.text = character.Health.ToString() + " -> " +
            character.CharacterStatsData[character.Level + 1].Health.ToString();
            _atk.text = character.Atk.ToString() + " -> " +
            character.CharacterStatsData[character.Level + 1].Atk.ToString();
            _def.text = character.Def.ToString() + " -> " +
            character.CharacterStatsData[character.Level + 1].Def.ToString();
            _speed.text = character.Speed.ToString() + " -> " +
            character.CharacterStatsData[character.Level + 1].Speed.ToString();
            _level.text = "Level " + character.Level.ToString() + " -> " +
            (character.Level + 1).ToString();

            _levelUpButton.onClick.RemoveAllListeners();
            _levelUpButton.onClick.AddListener(() => LevelUp(playerCharacters, character, playerData));
        }

        private Color _initialColor;
        private Color _initialColorText;
        public void SetButtonUnEnable()
        {
            _levelUpButton.enabled = false;
            _initialColor = _levelUpButton.GetComponent<Image>().color;
            _levelUpButton.GetComponent<Image>().color = Color.gray;
            TMP_Text buttonText = _levelUpButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                _initialColorText = buttonText.color;
                buttonText.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }

        [SerializeField] private CharacterInfo _characterInfo;

        private void LevelUp(List<CharacterDataWrapper> playerCharacters,
        CharacterDataWrapper character, PlayerDataWrapper playerData)
        {
            playerData.BooksData.BooksDataWrapper.BooksCount -= 1;

            if (_booksCoinController != null)
            {
                GameController.
                Instance.
                SaveManager.
                SavePlayerData(playerData);
                _booksCoinController.Refresh();
            }

            character.Level++;

            for (int i = 0; i < playerCharacters.Count; i++)
            {
                if (playerCharacters[i].CharacterName == character.CharacterName)
                {
                    playerCharacters[i].Level = character.Level;
                    break;
                }
            }

            Debug.Log("Level up character: " + character.CharacterName);
            foreach (var item in playerCharacters)
            {
                Debug.Log(item.CharacterName + " " + item.Level);
            }

            GameController.Instance.SaveManager.LevelUpCharacter(playerCharacters, character);

            _characterInfo.Initialize(character);
            _booksBuyPanel.SetActive(false);
        }
    }

}
