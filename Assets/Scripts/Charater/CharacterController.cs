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

        [SerializeField] private GameObject _selectionIndicatorPrefab;
        private GameObject _selectionIndicator;
        public bool IsAbleToSelect
        {
            get => _isAbleToSelect;
            set
            {
                _isAbleToSelect = value;
                if (_isAbleToSelect)
                {
                    ShowSelectionIndicator();
                }
                else
                {
                    HideSelectionIndicator();
                }
            }
        }

        
        private void Start()
        {
            _selectionIndicator = Instantiate(_selectionIndicatorPrefab, transform);
            _selectionIndicator.transform.localScale = new Vector3(5, 5, 1);
            _selectionIndicator.transform.rotation = Quaternion.Euler(90, 0, 0);
            _selectionIndicator.SetActive(false);
        }

        private void ShowSelectionIndicator()
        {
            _selectionIndicator.SetActive(true);
            _selectionIndicator.transform.position = transform.position + Vector3.up * 0.1f;
        }

        private void HideSelectionIndicator()
        {
            _selectionIndicator.SetActive(false);
        }

        protected CharacterBrain _brain;
        protected CharacterStats _characterStats;
        
        protected CharacterSkill[] _skills;
        public CharacterSkill[] Skills => _skills;

        private CharacterDataWrapper _characterDataWrapper;
        
        public float Def
        {
            get => _characterStats.Def;
            set => _characterStats.Def = value; // ������ ��� ������
        }

        public float MaxDef
        {
            get => _characterStats.MaxDef;
            set => _characterStats.MaxDef = value;
        }

        public float Speed
        {
            get => _characterStats.Speed;
            set => _characterStats.Speed = value; // ������ ��� ��������
        }

        public float Atk
        {
            get => _characterStats.Atk;
            set => _characterStats.Atk = value; // ������ ��� �����
        }

        public float Health
        {
            get => _characterStats.Health;
            set => _characterStats.Health = value; // ������ ��� ��������
        }

        public float MaxHealth
        {
            get => _characterStats.MaxHealth;
            set => _characterStats.MaxHealth = value;
        }

        public virtual void Initialization(CharacterBrain brain, CharacterDataWrapper characterDataWrapper)
        {
            //Debug.Log("Initialized character "+gameObject.name);
           
            _brain = brain;
            _characterDataWrapper = characterDataWrapper;
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

