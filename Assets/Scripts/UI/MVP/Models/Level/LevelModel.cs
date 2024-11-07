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
    [SerializeField] private SceneAsset _mainMenuScene;
    [SerializeField] private SceneAsset _inventoryScene;
    [SerializeField] private SceneAsset _menuSquadScene;
    void Start()
    {
        LoadBackgroundScene();

        //ToDo: refactor
        if(SceneManager.GetActiveScene().name == _mainMenuScene.name){
            LoadNewScene(_mainMenuScene.name);
        } else if(SceneManager.GetActiveScene().name == _inventoryScene.name){
            OpenInventory();
        } else if(SceneManager.GetActiveScene().name == _menuSquadScene.name){
            OpenButtleMap();
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
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        Debug.Log("LoadNewScene: " + SceneManager.GetActiveScene().name);
    }

    public void UnLoadMainMenuScene(bool typeScene)
    {
        SceneManager.UnloadSceneAsync(_mainMenuScene.name);
        LoadNewScene(typeScene ? _inventoryScene.name : _menuSquadScene.name);
    }

    public void UnLoadPrevScene(bool menu)
    {
        SceneManager.UnloadSceneAsync(menu ? _inventoryScene.name : _menuSquadScene.name);
        LoadNewScene(_mainMenuScene.name);
    }

    private void OpenInventory()
    {
        UnLoadMainMenuScene(true);
    }

    private void OpenButtleMap()
    {
        UnLoadMainMenuScene(false);
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
