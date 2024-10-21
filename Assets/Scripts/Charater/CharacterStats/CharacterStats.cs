using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Stats
{
    public class CharacterStats : MonoBehaviour
    {
       [SerializeField] private float _health;

        public float Health
        {
            get
            {
                return _health;
            }
            set
            {
                if(value < 0)
                {
                    _health = 0;
                }
                else
                {
                    _health = value;
                }
            }
        }

        [SerializeField] private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public void Initialize()
        {
           
        }
    }
}
