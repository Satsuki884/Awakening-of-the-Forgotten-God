using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AFG
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelName;
        [SerializeField] private Button _pauseLevel;
        [SerializeField] private GameObject _endLevelPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _continue;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _leave;
        [SerializeField] private TMP_Text _pauseLevelName;

        void Start()
        {
            AddInventoryEvents();
        }
        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void AddInventoryEvents()
        {
            _pauseLevel.onClick.RemoveAllListeners();
            _pauseLevel.onClick.AddListener(PauseLevel);
            if (_levelName != null || _pauseLevelName != null)
            {
                _levelName.text = "Level " + LevelModel.LevelNumber.ToString();
                _pauseLevelName.text = "Level " + LevelModel.LevelNumber.ToString();
            }
            _continue.onClick.RemoveAllListeners();
            _continue.onClick.AddListener(ContinueLevel);

            _restart.onClick.RemoveAllListeners();
            _restart.onClick.AddListener(RestartLevel);

            _leave.onClick.RemoveAllListeners();
            _leave.onClick.AddListener(LeaveLevel);
        }

        private void LeaveLevel()
        {
            LevelModel.UnLoadPrevScene(LevelModel.LevelScene, LevelModel.LevelMenuScene);
        }

        private void RestartLevel()
        {
            LevelModel.UnLoadPrevScene(LevelModel.LevelScene, LevelModel.LevelScene);
        }

        private void ContinueLevel()
        {
            _pausePanel.SetActive(false);
        }

        private void PauseLevel()
        {
            _pausePanel.SetActive(true);
        }

    }
}