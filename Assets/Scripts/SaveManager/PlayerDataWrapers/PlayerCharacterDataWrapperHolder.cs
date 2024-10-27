using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu (fileName = "PlayerCharacterDataWrapperHolder", menuName = "Configs/new PlayerCharacterDataWrapperHolder")]
    public class PlayerCharacterDataWrapperHolder : ScriptableObject
    {
        [FormerlySerializedAs("_playerCharacterWrapperEntities")] [SerializeField] private PlayerCharacterDataWrapper[] playerCharacterDataWrappers;
        public PlayerCharacterDataWrapper[] PlayerCharacterDataWrappers => playerCharacterDataWrappers;
    }

    [System.Serializable]
    public class PlayerCharacterDataWrapper
    {
        [SerializeField] private string _characterName;
        public string CharacterName => _characterName;
        
        [SerializeField] private CharacterController _characterPrefab;
        public CharacterController CharacterPrefab=> _characterPrefab;
        
        public void SetCharacterPrefab(CharacterController characterPrefab)
        {
            _characterPrefab = characterPrefab;
        }
    }
}
