using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    public class PlayerCharacterItemsHolderPresenterInventory : MonoBehaviour
    {
        [SerializeField] private CharacterHolderItemInventory _characterItem;
        [SerializeField] private GameObject _holder;

        [SerializeField] private CharacterInfo _characterInfo;

        [SerializeField] private Transform _inventoryItemsContainer;

        private float _itemSpacing = .5f;
        private float _itemHeight;

        private List<CharacterDataWrapper> PlayerCharacters { get; set; } =
            new List<CharacterDataWrapper>();

        public void Start()
        {
            PlayerCharacters = GameController.Instance.SaveManager.PlayerCharacters;
            ShowAllPlayersCharacter();
        }

        private void ShowAllPlayersCharacter()
        {
            _itemHeight = _characterItem.GetComponent<RectTransform>().sizeDelta.y;
            
            foreach (var character in PlayerCharacters)
            {
                var characterItem = Instantiate(_characterItem, _holder.transform);
                characterItem.Initialize(character, (characterName) =>
                {
                    SelectCharacter(character);
                });
            }

            _inventoryItemsContainer.GetComponent<RectTransform>().sizeDelta =
                    Vector3.up * (_itemSpacing + _itemHeight * ((PlayerCharacters.Count - 1)/2));

        }

        private void SelectCharacter(CharacterDataWrapper character)
        {
            _characterInfo.gameObject.SetActive(true);
            _characterInfo.Initialize(character);
        }
    }
}

