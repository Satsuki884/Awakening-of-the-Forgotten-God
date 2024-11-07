using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{
    public class BooksShopController : MonoBehaviour
    {
        [SerializeField] private GameObject _booksBuyPanel;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _buyNow;
        [SerializeField] private Button _booksPurchaseButton;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private TMP_Text _booksPrice;
        [SerializeField] private Button _notBuy;

        private PlayerDataWrapper PlayerData { get; set; }

        [SerializeField] private TMP_Text _maxCountToBuy;

        private BooksCoinController _booksCoinController;

        void Start()
        {
            AddShopEvents();
            PlayerData = GameController.Instance.SaveManager.PlayerData;
            _booksCoinController = FindObjectOfType<BooksCoinController>();
            Refresh();
            SetBooksPrice();

            _slider.onValueChanged.AddListener(UpdateBuyNowText);
        }

        private void SetBooksPrice()
        {
            _booksPrice.text = PlayerData.BooksData.BooksDataWrapper.BooksPrice.ToString();
        }

        private void Refresh()
        {
            _maxCountToBuy.text = (PlayerData.
                                CoinData.
                                CoinDataWrapper
                                .CoinCount / PlayerData.
                                BooksData.
                                BooksDataWrapper.
                                BooksPrice)
                                .ToString();
            if (PlayerData.BooksData.BooksDataWrapper.BooksPrice > PlayerData.CoinData.CoinDataWrapper.CoinCount)
            {
                //_booksBuyPanel.SetActive(false);
                SetButtonUnEnable();
            }
        }

        private void AddShopEvents()
        {
            _booksPurchaseButton.onClick.RemoveAllListeners();
            _booksPurchaseButton.onClick.AddListener(PurchaseBooks);


            _notBuy.onClick.RemoveAllListeners();
            _notBuy.onClick.AddListener(NotBuyBooks);

            _purchaseButton.onClick.RemoveAllListeners();
            _purchaseButton.onClick.AddListener(BuyBooks);
        }

        private void UpdateBuyNowText(float value)
        {
            _slider.maxValue = PlayerData.
                                CoinData.
                                CoinDataWrapper.
                                CoinCount / PlayerData.
                                BooksData.
                                BooksDataWrapper.
                                BooksPrice;
            _buyNow.text = $"Buy {value} books?";
            
        }


        private void PurchaseBooks()
        {
            Debug.Log("you buy " + _slider.value + " books");
            PlayerData.
            CoinData.
            CoinDataWrapper.
            CoinCount = PlayerData.
                        CoinData.
                        CoinDataWrapper.
                        CoinCount - PlayerData.
                                    BooksData.
                                    BooksDataWrapper.
                                    BooksPrice * (int)_slider.value;
            PlayerData.
            BooksData.
            BooksDataWrapper.
            BooksCount = (int)_slider.value;

            if (_booksCoinController != null)
            {
                GameController.
                Instance.
                SaveManager.
                SavePlayerData(PlayerData);
                _booksCoinController.Refresh();
                Refresh();
            }
            _booksBuyPanel.SetActive(false);
        }

        private void NotBuyBooks()
        {
            _booksBuyPanel.SetActive(false);
        }

        private Color _initialColor;
        private Color _initialColorText;

        private void SetButtonUnEnable()
        {
            _purchaseButton.enabled = false;
            _initialColor = _purchaseButton.GetComponent<Image>().color;
            _purchaseButton.GetComponent<Image>().color = Color.gray;
            TMP_Text buttonText = _purchaseButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                _initialColorText = buttonText.color;
                buttonText.color = new Color(0.5f, 0.5f, 0.5f);
            }
        }

        private void BuyBooks()
        {
            _booksBuyPanel.SetActive(true);

        }
    }

}
