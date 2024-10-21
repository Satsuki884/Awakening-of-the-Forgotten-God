using System.Collections;
using System.Collections.Generic;
using AFG.Stats;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterController : MonoBehaviour
    {
        //TODO refactoring change after buf-debuf
        [SerializeField] protected CharacterAnimationController _animationController;
        [SerializeField] protected CharacterMoveController _moveController;
        [SerializeField] protected CharacterDamageController _damageController;

        protected CharacterBrain _brain;
        protected CharacterStats _characterStats;
        
        public float Speed => _characterStats.Speed;
        
        public virtual void Initialization(CharacterBrain brain)
        {
            Debug.Log("Initialized character "+gameObject.name);
           
            _brain = brain;
            _brain.Initialization(this);

            _characterStats = GetComponentInChildren<CharacterStats>();
        }

        public virtual void SelectCharacter()
        {
            Debug.Log("Select character "+gameObject.name);
        }
    }
}

