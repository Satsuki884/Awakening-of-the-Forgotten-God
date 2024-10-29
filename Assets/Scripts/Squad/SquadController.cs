using System.Collections.Generic;
using AFG.Character;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.Squad
{
    public class SquadController : MonoBehaviour
    {
        [SerializeField] private CharacterBrainType _brainType;
        
        protected List<CharacterController> _characters = new List<CharacterController>();
        public List<CharacterController> Characters => _characters;

        //[SerializeField] private List<Transform> _points = new List<Transform>();
        //[SerializeField] private List<Transform> _pointsAI = new List<Transform>();

        public bool IsActive { get; set; }


        public virtual void Initialization(List<CharacterDataWrapper> characterDataWrappers)
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
            int totalCharacters = characterDataWrappers.Count;
            float halfWidth = (totalCharacters - 1) * step / 2;

            for (int i=0;i<totalCharacters;i++)
            {
                var characterData = characterDataWrappers[i];
                characterController = Instantiate(characterData.CharacterPrefab, transform);

                positionOffset = new Vector3(i * step - halfWidth, 1, 0);
                characterController.transform.localPosition = positionOffset;
                if (_brainType == CharacterBrainType.AI)
                {
                    characterController.transform.eulerAngles = new Vector3(0, 180, 0); // ������� �� 180 ��������
                }

                //inject brain
                characterController.Initialization(characterBrain, characterDataWrappers[i]);
                
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
