using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Stats
{
    [System.Serializable]
    public class CharacterStatsData
    {
        public float health;
        public float speed;
    }

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
                if (value < 0)
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

        public void Initialize(string jsonData)
        {
            // Десериализация данных из JSON
            CharacterStatsData statsData = JsonUtility.FromJson<CharacterStatsData>(jsonData);
            
            // Присвоение значений из JSON
            Health = statsData.health;
            Speed = statsData.speed;

            Debug.Log("Character initialized from JSON: Health=" + Health + ", Speed=" + Speed);
        }
    }
}