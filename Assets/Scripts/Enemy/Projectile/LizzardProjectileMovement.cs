using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class LizzardProjectileMovement : RepeatMonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Rigidbody2D rigidbody;
        
        [SerializeField] private float speed = 10f; 
        [SerializeField] private float lifetime = 4f;
        private Vector2 directionMove;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadRigidbody2D();
        }
        
        private void LoadRigidbody2D() => this.rigidbody ??= transform.parent.GetComponent<Rigidbody2D>();

        void Start()
        {
            directionMove = GetDirectionMove();
            Destroy(transform.parent.gameObject, lifetime);
        }

        private Vector2 GetDirectionMove()
        {
            if (playerMovement.GetIsFacingRight())
                return Vector2.right;
            return Vector2.left;
        }

        private void FixedUpdate() => Move();

        private void Move()
        {
            rigidbody.velocity = directionMove * speed;
        }

        public void GetDirectionMove(Vector3 direction) => this.directionMove = direction;
    }
}