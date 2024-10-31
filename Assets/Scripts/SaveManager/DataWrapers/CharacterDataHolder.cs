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
        [SerializeField] private List<CharacterData> characterData;

        public List<CharacterData> CharacterData
        {
            get=> characterData;
            set => characterData = value;
        }
        
        [Button("MyCustomMethod")]
        public void MyCustomMethod()
        {
            Debug.Log("Call MyCustomMethod");
        }
    }
}
