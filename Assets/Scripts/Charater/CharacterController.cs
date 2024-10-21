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

        public CharacterAnimationController AnimationController => _animationController;
        public CharacterMoveController MoveController => _moveController;
        public CharacterDamageController DamageController => _damageController;

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
        
        public float Speed => _characterStats.Speed;
        
        public virtual void Initialization(CharacterBrain brain)
        {
            Debug.Log("Initialized character "+gameObject.name);
           
            _brain = brain;
            _brain.Initialization(this);

            _characterStats = GetComponentInChildren<CharacterStats>();
            
            _skills = GetComponentsInChildren<CharacterSkill>();
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

