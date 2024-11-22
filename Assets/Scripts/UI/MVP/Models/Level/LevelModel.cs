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
    [SerializeField] private Animator transition;
    [SerializeField] private float _transitionTime = 0.5f;


    [SerializeField] private string _mainMenuScene;
    public string MainMenuScene => _mainMenuScene;
    [SerializeField] private string _inventoryScene;
    public string InventoryScene => _inventoryScene;
    [SerializeField] private string _menuSquadScene;
    public string MenuSquadScene => _menuSquadScene;

    void Start()
    {
        LoadBackgroundScene();
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetBool("End", false);
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        transition.SetBool("End", true);
        yield return new WaitForSeconds(_transitionTime);
        transition.SetBool("Start", false);
    }

    private void LoadBackgroundScene()
    {
        if (_backgroundScene != null && !SceneManager.GetSceneByName(_backgroundScene).isLoaded)
        {
            //SceneManager.LoadScene(_mainMenuScene);
            SceneManager.LoadScene(_backgroundScene, LoadSceneMode.Additive);
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
        StartCoroutine(LoadScene(sceneName));
    }

    public void UnLoadMainMenuScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(_mainMenuScene);
        LoadNewScene(sceneName);
    }

    public void UnLoadPrevScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        LoadNewScene(_mainMenuScene);
    }

    public void ReLoadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        LoadNewScene(sceneName);
    }

    public void OpenInventory()
    {
        UnLoadMainMenuScene(_inventoryScene);
    }

    public void OpenButtleMap()
    {
        UnLoadMainMenuScene(_menuSquadScene);
    }

    [SerializeField] private string _backgroundScene;
    public event Action<int> OnLevelStarted;
    public event Action<int> OnLevelFinish;

    [SerializeField] private int _levelIndex;
    public int LevelIndex => _levelIndex;

    [SerializeField] private LevelData[] _levels;

    public CharacterDataWrapper[] PlayerSquad { get; set; }

    public void StartLevel()
    {
        OnLevelStarted?.Invoke(LevelIndex);
        SceneManager.LoadScene(GetLevelScene(), LoadSceneMode.Additive);
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

    public string GetLevelScene()
    {
        return _levels[LevelIndex].LevelScene;
    }

    public string GetPrevLevelScene()
    {
        return _levels[LevelIndex - 1].LevelScene;
    }
}