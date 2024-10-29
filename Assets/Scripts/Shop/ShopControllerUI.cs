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
        //private float _playersMoney = 10;


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

        /*[SerializeField] private GameObject _itemBuyNow;
        [SerializeField] private Button _youSure;
        [SerializeField] private Button _notBuy;*/

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
           

            PlayerCharacters = GameController.
                Instance.
                SaveManager.LoadPlayerCharacterNames().
                characterDataWrappers.
                ToList();

            GenerateShopItemsUI();
        }

        private CharacterStats _characterStats;

        private void GenerateShopItemsUI()
        {
            

            _itemHeight = _shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            Destroy(_shopItemsContainer.GetChild(0).gameObject);
            _shopItemsContainer.DetachChildren();

            for (int i = 0; i < Characters.Count; i++)
            {
                _characterStats = Characters[i].CharacterPrefab.GetComponentInChildren<CharacterStats>();
                //Debug.Log(_characterStats);

                CharacterShopItemUI itemUI = Instantiate(_itemPrefab, _shopItemsContainer).GetComponent<CharacterShopItemUI>();

                itemUI.SetItemPosition(Vector3.down * i * (_itemHeight + _itemSpacing));
                itemUI.gameObject.name = "Item " + i + " - " + Characters[i].CharacterName;
                itemUI.SetCharacterName(Characters[i].CharacterName);
                itemUI.SetCharacterAtk(_characterStats.Atk);
                itemUI.SetCharacterHp(_characterStats.Health);
                itemUI.SetCharacterDef(_characterStats.Def);
                itemUI.SetCharacterSpeed(_characterStats.Speed);
                itemUI.SetCharacterPrice(_characterStats.Speed);

                //TODO info about user`s money
                /*if (100 > _playersMoney)
                {
                    itemUI.SetButtonUnEnable();
                } */
                

                for (int j = 0; j < PlayerCharacters.Count; j++)
                {
                    if (Characters[i].CharacterName == PlayerCharacters[j].CharacterName)
                    {
                        itemUI.SetSoldOut();
                    }
                }

                itemUI.OnItemPurchase(itemUI, Characters[i], OnItemPurchased);
                _shopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                    Vector3.up * ((_itemHeight + _itemSpacing) * (Characters.Count-1) + _itemSpacing);
            }
        }

        void OnItemPurchased(CharacterShopItemUI item,
            CharacterDataWrapper character,
            GameObject _itemBuyNow, GameObject _itemSoldOut)
        {

            BuyNow(_itemBuyNow, _itemSoldOut);

            item.ItemPurchaseConfirm(item, character, OnItemPurchasedConfirmed);
            item.DontBuy();

        }

        void OnItemPurchasedConfirmed(CharacterShopItemUI item, 
            CharacterDataWrapper character, 
            GameObject _itemBuyNow, GameObject _itemSoldOut)
        {
            PlayerCharacters.Add(character);
            GameController.
                Instance.
                SaveManager.
                SavePurchaseCharacters(PlayerCharacters);

            _itemBuyNow.SetActive(false);
            _itemSoldOut.SetActive(true);

            PlayerCharacters = GameController.
                Instance.
                SaveManager.LoadPlayerCharacterNames().
                characterDataWrappers.
                ToList();
            GenerateShopItemsUI();
        }

        void BuyNow(GameObject _itemBuyNow, GameObject _itemSoldOut)
        {
            Debug.Log("sdjkfljsd");
            _itemBuyNow.SetActive(true);
        }

        void NotBuy(GameObject _itemBuyNow, GameObject _itemSoldOut)
        {
            _itemBuyNow.SetActive(false);
        }

        void AddShopEvents()
        {
            openShop.onClick.RemoveAllListeners();
            openShop.onClick.AddListener(OpenShop);


            closeShop.onClick.RemoveAllListeners();
            closeShop.onClick.AddListener(CloseShop);

            /*_youSure.onClick.RemoveAllListeners();
            _youSure.onClick.AddListener(BuyNow);

            _notBuy.onClick.RemoveAllListeners();
            _notBuy.onClick.AddListener(NotBuy);*/
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