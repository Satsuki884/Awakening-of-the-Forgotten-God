using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterHealController : MonoBehaviour
    {
        [SerializeField] private HPDefBarsController hpDefBarsController;
        public void Healing(CharacterController target,  float healValue)
        {
            if (target.Health + healValue > target.MaxHealth)
            {
                target.Health = target.MaxHealth;
            }
            else{
                target.Health += healValue;
            }


            hpDefBarsController.UpdateHealthBar(target.Health, target.MaxHealth);
        }
    }
}