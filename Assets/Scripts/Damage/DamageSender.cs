using UnityEngine;

namespace DefaultNamespace.Damage
{
    public abstract class DamageSender : MonoBehaviour
    {
        [SerializeField] private int damage = 1;

        public int GetDamage() => damage;
        public abstract void GetHit();
    }
}