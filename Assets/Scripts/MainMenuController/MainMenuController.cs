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
        void Start()
        {
            AddShopEvents();
        }

        private void AddShopEvents()
        {
            _openShop.onClick.RemoveAllListeners();
            _openShop.onClick.AddListener(OpenShop);
        }

        private void OpenShop()
        {
            shopUI.SetActive(true);
        }
    }
}
