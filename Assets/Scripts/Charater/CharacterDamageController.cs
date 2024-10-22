using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AFG.Character
{
    public class CharacterDamageController : MonoBehaviour
    {
        public void TakeDamage(float i, CharacterController target)
        {
            if(target.Def > i)
            {
                //Debug.Log("target.Def > i");
                target.Def -= i;
            }else if(target.Def < i && target.Def >0)
            {
                //Debug.Log("target.Def < i && target.Def != 0");
                float temp = target.Def - i;
                target.Def = 0;
                target.Health += temp;
            }else if(target.Health > i)
            {
                //Debug.Log("target.Health > i");
                target.Health -= i;
            }else if (target.Health < i && target.Health > 0)
            {
                Debug.Log("Health = 0");
                target.Health = 0;
                Destroy(target, 2f);
            }

            Debug.Log("Target Def after damage: " + target.Def);
            Debug.Log("Target Health after damage: " + target.Health);
        }


    }

}
