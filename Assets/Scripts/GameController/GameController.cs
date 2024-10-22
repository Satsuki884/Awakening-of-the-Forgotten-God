using System.Collections;
using System.Collections.Generic;
using AFG.Character;
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
        
        public CharacterBrain Brain { get; set; }
        
        public void DoSomething()
        {
            
        }
        public int TestProperty { get; set; }
    }
}

