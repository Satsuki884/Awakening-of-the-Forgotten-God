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
        [SerializeField] private Button _levelUpButton;
        [SerializeField] private TMP_Text _characcterName;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _atk;
        [SerializeField] private TMP_Text _def;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Image _characterImage;

        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _levelUpButton.onClick.RemoveAllListeners();
            _levelUpButton.onClick.AddListener(LevelUp);

            _closeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void Initialize(CharacterDataWrapper character)
        {

            _characcterName.text = character.CharacterName;
            _hp.text = character.Health.ToString();
            _atk.text = character.Atk.ToString();
            _def.text = character.Def.ToString();
            _speed.text = character.Speed.ToString();
            _level.text = "Level 1";
            _characterImage.sprite = character.Icon;
        }

        private void LevelUp()
        {
            Debug.Log("Level Up");
        }
    }

}
