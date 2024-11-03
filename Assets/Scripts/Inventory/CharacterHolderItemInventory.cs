using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{
    public class CharacterHolderItemInventory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private Button _button;
        [SerializeField] private Image _characterImage;

        public void Initialize(CharacterDataWrapper character,
        Action<string> onCharacterSelected)
        {
            _characterImage.sprite = character.Icon;
            _textName.text = character.CharacterName;
            _button.onClick.AddListener(() => onCharacterSelected?.Invoke(name));
        }
    }
}

