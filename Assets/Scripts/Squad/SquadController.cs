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

        //[SerializeField] private List<Transform> _points = new List<Transform>();
        //[SerializeField] private List<Transform> _pointsAI = new List<Transform>();

        public bool IsActive { get; set; }

        [SerializeField] private GameObject targetObject;

        public virtual void Initialization()
        {
            CharacterController characterController;

            CharacterBrain characterBrain;

            if (_brainType == CharacterBrainType.AI)
            {
                characterBrain = new CharacterAIBrain();
            }
            else
            {
                characterBrain = new CharacterPlayerBrain();
            }

            float step = 6f;
            Vector3 positionOffset;
            int totalCharacters = _characterPrefabs.Length;
            float halfWidth = (totalCharacters - 1) * step / 2;

            for (int i=0;i<_characterPrefabs.Length;i++)
            {
                characterController = Instantiate(_characterPrefabs[i], transform);

                positionOffset = new Vector3(i * step - halfWidth, 1, 0);
                characterController.transform.localPosition = positionOffset;

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
