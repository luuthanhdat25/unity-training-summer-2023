using System;
using UnityEngine;

namespace DefaultNamespace.Damage
{
    public abstract class DamageReceiver : MonoBehaviour
    {
        [SerializeField] private int health = 2;

        public void AddHealth(int value)
        {
            health += value;
        }

        public void Deduct(int value)
        {
            if (IsDead()) return;
            health -= value;
            if(IsDead()) OnDead();
        }

        private bool IsDead()
        {
            return health <= 0;
        }

        public abstract void OnDead();
        
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.transform.TryGetComponent<DamageSender>(out DamageSender damageSender))
            {
                Deduct(damageSender.GetDamage());
                damageSender.GetHit();
            }
            
            /*DamageSender damageSender = collider2D.transform.GetComponent<DamageSender>();
            if (damageSender != null)
            {
                Deduct(damageSender.GetDamage());
            }*/
        }
    }
}