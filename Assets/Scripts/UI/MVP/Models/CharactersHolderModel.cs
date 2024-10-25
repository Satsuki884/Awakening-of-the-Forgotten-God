using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CharactersHolderModel : MonoBehaviour
    {
        public event Action OnStartCharacterSelection;
        public event Action OnStopCharacterSelection;
        
        public event Action OnCharactersUpdated;
        public List<CharacterDataWrapper> Characters { get; set; } =
            new List<CharacterDataWrapper>();
        
        private Transform _characterHolder;
        private GameObject _oldCharacter;
        
        public virtual void Start()
        {
            Characters = GameController.
                Instance.
                SaveManager.
                CharacterDataWrapperHolder.
                CharacterDataWrappers.
                ToList();
            
            OnCharactersUpdated?.Invoke();
        }
        
        public void StartCharacterSelection(Transform characterHolder)
        {
            _characterHolder = characterHolder;
            OnStartCharacterSelection?.Invoke();
        }
        
        public void StopCharacterSelection(string characterName)
        {
            if (_oldCharacter != null)
            {
                Destroy(_oldCharacter);
            }

            
            
            var character = Characters.Find(c => c.CharacterName.Equals(characterName));

            foreach (var bufCharacter in Characters)
            {
                Debug.Log(bufCharacter.CharacterName);
            }
            
            Debug.Log(characterName);
            Debug.Log(character);
            Debug.Log(character.CharacterPrefab);
            Debug.Log(character.CharacterPrefab.gameObject);
            Debug.Log(_characterHolder);
            
            _oldCharacter = Instantiate(
                character.CharacterPrefab.gameObject, 
                _characterHolder);
            
            OnStopCharacterSelection?.Invoke();
        }
    }
}
