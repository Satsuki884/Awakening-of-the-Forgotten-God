using UnityEditor;
using UnityEngine;

namespace AFG
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Configs/new LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private CharacterDataHolder _aiSquad;
        [SerializeField] private SceneAsset _levelScene;
    
        public CharacterDataHolder AISquad => _aiSquad;
        public SceneAsset LevelScene => _levelScene;
    }
}

