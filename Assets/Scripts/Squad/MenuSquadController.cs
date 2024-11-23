using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{
    public class MenuSquadController : MonoBehaviour
    {
        [SerializeField] private Button _closeMenuSquad;
        void Start()
        {
            AddInventoryEvents();
        }

        private void AddInventoryEvents()
        {
            _closeMenuSquad.onClick.RemoveAllListeners();
            _closeMenuSquad.onClick.AddListener(CloseMenuSquad);
        }
        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void CloseMenuSquad()
        {
            LevelModel.UnLoadPrevScene(LevelModel.MenuSquadScene, LevelModel.LevelMenuScene);
        }
    }
}
