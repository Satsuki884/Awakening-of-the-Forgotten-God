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
            //Debug.Log("Got a speed buf");
            if(bufType == 1) //speed
            {
                Debug.Log("Got a speed buf");
                target.Speed += bufValue;
            }else if(bufType == 2) // def
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
            if (bufType == 1) //speed
            {
                Debug.Log("Got a speed debuf");

                target.Speed = (target.Speed >= bufValue) ? target.Speed - bufValue : 0;

            }
            else if (bufType == 2) // def
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