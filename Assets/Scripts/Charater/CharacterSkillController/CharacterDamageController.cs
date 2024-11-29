using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace AFG.Character
{
    public class CharacterDamageController : MonoBehaviour
    {

        [SerializeField] private HPDefBarsController hpDefBarsController;
        public event Action<CharacterController> OnQueueUpdated;
        public void TakeDamage(float i, CharacterController target)
        {
            Debug.Log("TakeDamage: " + i);
            if(target.Def > i)
            {
                target.Def -= i;
                
            }else if(target.Def < i && target.Def >0)
            {
                float temp = target.Def - i;
                target.Def = 0;
                target.Health -= temp;
            }else if(target.Health > i)
            {
                target.Health -= i;
            }else if (target.Health < i && target.Health > 0)
            {
                target.Health = 0;
                Destroy(target.gameObject);
                OnQueueUpdated?.Invoke(target);
            }
            hpDefBarsController.UpdateDefBar(target.Def, target.MaxDef);
            hpDefBarsController.UpdateHealthBar(target.Health, target.MaxHealth);

        }


        
    }

}
