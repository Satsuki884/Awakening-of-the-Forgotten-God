using System;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AFG
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Button _closeInventory;
        void Start()
        {
            AddInventoryEvents();
        }

        // Update is called once per frame
        private void AddInventoryEvents()
        {
            _closeInventory.onClick.RemoveAllListeners();
            _closeInventory.onClick.AddListener(CloseInventory);
        }

        private void CloseInventory()
        {
            GameController.Instance.LevelModel.LoadNewScene("MainMenu");
            //SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }
    }
}