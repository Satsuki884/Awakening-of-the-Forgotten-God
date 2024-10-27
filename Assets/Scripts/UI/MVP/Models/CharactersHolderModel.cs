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

        private Transform _characterHolder;// = new Transform[3];  // Массив холдеров для каждой кнопки
        private GameObject[] _selectedCharacters = new GameObject[3];

        public virtual void Start()
        {
            Characters = GameController.
                Instance.
                SaveManager.
                CharacterDataWrapperHolder.
                CharacterDataWrappers.
                ToList();

            //_selectedCharacters = new GameObject[3];
            OnCharactersUpdated?.Invoke();
        }

        private int _buttonIndex;// = 0;

        public void StartCharacterSelection(Transform characterHolder, int buttonIndex)
        {
            //Debug.Log(buttonIndex);
            _characterHolder = characterHolder;
            _buttonIndex = buttonIndex;
            OnStartCharacterSelection?.Invoke();
        }

        public void StopCharacterSelection(string characterName)
        {
            if (_selectedCharacters[_buttonIndex] != null)
            {
                Destroy(_selectedCharacters[_buttonIndex]);
            }

            // Находим выбранного персонажа
            var character = Characters.Find(c => c.CharacterName.Equals(characterName));

            // Проверяем, не был ли выбранный персонаж уже привязан к другой кнопке
            /*for (int i = 0; i < _selectedCharacters.Length; i++)
            {
                if (i != _buttonIndex && _selectedCharacters[i] != null &&
                    _selectedCharacters[i].name == character.CharacterPrefab.gameObject.name)
                {
                    Destroy(_selectedCharacters[i]);
                    _selectedCharacters[i] = null;
                }
            }*/

            // Создаем персонажа возле соответствующей кнопки
            _selectedCharacters[_buttonIndex] = Instantiate(character.CharacterPrefab.gameObject, _characterHolder);

            _selectedCharacters[_buttonIndex].transform.rotation = Quaternion.Euler(0, 180, 0);

            

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
