using System.Collections;
using System.Collections.Generic;
using AFG.Character;
using AFG.MVP;
using UnityEngine;

namespace AFG
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        //TODO add save and Load
        //public SaveManager SaveManager {get;set;}
        
        private CombatModel _combatModel;

        public CombatModel CombatModel
        {
            get
            {
                if (_combatModel == null)
                {
                    _combatModel = new CombatModel();
                }

                return _combatModel;
            }
        }
    }
}

