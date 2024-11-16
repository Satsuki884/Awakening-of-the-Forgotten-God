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
        public float maxHealth;
        public float speed;
        public float def;
        public float maxDef;
        public float atk;
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

        [SerializeField] private float _maxHealth;
        public float MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        [SerializeField] private float _speed;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        [SerializeField] private float _def;

        public float Def
        {
            get => _def;
            set => _def = value;
        }

        [SerializeField] private float _maxDef;
        public float MaxDef
        {
            get => _maxDef;
            set => _maxDef = value;
        }

        [SerializeField] private float _atk;

        public float Atk
        {
            get => _atk;
            set => _atk = value;
        }

        public void Initialize(string jsonData)
        {
            // Десериализация данных из JSON
            CharacterStatsData statsData = JsonUtility.FromJson<CharacterStatsData>(jsonData);
            
            // Присвоение значений из JSON
            Health = statsData.health;
            Speed = statsData.speed;
            Atk = statsData.atk;
            Def = statsData.def;

            Debug.Log("Character initialized from JSON: Health=" + Health + ", Speed=" + Speed + ", Def=" + Def + ", Atk=" + Atk);
        }
    }
}