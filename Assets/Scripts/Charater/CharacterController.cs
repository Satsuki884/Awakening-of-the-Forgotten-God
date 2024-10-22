using System;
using System.Collections;
using System.Collections.Generic;
using AFG.Stats;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterController : MonoBehaviour
    {
        public event Action<CharacterController> OnSelected;
        
        //TODO refactoring change after buf-debuf
        [SerializeField] protected CharacterAnimationController _animationController;
        [SerializeField] protected CharacterMoveController _moveController;
        [SerializeField] protected CharacterDamageController _damageController;
        [SerializeField] protected CharacterHealController _healController;
        [SerializeField] protected CharacterDeBufController _deBufController;

        public CharacterAnimationController AnimationController => _animationController;
        public CharacterMoveController MoveController => _moveController;
        public CharacterDamageController DamageController => _damageController;
        public CharacterHealController HealController => _healController;
        public CharacterDeBufController DeBufController => _deBufController;

        private bool _isAbleToSelect;
        public bool IsAbleToSelect
        {
            get => _isAbleToSelect;
            set
            {
                _isAbleToSelect = value;
                if (_isAbleToSelect)
                {
                    transform.position+=Vector3.up * 10;
                }
                else
                {
                    transform.position-=Vector3.up * 10;
                }
            }
        }
        
        protected CharacterBrain _brain;
        protected CharacterStats _characterStats;
        
        protected CharacterSkill[] _skills;
        public CharacterSkill[] Skills => _skills;

        public float Def
        {
            get => _characterStats.Def;
            set => _characterStats.Def = value; // Сеттер для защиты
        }

        public float Speed
        {
            get => _characterStats.Speed;
            set => _characterStats.Speed = value; // Сеттер для скорости
        }

        public float Atk
        {
            get => _characterStats.Atk;
            set => _characterStats.Atk = value; // Сеттер для атаки
        }

        public float Health
        {
            get => _characterStats.Health;
            set => _characterStats.Health = value; // Сеттер для здоровья
        }

        public virtual void Initialization(CharacterBrain brain)
        {
            Debug.Log("Initialized character "+gameObject.name);
           
            _brain = brain;
            _brain.Initialization(this);

            _characterStats = GetComponentInChildren<CharacterStats>();
            
            _skills = GetComponentsInChildren<CharacterSkill>();
            //Debug.Log("Character skill" + _skills);
            //TODO add load from file
            //_characterStats.Initialize("{\n    \"health\": 100.0,\n    \"speed\": 5.5\n}\n");
        }

        public virtual void SelectCharacter()
        {
            Debug.Log("Select character "+gameObject.name);
        }

        private void OnMouseDown()
        {
            if (IsAbleToSelect)
            {
                OnSelected?.Invoke(this);
            }
        }
    }
}

