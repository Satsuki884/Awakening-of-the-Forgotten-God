using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu (fileName = "CharacterDataWrapperHolder", menuName = "Configs/new CharacterDataWrapperHolder")]
    public class CharacterDataWrapperHolder : ScriptableObject
    {
        [FormerlySerializedAs("_characterWrapperEntities")] [SerializeField] private CharacterDataWrapper[] characterDataWrappers;
        public CharacterDataWrapper[] CharacterDataWrappers => characterDataWrappers;
    }

    [System.Serializable]
    public class CharacterDataWrapper
    {
        [SerializeField] private string _characterName;
        public string CharacterName => _characterName;
        
        [SerializeField] private CharacterController _characterPrefab;
        public CharacterController CharacterPrefab=> _characterPrefab;
    }
}
