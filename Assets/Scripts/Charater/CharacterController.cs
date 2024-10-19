using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterController : MonoBehaviour
    {
        //TODO refactoring change after buf-debuf
        [SerializeField] protected int _speed;
        public int Speed => _speed;
        
        [SerializeField] protected CharacterAnimationController _animationController;
        [SerializeField] protected CharacterMoveController _moveController;
        [SerializeField] protected CharacterDamageController _damageController;

        protected CharacterBrain _brain;
        
        public virtual void Initialization(CharacterBrain brain)
        {
            Debug.Log("Initialized character "+gameObject.name);
           
            _brain = brain;
            _brain.Initialization(this);
        }

        public virtual void SelectCharacter()
        {
            Debug.Log("Select character "+gameObject.name);
        }
    }
}

