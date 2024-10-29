using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControllerUI : MonoBehaviour
{
    //[Header("Shop Events")]
    [SerializeField] GameObject shopUI;
    [SerializeField] Button openShop;
    [SerializeField] Button closeShop;

    // Start is called before the first frame update
    void Start()
    {
        AddShopEvents();
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