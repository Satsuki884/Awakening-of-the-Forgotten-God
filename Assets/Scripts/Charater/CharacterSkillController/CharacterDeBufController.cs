using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterDeBufController : MonoBehaviour
    {

        //TODO add different buf on 1-2 move
        public void TakeBuf(CharacterController target, int bufType, float bufValue)
        {
            if(bufType == 2) // def
            {
                Debug.Log("Got a def buf");
                target.Def += bufValue;
            } else if(bufType == 3) // atk
            {
                Debug.Log("Got a atk buf");
                target.Atk += bufValue;
            }


        }

        public void TakeDeBuf(CharacterController target, int bufType, float bufValue)
        {
            if (bufType == 2) // def
            {
                Debug.Log("Got a def debuf");

                target.Def = (target.Def >= bufValue) ? target.Def - bufValue : 0;
            }
            else if (bufType == 3) // atk
            {
                Debug.Log("Got a atk debuf");

                target.Atk = (target.Atk >= bufValue) ? target.Atk - bufValue : 0;
            }


        }
    }
}