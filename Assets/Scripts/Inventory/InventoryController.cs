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

        // Update is called once per frame
        private void AddInventoryEvents()
        {
            _closeInventory.onClick.RemoveAllListeners();
            _closeInventory.onClick.AddListener(CloseInventory);
        }

        // private string InventoryScene => GameController.Instance.LevelModel.InventoryScene.name;
        // private string MenuSquadScene => GameController.Instance.LevelModel.MenuSquadScene.name;
        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void CloseInventory()
        {
            // GameController.Instance.LevelModel.LoadedSceneName;
            // if(_isInventory)
            // {
                LevelModel.UnLoadPrevScene(LevelModel.LoadedSceneName);
            // }
            // else
            // {
            //     GameController.Instance.LevelModel.UnLoadPrevScene(MenuSquadScene);
            // }
        }
    }
}