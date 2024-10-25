using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG.MVP
{
    public class CharacterItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private Button _button;
        
        public void Initialize(string name, Action<string> onCharacterSelected)
        {
            _textName.text = name;
            _button.onClick.AddListener(() => onCharacterSelected?.Invoke(name));
        }
    }
}

