using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class PlayerCharacterItemsHolderPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _holder;
        [SerializeField] private CharacterItem _characterItem;
        
        private PlayerCharactersHolderModel _model;

        public void Awake()
        {
            _model = GameController.Instance.PlayerCharactersHolderModel;
            RefreshView();
        }

        private void OnEnable()
        {
            _model.OnCharactersUpdated += RefreshView;
            _model.OnStartCharacterSelection += Activate;
            _model.OnStopCharacterSelection += Deactivate;
        }

        private void OnDisable()
        {
            _model.OnCharactersUpdated -= RefreshView;
            _model.OnStartCharacterSelection -= Activate;
            _model.OnStopCharacterSelection -= Deactivate;
        }

        private void RefreshView()
        {
            //clear old items
            foreach (Transform child in _holder.transform)
            {
                Destroy(child.gameObject);
            }

            //fill holder with characters
            foreach (var character in _model.Characters)
            {
                var characterItem = Instantiate(_characterItem, _holder.transform);
                //TODO refactoring move name to json
                characterItem.Initialize(character.CharacterName, character.Icon, (characterName) =>
                {
                    _model.StopCharacterSelection(characterName);
                });
            }
            
            GameController.Instance.LevelModel.PlayerSquad = _model.Characters.ToArray();
        }

        private void Activate()
        {
            _holder.SetActive(true);
        }

        private void Deactivate()
        {
            _holder.SetActive(false);
        }
    }
}

