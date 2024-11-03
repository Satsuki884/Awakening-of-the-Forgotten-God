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

        private List<CharacterDataWrapper> PlayerCharacters { get; set; } =
            new List<CharacterDataWrapper>();

        public void Start()
        {
            PlayerCharacters = GameController.Instance.SaveManager.PlayerCharacters;
            ShowAllPlayersCharacter();
        }

        private void ShowAllPlayersCharacter()
        {
            var characterItem = Instantiate(_characterItem, _holder.transform);
            foreach (var character in PlayerCharacters)
            {
                characterItem.Initialize(character, (characterName) =>
                {
                    SelectCharacter(character);
                });
            }

        }

        private void SelectCharacter(CharacterDataWrapper character)
        {
            _characterInfo.gameObject.SetActive(true);
            _characterInfo.Initialize(character);
        }
    }
}

