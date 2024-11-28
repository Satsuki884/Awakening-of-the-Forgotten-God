using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AFG
{
    public class MenuSquadController : MonoBehaviour
    {
        [SerializeField] private Button _closeMenuSquad;
        [SerializeField] private TMP_Text _levelName;
        [SerializeField] private Button _startLevel;
        void Start()
        {
            AddInventoryEvents();
        }

        private void AddInventoryEvents()
        {
            _closeMenuSquad.onClick.RemoveAllListeners();
            _closeMenuSquad.onClick.AddListener(CloseMenuSquad);

            if(_startLevel != null)
            {
                _startLevel.onClick.RemoveAllListeners();
                _startLevel.onClick.AddListener(StartLevel);
            }

            if (_levelName != null)
            {
                _levelName.text = "Level " + LevelModel.LevelNumber.ToString();
            }


        }
        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void CloseMenuSquad()
        {
            LevelModel.UnLoadPrevScene(LevelModel.MenuSquadScene, LevelModel.LevelMenuScene);
        }

        private void StartLevel()
        {
            GameController.Instance.SaveManager.PlayerSquad = GameController.Instance.PlayerCharactersHolderModel.SelectedCharacters;
            LevelModel.StartCurrentLevel();
        }
    }
}
