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

            for (int i = 0; i < totalCharacters; i++)
            {
                var characterData = characterDataWrappers[i];
                if (_brainType == CharacterBrainType.AI)
                {
                    var hpBar = characterData.CharacterPrefab.transform.Find("HPBar");
                    var defBar = characterData.CharacterPrefab.transform.Find("DefBar");

                    hpBar?.Rotate(0, 180, 0);

                    defBar?.Rotate(0, 180, 0);
                }
                characterController = Instantiate(characterData.CharacterPrefab, transform);

                positionOffset = new Vector3(i * step - halfWidth, 1, 0);
                characterController.transform.localPosition = positionOffset;

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
