using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu (fileName = "CharacterDataHolder", menuName = "Configs/new CharacterDataHolder")]
    public class CharacterDataHolder : ScriptableObject
    {
        [SerializeField] private CharacterData[] characterData;
        public CharacterData[] CharacterData => characterData;
    }
}
