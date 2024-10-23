using UnityEngine;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class PlayerCharacterItemsHolderPresenter : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup _holder;
        [SerializeField] private CharacterItem _characterItem;
        
        private PlayerCharactersHolderModel _model;

        public void Start()
        {
            _model = GameController.Instance.PlayerCharactersHolderModel;
            RefreshView();
        }

        private void OnEnable()
        {
            _model.OnCharacterAdded += OnCharacterAdded;
            _model.OnCharacterRemoved += OnCharacterRemoved;
        }

        private void OnDisable()
        {
            _model.OnCharacterAdded -= OnCharacterAdded;
            _model.OnCharacterRemoved -= OnCharacterRemoved;
        }

        private void OnCharacterAdded(CharacterController character)
        {
            RefreshView();
        }

        private void OnCharacterRemoved(CharacterController character)
        {
            RefreshView();
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
                characterItem.Initialize(character.name);
            }
        }
    }
}

