using AFG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


namespace AFG {
    public class ShopControllerUI : MonoBehaviour
    {
        [SerializeField] private float _itemSpacing = .5f;


        [SerializeField] private GameObject _shopPanel;
        [SerializeField] private Transform _shopMenu;
        [SerializeField] private Transform _shopItemsContainer;
        [SerializeField] private GameObject _itemPrefab;
        private float _itemHeight;

        [SerializeField] public List<CharacterDataWrapper> Characters { get; set; } = 
            new List<CharacterDataWrapper>();
        [SerializeField] public List<CharacterDataWrapper> PlayerCharacters { get; set; } = 
            new List<CharacterDataWrapper>();


        //[Header("Shop Events")]
        [SerializeField] GameObject shopUI;
        [SerializeField] Button openShop;
        [SerializeField] Button closeShop;

        // Start is called before the first frame update
        void Start()
        {
            AddShopEvents();
            GenerateShopItemsUI();
            Characters = GameController.
                   Instance.
                   SaveManager.
                   CharacterDataWrapperHolder.
                   CharacterDataWrappers.
                   ToList();

            PlayerCharacters = GameController.
                Instance.
                SaveManager.LoadPlayerCharacterNames().
                characterDataWrappers.
                ToList();
        }

        private void GenerateShopItemsUI()
        {
            _itemHeight = _shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            Destroy(_shopItemsContainer.GetChild(0).gameObject);
            _shopItemsContainer.DetachChildren();

            for (int i = 0; i < Characters.Count; i++)
            {
                for (int j = 0; j < PlayerCharacters.Count; j++)
                {
                    CharacterShopItemUI itemUI = Instantiate(_itemPrefab, _shopItemsContainer).GetComponent<CharacterShopItemUI>();

                    itemUI.SetItemPosition(Vector3.down * i * (_itemHeight + _itemSpacing));
                    itemUI.gameObject.name = "Item " + i + " - " + Characters[i].CharacterName;
                    itemUI.SetCharacterName(Characters[i].CharacterName);
                    itemUI.SetCharacterAtk(Characters[i].CharacterPrefab.Atk);
                    itemUI.SetCharacterHp(Characters[i].CharacterPrefab.Health);
                    itemUI.SetCharacterDef(Characters[i].CharacterPrefab.Def);
                    itemUI.SetCharacterSpeed(Characters[i].CharacterPrefab.Speed);
                    itemUI.SetCharacterPrice(Characters[i].CharacterPrefab.Speed);

                    if (Characters[i].CharacterName == PlayerCharacters[j].CharacterName)
                    {
                        itemUI.SetCharacterAsPurchased();
                        itemUI.OnItemSelect(i, OnItemSelected);
                    }
                    else
                    {
                        itemUI.SetCharacterPrice(Characters[i].CharacterPrefab.Speed);
                        itemUI.OnItemPurchase(i, OnItemPurchased);
                    }
                }
            }
        }

        void OnItemSelected(int index)
        {
            Debug.Log("select" + index);
        }

        void OnItemPurchased(int index)
        {
            Debug.Log("purchase" + index);
        }

        void AddShopEvents()
        {
            openShop.onClick.RemoveAllListeners();
            openShop.onClick.AddListener(OpenShop);


            closeShop.onClick.RemoveAllListeners();
            closeShop.onClick.AddListener(CloseShop);
        }

        void OpenShop()
        {
            shopUI.SetActive(true);
        }

        void CloseShop()
        {
            shopUI.SetActive(false);
        }
    }
}