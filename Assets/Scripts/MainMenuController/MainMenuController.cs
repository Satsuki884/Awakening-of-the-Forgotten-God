using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AFG
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject shopUI;
        [SerializeField] private Button _openShop;
        //[SerializeField] private Button _openInventory;
        // [SerializeField] private Button _openButtleMap;
        void Start()
        {
            AddShopEvents();
        }

        private void AddShopEvents()
        {
            _openShop.onClick.RemoveAllListeners();
            _openShop.onClick.AddListener(OpenShop);

            // _openInventory.onClick.RemoveAllListeners();
            // _openInventory.onClick.AddListener(OpenInventory);

            // _openButtleMap.onClick.RemoveAllListeners();
            // _openButtleMap.onClick.AddListener(OpenButtleMap);
        }

        private void OpenShop()
        {
            shopUI.SetActive(true);
        }

        // private void OpenInventory()
        // {
        //     SceneManager.LoadScene("Inventory");
        // }

        // private void OpenButtleMap()
        // {
        //     SceneManager.LoadScene("MenuSquad");
        // }
    }
}
