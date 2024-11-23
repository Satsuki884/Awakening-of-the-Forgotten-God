using System;
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
        [SerializeField] private Button _openInventory;
        [SerializeField] private Button _openButtleMap;

        // private LevelModel LevelModel;
        void Start()
        {
            AddShopEvents();
            // LevelModel LevelModel = GameController.Instance.LevelModel;
        }

        

        private void AddShopEvents()
        {
            _openShop.onClick.RemoveAllListeners();
            _openShop.onClick.AddListener(OpenShop);

            _openInventory.onClick.RemoveAllListeners();
            _openInventory.onClick.AddListener(OpenInventory);

            _openButtleMap.onClick.RemoveAllListeners();
            _openButtleMap.onClick.AddListener(OpenLevelMenu);
        }
        private void OpenShop()
        {
            shopUI.SetActive(true);
        }

        public void OpenLevelMenu()
        {
            GameController.Instance.LevelModel.UnLoadPrevScene(GameController.Instance.LevelModel.MainMenuScene, GameController.Instance.LevelModel.LevelMenuScene);
        }

        public void OpenInventory()
        {
            GameController.Instance.LevelModel.UnLoadPrevScene(GameController.Instance.LevelModel.MainMenuScene, GameController.Instance.LevelModel.InventoryScene);
        }
    }
}
