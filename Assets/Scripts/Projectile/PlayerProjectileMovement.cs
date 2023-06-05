using System;
using UnityEngine;

namespace DefaultNamespace.Projectile
{
    public class PlayerProjectileMovement : RepeatMonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Rigidbody2D rigidbody;
        
        [SerializeField] private float speed = 10f; 
        [SerializeField] private float lifetime = 5f;
        private Vector2 directionMove;
        
        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadRigidbody2D();
            LoadPlayerMovement();
        }
        
        private void LoadRigidbody2D()
        {
            if (this.rigidbody != null) return;
            this.rigidbody = transform.parent.GetComponent<Rigidbody2D>();
            Debug.LogWarning(gameObject.name + "Load Rigidbody2D Component");
        }
        
        private void LoadPlayerMovement()
        {
            if (this.playerMovement != null) return;
            this.playerMovement = FindObjectOfType<PlayerMovement>();
            Debug.LogWarning(gameObject.name + "Load PlayerMovement Component");
        }
        
        void Start()
        {
            directionMove = GetDirectionMove();
            RotateFollowDirectionMove(directionMove);
            Destroy(transform.parent.gameObject, 5f);
        }

        private Vector2 GetDirectionMove()
        {
            if (playerMovement.GetIsFacingRight())
                return Vector2.right;
            return Vector2.left;
        }
        
        private void RotateFollowDirectionMove(Vector2 directionMove)
        {
            if (directionMove == Vector2.right) return;
            transform.parent.Rotate(0,0, 180);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            rigidbody.velocity = directionMove * speed;
        }
    }
}