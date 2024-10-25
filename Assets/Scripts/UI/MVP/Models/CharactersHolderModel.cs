using System;
using System.Collections.Generic;
using System.Linq;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CharactersHolderModel
    {
        public event Action OnCharactersUpdated;
        public List<CharacterDataWrapper> Characters { get; set; } =
            new List<CharacterDataWrapper>();
        
        public CharactersHolderModel()
        {
            Characters = GameController.
                Instance.
                SaveManager.
                CharacterDataWrapperHolder.
                CharacterDataWrappers.
                ToList();
            
            OnCharactersUpdated?.Invoke();
        }
    }
}
