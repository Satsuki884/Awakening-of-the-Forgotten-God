using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BooksShopController : MonoBehaviour
{
    [SerializeField] private GameObject _booksBuyPanel;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _buyNow;
    [SerializeField] private Button _booksPurchaseButton;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _notBuy;

    void Start()
    {
        AddShopEvents();

        _slider.onValueChanged.AddListener(UpdateBuyNowText);
    }

    void AddShopEvents()
    {
        _booksPurchaseButton.onClick.RemoveAllListeners();
        _booksPurchaseButton.onClick.AddListener(PurchaseBooks);


        _notBuy.onClick.RemoveAllListeners();
        _notBuy.onClick.AddListener(NotBuyBooks);

        _purchaseButton.onClick.RemoveAllListeners();
        _purchaseButton.onClick.AddListener(BuyBooks);
    }

    void UpdateBuyNowText(float value)
    {
        _buyNow.text = $"Buy {value} books?";
    }


    void PurchaseBooks()
    {
        Debug.Log("you buy " +  _slider.value + " books");
        _booksBuyPanel.SetActive(false);
    }

    void NotBuyBooks()
    {
        _booksBuyPanel.SetActive(false);
    }

    void BuyBooks()
    {
        _booksBuyPanel.SetActive(true);
    }
}
