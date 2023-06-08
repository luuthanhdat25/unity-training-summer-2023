using DefaultNamespace.Damage;
using UnityEngine;

namespace DefaultNamespace.Projectile
{
    public class PlayerProjectileDamageSender : DamageSender
    {
        public override void GetHit()
        {
            Debug.Log("Hieu Ung");
        }
    }
}