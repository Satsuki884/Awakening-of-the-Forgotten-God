using AFG;
using AFG.Character;
using AFG.Stats;
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


        //[SerializeField] private GameObject _shopPanel;
        [SerializeField] private Transform _shopMenu;
        [SerializeField] private Transform _shopItemsContainer;
        [SerializeField] private GameObject _itemPrefab;
        private float _itemHeight;

        [SerializeField] public List<CharacterDataWrapper> Characters { get; set; } = 
            new List<CharacterDataWrapper>();
        [SerializeField] public List<CharacterDataWrapper> PlayerCharacters { get; set; } = 
            new List<CharacterDataWrapper>();

        [SerializeField] private CharacterDataWrapperHolder playerCharacterDataWrapperHolder;


        //[Header("Shop Events")]
        [SerializeField] GameObject shopUI;
        [SerializeField] Button openShop;
        [SerializeField] Button closeShop;

        // Start is called before the first frame update
        void Start()
        {
            AddShopEvents();
            
            Characters = GameController.
                   Instance.
                   SaveManager.
                   CharacterDataWrapperHolder.
                   CharacterDataWrappers.
                   ToList();

            //Debug.Log(Characters[0].CharacterPrefab.Atk);

            

            PlayerCharacters = GameController.
                Instance.
                SaveManager.LoadPlayerCharacterNames().
                characterDataWrappers.
                ToList();

            Debug.Log(PlayerCharacters[0].CharacterName);

            GenerateShopItemsUI();
        }

        private CharacterStats _characterStats;

        private void GenerateShopItemsUI()
        {
            _itemHeight = _shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            Destroy(_shopItemsContainer.GetChild(0).gameObject);
            _shopItemsContainer.DetachChildren();
            //Debug.Log("Character Count\t" + Characters.Count);
            //Debug.Log("Player Character Count\t" + PlayerCharacters.Count);

            for (int i = 0; i < Characters.Count; i++)
            {
                for (int j = 0; j < PlayerCharacters.Count; j++)
                {

                    _characterStats = Characters[i].CharacterPrefab.GetComponentInChildren<CharacterStats>();
                    Debug.Log(_characterStats);

                    CharacterShopItemUI itemUI = Instantiate(_itemPrefab, _shopItemsContainer).GetComponent<CharacterShopItemUI>();

                    itemUI.SetItemPosition(Vector3.down * i * (_itemHeight + _itemSpacing));
                    itemUI.gameObject.name = "Item " + i + " - " + Characters[i].CharacterName;
                    itemUI.SetCharacterName(Characters[i].CharacterName);
                    itemUI.SetCharacterAtk(_characterStats.Atk);
                    itemUI.SetCharacterHp(_characterStats.Health);
                    itemUI.SetCharacterDef(_characterStats.Def);
                    itemUI.SetCharacterSpeed(_characterStats.Speed);
                    itemUI.SetCharacterPrice(_characterStats.Speed);

                    if (Characters[i].CharacterName == PlayerCharacters[j].CharacterName)
                    {
                        itemUI.SetCharacterAsPurchased();
                        itemUI.OnItemSelect(i, OnItemSelected);
                    }
                    else
                    {
                        itemUI.SetCharacterPrice(_characterStats.Speed);
                        itemUI.OnItemPurchase(i, OnItemPurchased);
                    }
                    _shopItemsContainer.GetComponent<RectTransform>().sizeDelta = 
                        Vector3.up*((_itemHeight+_itemSpacing)+Characters.Count + _itemSpacing);
                }
            }
        }

        void OnItemSelected(int index)
        {
            Debug.Log("select" + index);
        }

        void OnItemPurchased(int index, CharacterDataWrapper character)
        {
            Debug.Log("purchase" + index);
            PlayerCharacters.Add(character);
            GameController.
                Instance.
                SaveManager.
                SavePurchaseCharacters(PlayerCharacters);
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