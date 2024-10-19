using System.Collections.Generic;
using AFG.Character;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.Squad
{
    public class SquadController : MonoBehaviour
    {
        [SerializeField] private CharacterBrainType _brainType;
        [SerializeField] protected CharacterController[] _characterPrefabs;
        
        protected List<CharacterController> _characters = new List<CharacterController>();
        public List<CharacterController> Characters => _characters;
        
        public bool IsActive { get; set; }

        public virtual void Initialization()
        {
            CharacterController characterController;

            CharacterBrain characterBrain;
            
            if(_brainType == CharacterBrainType.AI)
            {
                characterBrain = new CharacterAIBrain();
            }
            else
            {
                characterBrain = new CharacterPlayerBrain();
            }

            for(int i=0;i<_characterPrefabs.Length;i++)
            {
                characterController = Instantiate(_characterPrefabs[i], transform);
                
                //inject brain
                characterController.Initialization(characterBrain);
                
                _characters.Add(characterController);
            }
        }

        public virtual void SetActive(bool isActive)
        {
            IsActive = isActive;
        }   

        public virtual void SelectCharacter()
        {
            
        }
    }
}
