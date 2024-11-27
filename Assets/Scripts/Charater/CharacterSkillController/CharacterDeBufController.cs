using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterDeBufController : MonoBehaviour
    {
        public void TakeBuf(CharacterController target, int bufType, float bufValue)
        {
            if(bufType == 2) // def
            {
                target.Def += bufValue;
            } else if(bufType == 3) // atk
            {
                target.Atk += bufValue;
            }


        }

        public void TakeDeBuf(CharacterController target, int bufType, float bufValue)
        {
            if (bufType == 2) // def
            {

                target.Def = (target.Def >= bufValue) ? target.Def - bufValue : 0;
            }
            else if (bufType == 3) // atk
            {

                target.Atk = (target.Atk >= bufValue) ? target.Atk - bufValue : 0;
            }


        }
    }
}