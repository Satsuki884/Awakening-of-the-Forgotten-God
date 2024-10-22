using System;
using AFG.Squad;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CombatModel
    {
        public event Action<CharacterController> OnCharacterSelected;
        public event Action OnMoveFinished;
        
        private CharacterController _selectedCharacter;
        public CharacterController SelectedCharacter
        {
            get
            {
                return _selectedCharacter;
            }
            set
            {
                if (_selectedCharacter == null || 
                    !_selectedCharacter.Equals(value))
                {
                    _selectedCharacter = value;
                    OnCharacterSelected?.Invoke(_selectedCharacter);
                }
            }
        }
        
        public SquadController PlayerSquad { get; set; }
        public SquadController AiSquad { get; set; }

        public void FinishMove()
        {
            OnMoveFinished?.Invoke();
        }
    }
}
