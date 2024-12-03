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

        //private Transform[] _characterHolders;
        private GameObject _oldCharacter;

        private Transform _characterHolder;// = new Transform[3];  // ������ �������� ��� ������ ������
        private GameObject[] _selectedCharacters = new GameObject[3];

        public List<CharacterDataWrapper> SelectedCharacters{ get; set; } = new List<CharacterDataWrapper>();

        public virtual void Start()
        {
            var saveManager = GameController.Instance.SaveManager;
            
            Characters = saveManager.AllCharacters;

            UpdateCharacters();
        }

        private int _buttonIndex;// = 0;

        public void StartCharacterSelection(Transform characterHolder, int buttonIndex)
        {
            //Debug.Log(buttonIndex);
            _characterHolder = characterHolder;
            _buttonIndex = buttonIndex;
            OnStartCharacterSelection?.Invoke();
        }

        protected void UpdateCharacters()
        {
            OnCharactersUpdated?.Invoke();
        }
        
        public void StopCharacterSelection(string characterName)
        {
            if (_selectedCharacters[_buttonIndex] != null)
            {
                Destroy(_selectedCharacters[_buttonIndex]);
            }

            var character = Characters.Find(c => c.CharacterName.Equals(characterName));

            _selectedCharacters[_buttonIndex] = Instantiate(character.CharacterPrefab.gameObject, _characterHolder);
            SelectedCharacters.Add(character);

            _selectedCharacters[_buttonIndex].transform.rotation = Quaternion.Euler(0, 90, 0);

            for (int i = 0; i < _selectedCharacters.Length; i++)
            {
                if (_selectedCharacters[i] != null)
                {
                    Debug.Log(_selectedCharacters[i].name);
                }
                if (i != _buttonIndex && _selectedCharacters[i] != null &&
                    _selectedCharacters[i].name == _selectedCharacters[_buttonIndex].name)
                {
                    Transform parentHolder = _selectedCharacters[i].transform.parent;

                    foreach (Transform child in parentHolder)
                    {
                        if (child.gameObject == _selectedCharacters[i])
                        {
                            Destroy(child.gameObject);
                            _selectedCharacters[i] = null;
                            break;
                        }
                    }
                }
            }


            OnStopCharacterSelection?.Invoke();
        }
    }
}
