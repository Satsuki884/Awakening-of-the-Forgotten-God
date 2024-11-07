using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG{
public class TabsController : MonoBehaviour
{

    [SerializeField] private GameObject _characterTabUI;
    [SerializeField] private GameObject _booksTabUI;
    [SerializeField] private Button _openCharactersTab;
    [SerializeField] private Button _openBooksTabShop;

    [SerializeField] private Color _activeColor;// = new Color(32, 0, 40, 255);
    [SerializeField] private Color _inactiveColor;// = new Color(77, 0, 96, 255);

    [SerializeField] private Button _toUp;


    void Start()
    {
        AddShopEvents();
        SetButtonColors(_openCharactersTab, _openBooksTabShop);
        OpenCharactersTab();
    }

    void AddShopEvents()
    {
        _openCharactersTab.onClick.RemoveAllListeners();
        _openCharactersTab.onClick.AddListener(OpenCharactersTab);


        _openBooksTabShop.onClick.RemoveAllListeners();
        _openBooksTabShop.onClick.AddListener(OpenBooksTab);

        _toUp.onClick.RemoveAllListeners(); 
        _toUp.onClick.AddListener(ToUp);
    }

        private void ToUp()
        {
            _characterTabUI.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        }

        public void OpenBooksTab()
    {
        _toUp.gameObject.SetActive(false);
        _booksTabUI.SetActive(true);
        _characterTabUI.SetActive(false);
        SetButtonColors(_openBooksTabShop, _openCharactersTab);
    }

    public void OpenCharactersTab()
    {
        _toUp.gameObject.SetActive(true);
        _characterTabUI.SetActive(true);
        _booksTabUI.SetActive(false);
        SetButtonColors(_openCharactersTab, _openBooksTabShop);
    }

    void SetButtonColors(Button activeButton, Button inactiveButton)
    {
        activeButton.GetComponent<Image>().color = _activeColor;
        inactiveButton.GetComponent<Image>().color = _inactiveColor;
    }
}

}
