using DefaultNamespace.Damage;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class LizardDamageReceiver : DamageReceiver
    {
        public override void OnDead()
        {
            Destroy(gameObject);
        }
    }
}