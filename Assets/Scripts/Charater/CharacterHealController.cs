using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterHealController : MonoBehaviour
    {

        public void Healing(CharacterController target,  float healValue)
        {
            Debug.Log(target.name + " я есть");
            target.Health += healValue;


        }
    }
}