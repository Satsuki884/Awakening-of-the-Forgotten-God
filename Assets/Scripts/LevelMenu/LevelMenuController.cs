using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AFG{
    public class LevelMenuController : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        void Start()
        {
            AddInventoryEvents();
        }

        private void AddInventoryEvents()
        {
            for(int i = 0; i < _buttons.Count; i++)
            {
                int j = i;
                _buttons[j].onClick.RemoveAllListeners();
                _buttons[j].onClick.AddListener(() => OnButtonClicked(j));
            }

        }

        LevelModel LevelModel => GameController.Instance.LevelModel;

        private void OnButtonClicked(int buttonIndex)
        {
            LevelModel.LevelNumber = buttonIndex + 1;
            LevelModel.UnLoadPrevScene(LevelModel.LevelMenuScene, LevelModel.MenuSquadScene);
        }
    }
}