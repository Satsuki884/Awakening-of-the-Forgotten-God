using AFG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu (fileName = "CharacterData", menuName = "Configs/new CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterDataWrapper _characterDataWrapper;
        public CharacterDataWrapper CharacterDataWrapper => _characterDataWrapper;
    }

    [System.Serializable]
    public class CharacterDataWrapper
    {
        [SerializeField] private string _characterName;
        public string CharacterName => _characterName;

        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField] private float _health;
        public float Health => _health;

        [SerializeField] private float _def;
        public float Def => _def;

        [SerializeField] private float _speed;
        public float Speed => _speed;

        [SerializeField] private float _atk;
        public float Atk => _atk;

        [SerializeField] private float _price;
        public float Price => _price;

        [SerializeField] private CharacterController _characterPrefab;
        public CharacterController CharacterPrefab=> _characterPrefab;
        
        public void SetCharacterPrefab(CharacterController characterPrefab)
        {
            _characterPrefab = characterPrefab;
        }
    }
}

