using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AFG;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelModel : MonoBehaviour
{

    [SerializeField] private Button _openInventory;
    [SerializeField] private Button _openButtleMap;
    // [SerializeField] private Button _closeInventory;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadBackgroundScene();
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            LoadNewScene("MainMenu");
        }
        AddShopEvents();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadBackgroundScene();
    }

    private void LoadBackgroundScene()
    {
        if (_backgroundScene != null && !SceneManager.GetSceneByName(_backgroundScene.name).isLoaded)
        {
            SceneManager.LoadScene(_backgroundScene.name);
            //LoadNewScene(SceneManager.GetActiveScene().name);
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Additive);
        }
    }

    private void AddShopEvents()
    {
        // _closeInventory.onClick.RemoveAllListeners();
        // _closeInventory.onClick.AddListener(CloseInventory);

        _openInventory.onClick.RemoveAllListeners();
        _openInventory.onClick.AddListener(OpenInventory);

        _openButtleMap.onClick.RemoveAllListeners();
        _openButtleMap.onClick.AddListener(OpenButtleMap);
    }

    private string _lastLoadedScene;

    public void LoadNewScene(string sceneName)
    {
        // Unload the last loaded scene if it exists
        if (!string.IsNullOrEmpty(_lastLoadedScene))
        {
            SceneManager.UnloadSceneAsync(_lastLoadedScene);
        }

        // Load the new scene additively
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        // Update the last loaded scene
        _lastLoadedScene = sceneName;
    }

    private void OpenInventory()
    {
        LoadNewScene("Inventory");
        // SceneManager.LoadScene("Inventory", LoadSceneMode.Additive);
        // LoadBackgroundScene();
    }

    private void OpenButtleMap()
    {
        LoadNewScene("MenuSquad");
        // SceneManager.LoadScene("MenuSquad", LoadSceneMode.Additive);
        // LoadBackgroundScene();
    }

    [SerializeField] private SceneAsset _backgroundScene;
    public event Action<int> OnLevelStarted;
    public event Action<int> OnLevelFinish;

    [SerializeField] private int _levelIndex;
    public int LevelIndex => _levelIndex;

    [SerializeField] private LevelData[] _levels;

    public CharacterDataWrapper[] PlayerSquad { get; set; }

    //private string _currentScene => SceneManager.GetActiveScene().name;

    public void StartLevel()
    {
        OnLevelStarted?.Invoke(LevelIndex);
        SceneManager.LoadScene(GetLevelScene().name, LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync(_currentScene);
    }

    public void FinishLevel()
    {
        OnLevelFinish?.Invoke(LevelIndex);
        _levelIndex = LevelIndex >= _levels.Length - 1 ? 0 : LevelIndex + 1;
    }



    public CharacterDataWrapper[] GetAiSquad()
    {
        return _levels[LevelIndex].AISquad.CharacterData.Select(x => x.CharacterDataWrapper).ToArray();
    }

    public SceneAsset GetLevelScene()
    {
        return _levels[LevelIndex].LevelScene;
    }

    public SceneAsset GetPrevLevelScene()
    {
        return _levels[LevelIndex - 1].LevelScene;
    }
}
