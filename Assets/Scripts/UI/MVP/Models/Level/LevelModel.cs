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

    [SerializeField] private SceneAsset _mainMenuScene;
    public SceneAsset MainMenuScene => _mainMenuScene;
    [SerializeField] private SceneAsset _inventoryScene;
    public SceneAsset InventoryScene => _inventoryScene;
    [SerializeField] private SceneAsset _menuSquadScene;
    public SceneAsset MenuSquadScene => _menuSquadScene;
    void Start()
    {
        LoadBackgroundScene();
    }

    private void LoadBackgroundScene()
    {
        if (_backgroundScene != null && !SceneManager.GetSceneByName(_backgroundScene.name).isLoaded)
        {
            SceneManager.LoadScene(_backgroundScene.name);
            LoadNewScene(_mainMenuScene.name);
        }
    }

    public string LoadedSceneName
    {
        get
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != SceneManager.GetActiveScene().name)
                {
                    Debug.Log(scene.name);
                    return scene.name;
                }
            }
            return null;
        }
    }

    public void LoadNewScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnLoadMainMenuScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(_mainMenuScene.name);
        LoadNewScene(sceneName);
    }

    public void UnLoadPrevScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        LoadNewScene(_mainMenuScene.name);
    }

    public void ReLoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        LoadNewScene(sceneName);
    }

    public void OpenInventory()
    {
        UnLoadMainMenuScene(_inventoryScene.name);
    }

    public void OpenButtleMap()
    {
        UnLoadMainMenuScene(_menuSquadScene.name);
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
