using System;
using System.Collections.Generic;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CharactersHolderModel
    {
        public event Action<CharacterController> OnCharacterAdded;
        public event Action<CharacterController> OnCharacterRemoved;
        public List<CharacterController> Characters { get; private set; } =
            new List<CharacterController>();
        
        public CharactersHolderModel(string chatactersJson)
        {
            Characters = new List<CharacterController>();
        }
        
        public virtual void AddCharacter(CharacterController character)
        {
            Characters.Add(character);
            OnCharacterAdded?.Invoke(character);
        }
        
        public virtual void RemoveCharacter(CharacterController character)
        {
            Characters.Remove(character);
            OnCharacterRemoved?.Invoke(character);
        }
    }
}
