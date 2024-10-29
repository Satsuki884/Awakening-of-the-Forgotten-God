using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AFG;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelModel : MonoBehaviour
{
    public event Action<int> OnLevelStarted;
    public event Action<int> OnLevelFinish;

    [SerializeField] private int _levelIndex;
    public int LevelIndex => _levelIndex;
    
    [SerializeField] private LevelData[] _levels;
    
    public CharacterDataWrapper[] PlayerSquad { get; set; }

    public void StartLevel()
    {
        OnLevelStarted?.Invoke(LevelIndex);
        SceneManager.LoadScene(GetLevelScene().name);
    }
    
    public void FinishLevel()
    {
        OnLevelFinish?.Invoke(LevelIndex);
        _levelIndex = LevelIndex >= _levels.Length - 1 ? 0 : LevelIndex + 1;
    }
    
    public CharacterDataWrapper[] GetAiSquad()
    {
        return _levels[LevelIndex].AISquad.CharacterData.Select(x=>x.CharacterDataWrapper).ToArray();
    }
    
    public SceneAsset GetLevelScene()
    {
        return _levels[LevelIndex].LevelScene;
    }
}
