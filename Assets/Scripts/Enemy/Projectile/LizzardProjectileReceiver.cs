using DefaultNamespace.Damage;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class LizzardProjectileReceiver : DamageReceiver
    {
        public override void OnDead()
        {
            Destroy(gameObject);
        }
    }
}