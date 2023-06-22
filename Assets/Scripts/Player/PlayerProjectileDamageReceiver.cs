using DefaultNamespace.Damage;
using UnityEngine;

namespace DefaultNamespace.Projectile
{
    public class PlayerProjectileDamageReceiver : DamageReceiver
    {
        public override void OnDead()
        {
            Destroy(gameObject);
        }
    }
}