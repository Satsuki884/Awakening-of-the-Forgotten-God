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

        [SerializeField] private int _level;
        public int Level => _level;
        
        [SerializeField] private CharacterStatsData[] _characterStatsData;
        public CharacterStatsData[] CharacterStatsData => _characterStatsData;
        
        //TODO add validation for level
        public float Health => CharacterStatsData[Level].Health;
        public float Def => CharacterStatsData[Level].Def;
        public float Speed => CharacterStatsData[Level].Speed;
        public float Atk => CharacterStatsData[Level].Atk;
        
        [SerializeField] private float _price;
        public float Price => _price;

        [SerializeField] private CharacterController _characterPrefab;
        public CharacterController CharacterPrefab=> _characterPrefab;
        
        public void SetCharacterPrefab(CharacterController characterPrefab)
        {
            _characterPrefab = characterPrefab;
        }
    }

    [System.Serializable]
    public class CharacterStatsData
    {
        [SerializeField] private float _health;
        public float Health => _health;

        [SerializeField] private float _def;
        public float Def => _def;

        [SerializeField] private float _speed;
        public float Speed => _speed;

        [SerializeField] private float _atk;
        public float Atk => _atk;
    }
}

