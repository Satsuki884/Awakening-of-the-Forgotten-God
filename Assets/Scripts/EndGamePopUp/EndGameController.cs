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
        [SerializeField] private GameObject _characters;
        [SerializeField] private Image _prefabImage;
        [SerializeField] private bool _win;
        [SerializeField] private GameObject _books;
        [SerializeField] private GameObject _rewards;
        // [SerializeField] Camera _particleCamera;
        // [SerializeField] List<ParticleSystem> _winParticles;
        // [SerializeField] List<ParticleSystem> _loseParticles;
        // [SerializeField] private float _delayBetweenParticles = 0.5f;

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
            var characters = GameController.Instance.PlayerCharactersHolderModel.SelectedCharacters;
            foreach (var character in characters)
            {
                Debug.Log(character.CharacterName);
            }
            if (_win)
            {
                _endGamePopUpImage.color = _winColor;
                _nextLevelButton.gameObject.SetActive(true);
                _level.text = "Level " + GameController.Instance.LevelModel.LevelNumber + " - " + "Victory";
                _resultCoin.text = Random.Range(20, 41).ToString();
                var books = Random.Range(0, 51);

                foreach (var character in characters)
                {
                    Debug.Log(character.Icon);
                    var image = Instantiate(_prefabImage, _characters.transform);
                    image.sprite = character.Icon;
                }
                if (books != 0)
                {
                    _resultBooks.text = "0";
                    _books.SetActive(false);
                }
                else
                {
                    _resultBooks.text = "1";
                }
                if (GameController.Instance.LevelModel.CountLevels == GameController.Instance.LevelModel.LevelNumber)
                {
                    _nextLevelButton.gameObject.SetActive(false);
                }
                // StartCoroutine(PlayParticlesWithDelay(_winParticles));
            }
            else
            {
                foreach (var character in characters)
                {
                    var imageObject = new GameObject();
                    var image = imageObject.AddComponent<Image>();
                    image.sprite = character.Icon;
                    image.transform.SetParent(_characters.transform);
                    var color = image.color;
                    color.r = Mathf.Clamp01(color.r + 0.2f);
                    image.color = color;
                    var charact = Instantiate(image, _characters.transform);
                }
                _rewards.SetActive(false);
                _level.text = "Level " + GameController.Instance.LevelModel.LevelNumber + " - " + "Defeat";
                _endGamePopUpImage.color = _loseColor;
                _nextLevelButton.gameObject.SetActive(false);
                // StartCoroutine(PlayParticlesWithDelay(_loseParticles));
            }


        }

        // private IEnumerator PlayParticlesWithDelay(List<ParticleSystem> _particles)
        // {
        //     foreach (var particle in _particles)
        //     {
        //         particle.Play();
        //         yield return new WaitForSeconds(_delayBetweenParticles);
        //     }
        // }

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
            GameController.Instance.LevelModel.LevelNumber++;
            GameController.Instance.LevelModel.UnLoadPrevScene(LevelModel.LevelScene, LevelModel.MenuSquadScene);
        }

        private void OnRestartButtonClicked()
        {
            if (_win)
            {
                SavePlayerData();
            }
            Debug.Log("Restart");
            GameController.Instance.LevelModel.UnLoadPrevScene(LevelModel.LevelScene, LevelModel.LevelScene);

        }

        private LevelModel LevelModel => GameController.Instance.LevelModel;
        private void OnMenuButtonClicked()
        {
            if (_win)
            {
                SavePlayerData();
            }
            GameController.Instance.LevelModel.UnLoadPrevScene(LevelModel.LevelScene, LevelModel.LevelMenuScene);
        }

        private void SavePlayerData()
        {
            PlayerData.CoinData.CoinDataWrapper.CoinCount += int.Parse(_resultCoin.text);
            PlayerData.BooksData.BooksDataWrapper.BooksCount += int.Parse(_resultBooks.text);
            GameController.Instance.SaveManager.SavePlayerData(PlayerData);
        }

        public void EndLevel(bool result)
        {
            _win = result;
            _endGamePopUp.SetActive(true);
            // _particleCamera.gameObject.SetActive(true);
        }
    }
}