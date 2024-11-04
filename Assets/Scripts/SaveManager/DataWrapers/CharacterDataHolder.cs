using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu (fileName = "CharacterDataHolder", menuName = "Configs/new CharacterDataHolder")]
    public class CharacterDataHolder : ScriptableObject
    {
        [SerializeField] private bool _isPlayerCharacter;
        [SerializeField] private List<CharacterData> characterData;

        
        public List<CharacterData> CharacterData
        {
            get=> characterData;
            set => characterData = value;
        }
        
        [Button("SynchronizeFileData")]
        public void SynchronizeFileData()
        {
            var path = _isPlayerCharacter ? 
                SaveManager.FilePathToPlayerCharacters : 
                SaveManager.FilePathToAllCharacters;

            var characters = 
                characterData.ConvertAll(character => character.CharacterDataWrapper);
            
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string json = JsonUtility.ToJson(new CharactersDataWrapper
            {
                characterDataWrappers = characters
            }, true);

            File.WriteAllText(path, json);
            
            Debug.Log("Synchronize File Data " + path);
        }
    }
}
