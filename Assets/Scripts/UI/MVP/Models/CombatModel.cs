using System;
using System.Collections.Generic;
using AFG.Squad;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CombatModel
    {
        public event Action<CharacterController> OnCharacterSelected;
        public event Action OnMoveFinished;

        private string _ai = "AI";
        private string _player = "Player";

        private CharacterController _selectedCharacter;
        public CharacterController SelectedCharacter
        {
            get
            {
                return _selectedCharacter;
            }
            set
            {
                if (value != null)
                {
                    value.IsSelected = true;
                }
                else if (_selectedCharacter != null)
                {
                    _selectedCharacter.IsSelected = false;
                }

                _selectedCharacter = value;
                OnCharacterSelected?.Invoke(_selectedCharacter);
            }
        }

        private List<CharacterController> _selectedTargets;
        public List<CharacterController> SelectedTargets
        {
            get
            {
                return _selectedTargets;
            }
            set
            {
                _selectedTargets = value;
                //execute skill
                UseSelectedCharacterSkill(_player);
            }
        }

        private CharacterController _selectedAITarget;
        public CharacterController SelectedAITarget
        {
            get
            {
                return _selectedAITarget;
            }
            set
            {
                _selectedAITarget = value;
                UseSelectedCharacterSkill(_ai);
            }
        }

        public SquadController PlayerSquad { get; set; }
        public SquadController AiSquad { get; set; }

        public void FinishMove()
        {
            SelectedCharacter = null;
            SelectedTargets = null;
            // SelectedAITarget = null;
            OnMoveFinished?.Invoke();
        }

        public void UseSelectedCharacterSkill(string type)
        {
            if (type == _ai)
            {
                _selectedCharacter.SelectedCharacterSkill.UseAISkill(
                    _selectedCharacter,
                    _selectedAITarget,
                    FinishMove);
            }
            else if (type == _player)
            {
                if (_selectedCharacter != null &&
                _selectedCharacter.IsSelected &&
                _selectedTargets != null &&
                _selectedTargets.Count > 0)
                {
                    _selectedCharacter.SelectedCharacterSkill.UseSkill(
                        _selectedCharacter,
                        _selectedTargets,
                        FinishMove);
                }
            }

        }
    }
}
