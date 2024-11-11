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
        [SerializeField] private bool _isInventory;
        void Start()
        {
            AddInventoryEvents();
        }

        private void AddInventoryEvents()
        {
            _closeInventory.onClick.RemoveAllListeners();
            _closeInventory.onClick.AddListener(CloseInventory);
        }
        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void CloseInventory()
        {
            LevelModel.UnLoadPrevScene(LevelModel.LoadedSceneName);
        }
    }
}