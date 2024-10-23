using System;
using System.Collections.Generic;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class PlayerCharactersHolderModel : CharactersHolderModel
    {
        public PlayerCharactersHolderModel(string chatactersJson) : base(chatactersJson)
        {
        }
    }
}
