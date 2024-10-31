using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AFG {
    public class ShopControllerUI : MonoBehaviour
    {
        [SerializeField] private float _itemSpacing = .5f;
        private float _playersMoney = 150;


        //[SerializeField] private GameObject _shopPanel;
        [SerializeField] private Transform _shopMenu;
        [SerializeField] private Transform _shopItemsContainer;
        [SerializeField] private GameObject _itemPrefab;
        private float _itemHeight;

        private List<CharacterDataWrapper> Characters { get; set; } = 
            new List<CharacterDataWrapper>();
        private List<CharacterDataWrapper> PlayerCharacters { get; set; } = 
            new List<CharacterDataWrapper>();

        [FormerlySerializedAs("playerCharacterDataWrapperHolder")] [SerializeField] private CharacterDataHolder playerCharacterDataHolder;


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

            Characters = GameController.Instance.SaveManager.AllCharacters;
            PlayerCharacters = GameController.Instance.SaveManager.PlayerCharacters;

            GenerateShopItemsUI();
        }

        //private CharacterStats _characterStats;

        private void GenerateShopItemsUI()
        {
            

            _itemHeight = _shopItemsContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta.y;
            Destroy(_shopItemsContainer.GetChild(0).gameObject);
            _shopItemsContainer.DetachChildren();

            for (int i = 0; i < Characters.Count; i++)
            {
                //_characterStats = Characters[i].CharacterPrefab.GetComponentInChildren<CharacterStats>();
                //Debug.Log(Characters[i].CharacterName);

                CharacterShopItemUI itemUI = Instantiate(_itemPrefab, _shopItemsContainer).GetComponent<CharacterShopItemUI>();

                itemUI.SetItemPosition(Vector3.down * i * (_itemHeight + _itemSpacing));
                itemUI.gameObject.name = "Item " + i + " - " + Characters[i].CharacterName;
                itemUI.SetCharacterName(Characters[i].CharacterName);
                itemUI.SetCharacterImage(Characters[i].Icon);
                itemUI.SetCharacterAtk(Characters[i].Atk);
                itemUI.SetCharacterHp(Characters[i].Health);
                itemUI.SetCharacterDef(Characters[i].Def);
                itemUI.SetCharacterSpeed(Characters[i].Speed);
                itemUI.SetCharacterPrice(Characters[i].Price);

                //TODO info about user`s money
                if (Characters[i].Price > _playersMoney)
                {
                    itemUI.SetButtonUnEnable();
                } 
                

                for (int j = 0; j < PlayerCharacters.Count; j++)
                {
                    if (Characters[i].CharacterName == PlayerCharacters[j].CharacterName)
                    {
                        itemUI.SetSoldOut();
                    }
                }

                itemUI.OnItemPurchase(itemUI, Characters[i], OnItemPurchasedConfirmed);
                _shopItemsContainer.GetComponent<RectTransform>().sizeDelta =
                    Vector3.up * ((_itemHeight + _itemSpacing) * (Characters.Count-1) + _itemSpacing);
            }
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

            //synchronize player characters holders (SO)
            GameController.
                Instance.
                SaveManager.
                SynchronizePlayerCharactersHolders(PlayerCharacters);

            
            _itemBuyNow.SetActive(false);
            _itemSoldOut.SetActive(true);

            GenerateShopItemsUI();
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