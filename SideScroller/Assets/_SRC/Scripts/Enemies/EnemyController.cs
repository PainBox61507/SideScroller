using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace MiloZitare
{

    public class EnemyController : MonoBehaviour
    {

        [SerializeField] public List<AttackType> healthBarCombo = new List<AttackType> {};
        [SerializeField] public int currentHealth;
        
        //----------EVENTS-----------
        public delegate void DelegateOnReceiveDamage();
        public event DelegateOnReceiveDamage OnReceiveDamage;

        public delegate void DelegateOnDeath();
        public event DelegateOnDeath OnDeath;

        public struct AttackInfo
        {
            public bool wasEffective;
            public bool didKill;
        }
        AttackInfo attackInfo = new AttackInfo();


        void Awake()
        {
            currentHealth = healthBarCombo.Count;
            healthBarCombo.Reverse();
        }


        public AttackInfo RecieveDamage(AttackType _attackType)
        {
            if (currentHealth <= 0)
            {
                Die();
                attackInfo.wasEffective = true;
                return attackInfo;
            }

            if (_attackType.attackName == healthBarCombo[currentHealth - 1].attackName)
            {
                currentHealth -= 1;
                OnReceiveDamage.Invoke();
                if (currentHealth <= 0)
                {
                    Die();
                    attackInfo.didKill = true;
                }
                
                return attackInfo;
            }
            
                return attackInfo;
        }

        protected virtual void Die()
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            if(OnDeath != null && OnDeath.Target != null)
            {
                OnDeath.Invoke();
            }
        }
    }

   

}