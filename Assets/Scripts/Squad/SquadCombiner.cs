using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;
namespace AFG.Squad
{
    public class SquadCombiner : MonoBehaviour
    {
        [SerializeField] protected CharacterController[] _characterPrefabs;
    }
}
