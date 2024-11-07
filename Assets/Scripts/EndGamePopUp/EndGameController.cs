using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AFG
{
    public class EndGameController : MonoBehaviour
    {
        [SerializeField] private GameObject _endGamePopUp;
        private Image _endGamePopUpImage;
        [SerializeField] private Color _winColor;
        [SerializeField] private Color _loseColor;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _resultCoin;
        [SerializeField] private TMP_Text _resultBooks;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Image _prefabImage;
        [SerializeField] private bool _win;
        [SerializeField] private GameObject _books;
        [SerializeField] private GameObject _rewards;

        private PlayerDataWrapper PlayerData { get; set; }
        void Start()
        {
            PlayerData = GameController.Instance.SaveManager.PlayerData;
            _endGamePopUpImage = _endGamePopUp.GetComponent<Image>();
            SetResult();
            AddEventListeners();
        }
        public void SetResult()
        //public void SetResult(int level, List<CharacterDataWrapper> characters)
        {
            if (_win)
            {
                _endGamePopUpImage.color = _winColor;
                _nextLevelButton.gameObject.SetActive(true);
                _level.text = "Level 1" + " - " + "Victory";
                _resultCoin.text = Random.Range(20, 41).ToString();
                var books = Random.Range(0, 51);
                if (books != 0)
                {
                    _resultBooks.text = "0";
                    _books.SetActive(false);
                }
                else
                {
                    _resultBooks.text = "1";
                }
            }
            else
            {
                _rewards.SetActive(false);
                _level.text = "Level 1" + " - " + "Defeat";
                _endGamePopUpImage.color = _loseColor;
                _nextLevelButton.gameObject.SetActive(false);
            }


        }

        private void AddEventListeners()
        {
            _menuButton.onClick.RemoveAllListeners();
            _restartButton.onClick.RemoveAllListeners();
            _nextLevelButton.onClick.RemoveAllListeners();
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        }

        private void OnNextLevelButtonClicked()
        {
            SavePlayerData();
            Debug.Log("NextLevel");
        }

        private void OnRestartButtonClicked()
        {
            if (_win)
            {
                SavePlayerData();
            }
            else
            {
                GameController.Instance.LevelModel.ReLoadScene(MenuSquadScene);
            }

        }

        private string MenuSquadScene => GameController.Instance.LevelModel.MenuSquadScene.name;
        private void OnMenuButtonClicked()
        {
            if (_win)
            {
                SavePlayerData();
            }
            GameController.Instance.LevelModel.UnLoadPrevScene(MenuSquadScene);
        }

        private void SavePlayerData()
        {
            PlayerData.CoinData.CoinDataWrapper.CoinCount += int.Parse(_resultCoin.text);
            PlayerData.BooksData.BooksDataWrapper.BooksCount += int.Parse(_resultBooks.text);
            GameController.Instance.SaveManager.SavePlayerData(PlayerData);
        }
    }
}