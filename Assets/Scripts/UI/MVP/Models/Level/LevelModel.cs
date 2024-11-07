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
        LoadBackgroundScene();

        //ToDo: refactor
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            LoadNewScene("MainMenu");
        } else if(SceneManager.GetActiveScene().name == "Inventory"){
            LoadNewScene("Inventory");
        } else if(SceneManager.GetActiveScene().name == "MenuSquad"){
            LoadNewScene("MenuSquad");
        }
        AddShopEvents();
    }

    private void LoadBackgroundScene()
    {
        if (_backgroundScene != null && !SceneManager.GetSceneByName(_backgroundScene.name).isLoaded)
        {
            SceneManager.LoadScene(_backgroundScene.name);
        }
    }

    private void AddShopEvents()
    {
        _openInventory.onClick.RemoveAllListeners();
        _openInventory.onClick.AddListener(OpenInventory);

        _openButtleMap.onClick.RemoveAllListeners();
        _openButtleMap.onClick.AddListener(OpenButtleMap);
    }

    private string _lastLoadedScene;

    public void LoadNewScene(string sceneName)
    {
        Debug.Log("LoadNewScene: " + sceneName);
        Debug.Log("LastLoadedScene: " + _lastLoadedScene);
        // if (!string.IsNullOrEmpty(_lastLoadedScene))
        // {
        //     SceneManager.UnloadSceneAsync(_lastLoadedScene);
        // }
        // SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        // _lastLoadedScene = sceneName;
        // Debug.Log("LastLoadedScene = sceneName: " + _lastLoadedScene);
        if (!string.IsNullOrEmpty(_lastLoadedScene))
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.UnloadSceneAsync(_lastLoadedScene);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            _lastLoadedScene = sceneName;
            Debug.Log("LastLoadedScene = sceneName: " + _lastLoadedScene);
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        SceneManager.LoadScene(_lastLoadedScene, LoadSceneMode.Additive);
        _lastLoadedScene = scene.name;
        Debug.Log("LastLoadedScene = sceneName: " + _lastLoadedScene);
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
