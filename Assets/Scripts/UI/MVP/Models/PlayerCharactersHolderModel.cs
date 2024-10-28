using System;
using System.Linq;
using System.Collections.Generic;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class PlayerCharactersHolderModel : CharactersHolderModel
    {
        public override void Start()
        {
            Characters = GameController.
                Instance.
                SaveManager.LoadPlayerCharacterNames().
                characterDataWrappers.
                ToList();

            UpdateCharacters();
        }
    }
}
