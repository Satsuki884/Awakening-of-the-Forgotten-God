using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AFG.MVP
{
    public class CharacterItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textName;

        public void Initialize(string name)
        {
            _textName.text = name;
        }
    }
}

