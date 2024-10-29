using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;
using static UnityEditor.Progress;

namespace AFG
{
    public class CharacterShopItemUI : MonoBehaviour
    {
        [SerializeField] private Color _itemNotSelectedColor;
        [SerializeField] private Color _itemSelectedColor;

        //[SerializeField] private Image _characterImage = null;
        [SerializeField] private TMP_Text _characterName;
        [SerializeField] private TMP_Text _atk;
        [SerializeField] private TMP_Text _hp;
        [SerializeField] private TMP_Text _def;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _characterPrice;
        [SerializeField] private Button _characterPurchaseButtonInItem;

        [SerializeField] private Button _itemButton;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Outline _itemOutline;

        [SerializeField] private GameObject _itemBuyNow;
        [SerializeField] private Button _characterPurchaseButton;
        [SerializeField] private Button _notBuy;


        [SerializeField] private GameObject _itemSoldOut;


        

        public void SetItemPosition(Vector2 pos)
        {
            GetComponent<RectTransform>().anchoredPosition += pos;
        }

        /*public void SetCharacterImage(Sprite sprite)
        {
            _characterImage.sprite = sprite;
        }*/

        public void SetCharacterName(string name)
        {
            _characterName.text = name;
        }

        public void SetCharacterHp(float hp)
        {
            _hp.text = hp.ToString();
        }

        public void SetCharacterAtk(float atk)
        {
            _atk.text = atk.ToString();
        }

        public void SetCharacterDef(float def)
        {
            _def.text = def.ToString();
        }

        public void SetCharacterSpeed(float speed)
        {
            _speed.text = speed.ToString();
        }

        public void SetCharacterPrice(float price)
        {
            _characterPrice.text = 100.ToString();
        }

        public void SetSoldOut()
        {
            _itemSoldOut.SetActive(true);
            _characterPurchaseButtonInItem.gameObject.SetActive(false);
        }

        public void DontBuy()
        {
            _notBuy.onClick.RemoveAllListeners();
            _notBuy.onClick.AddListener(DBuy);
            
        }

        public void DBuy()
        {
            _itemBuyNow.SetActive(false);
        }

        private Color _initialColor;
        private Color _initialColorText;

        public void SetButtonUnEnable()
        {
            _characterPurchaseButtonInItem.enabled = false;
            _initialColor = _characterPurchaseButtonInItem.GetComponent<Image>().color;
            _characterPurchaseButtonInItem.GetComponent<Image>().color = Color.gray;
            TMP_Text buttonText = _characterPurchaseButtonInItem.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                _initialColorText = buttonText.color;
                buttonText.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }
        public void SetButtonEnable()
        {
            _characterPurchaseButtonInItem.enabled = true;
            _characterPurchaseButtonInItem.GetComponent<Image>().color = _initialColor;
            TMP_Text buttonText = _characterPurchaseButtonInItem.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.color = _initialColorText;
            }
        }


        /*public void SetCharacterAsPurchased()
        {
            _characterPurchaseButton.gameObject.SetActive(false);

            _itemButton.interactable = true;
            _itemImage.color = _itemNotSelectedColor;

        }*/

        public void OnItemPurchase(CharacterShopItemUI item, CharacterDataWrapper character, UnityAction<CharacterShopItemUI, CharacterDataWrapper, GameObject, GameObject> action)
        {
            _characterPurchaseButton.onClick.RemoveAllListeners();
            _characterPurchaseButton.onClick.AddListener(() => action.Invoke(item, character, _itemBuyNow, _itemSoldOut));
        }

        public void ItemPurchaseConfirm(CharacterShopItemUI item, CharacterDataWrapper character, UnityAction<CharacterShopItemUI, CharacterDataWrapper, GameObject, GameObject> action)
        {
            _characterPurchaseButton.onClick.RemoveAllListeners();
            _characterPurchaseButton.onClick.AddListener(() => action.Invoke(item, character, _itemBuyNow, _itemSoldOut));
        }

        /*public void OnItemSelect(int itemIndex, UnityAction<int> action)
        {

            _itemButton.interactable = true;
            _itemButton.onClick.RemoveAllListeners();
            _itemButton .onClick.AddListener(() => action.Invoke(itemIndex));
        }

        public void SelectItem()
        {
            _itemOutline.enabled = true;
            _itemImage.color = _itemSelectedColor;
            _itemButton.interactable = false;
        }

        public void deselectItem()
        {
            _itemOutline.enabled = false;
            _itemImage.color = _itemNotSelectedColor;
            _itemButton.interactable = true;
        }*/
    }
}

