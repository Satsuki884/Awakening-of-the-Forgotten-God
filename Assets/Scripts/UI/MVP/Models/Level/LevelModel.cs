using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AFG;
using JetBrains.Annotations;
using TMPro;
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
    [SerializeField] private string _levelMenuScene;
    public string LevelMenuScene => _levelMenuScene;
    [SerializeField] private string _levelScene;
    public string LevelScene => _levelScene;

    private string _end = "End";
    private string _start = "Start";

    void Start()
    {
        LoadBackgroundScene();
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetBool(_end, false);
        transition.SetBool(_start, true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        transition.SetBool(_end, true);
        transition.SetBool(_start, false);
        yield return new WaitForSeconds(_transitionTime);
    }

    private void LoadBackgroundScene()
    {
        if (_backgroundScene != null && !SceneManager.GetSceneByName(_backgroundScene).isLoaded)
        {
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

    public void UnLoadPrevScene(string prevSceneName, string nextSceneName)
    {
        SceneManager.UnloadSceneAsync(prevSceneName);
        StartCoroutine(LoadScene(nextSceneName));
    }

    //[Header("Level Menu")]

    public int LevelNumber{ get; set; }

    [SerializeField] private string _backgroundScene;

    [SerializeField] private LevelData[] _levels;

    public CharacterDataWrapper[] PlayerSquad { get; set; }

    public CharacterDataWrapper[] GetAiSquad()
    {
        return _levels[LevelNumber].AISquad.CharacterData.Select(x => x.CharacterDataWrapper).ToArray();
    }

    public void StartNextLevel(){
        LevelNumber++;
        StartCurrentLevel();
    }

    public void StartCurrentLevel(){
        // OnLevelStarted?.Invoke(LevelNumber);
        UnLoadPrevScene(MenuSquadScene, LevelScene);
    }
}