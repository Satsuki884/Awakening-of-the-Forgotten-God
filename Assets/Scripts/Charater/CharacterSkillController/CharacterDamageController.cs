using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace AFG.Character
{
    public class CharacterDamageController : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        [SerializeField] private SpriteRenderer _hpSriteRenderer;
        
        private float _viewMaxHealth;
        private float _viewMaxXLocalPosition = 1; 
        
        private void Awake()
        {
            _viewMaxHealth = _hpSriteRenderer.size.x;
            _viewMaxXLocalPosition = _hpSriteRenderer.transform.localPosition.x;
        }

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
                //Debug.LogError("Health "+ target.name+ " = 0");
                target.Health = 0;
                Destroy(target, 2f);
            }

            //Debug.Log(target.name + " Def after damage: " + target.Def);
            //Debug.Log(target.name + " Health after damage: " + target.Health);
        }


        private void Update()
        {
            ChangeHP(_health);
        }
        
        private void ChangeHP(float health)
        {
            var currentViewHealth = (health * _viewMaxHealth) / _maxHealth;
            var currentViewXLocalPosition = (health * _viewMaxXLocalPosition) / _maxHealth;

            currentViewHealth = Mathf.Clamp(currentViewHealth, 0, _viewMaxHealth);
            currentViewXLocalPosition = Mathf.Clamp(currentViewXLocalPosition, 0, _viewMaxXLocalPosition);

            _hpSriteRenderer.size = new Vector2(currentViewHealth, _hpSriteRenderer.size.y);
            _hpSriteRenderer.transform.localPosition = new Vector3(currentViewXLocalPosition, _hpSriteRenderer.transform.localPosition.y, _hpSriteRenderer.transform.localPosition.z);
        }
    }

}
