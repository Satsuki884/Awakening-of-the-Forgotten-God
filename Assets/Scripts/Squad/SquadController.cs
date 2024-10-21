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

        public virtual void Initialization()
        {
            CharacterController characterController;

            CharacterBrain characterBrain;

            //Vector3 position;


            if (_brainType == CharacterBrainType.AI)
            {
                characterBrain = new CharacterAIBrain();
                //position = _points[].position;
            }
            else
            {
                characterBrain = new CharacterPlayerBrain();
                //position = _points[].position;
            }

            for(int i=0;i<_characterPrefabs.Length;i++)
            {
                //position = _points[i % _points.Count].position;
                //characterController = Instantiate(_characterPrefabs[i], _points[i % _points.Count].position, Quaternion.identity);
                characterController = Instantiate(_characterPrefabs[i], transform);
                characterController.transform.parent = transform;
                
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
