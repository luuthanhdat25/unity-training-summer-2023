using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class LizardMovement : MonoBehaviour
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float forceJump = 4f;
        [SerializeField] private float timeDelayJump = 1f;
        [SerializeField] private float distanceToTarget = 0.5f;
        [SerializeField] private Animator lizzardAnimator;
        [SerializeField] private bool isMoveToA = true;
        private Rigidbody2D rigidbody2D;
        private const String IS_JUMP = "isJump";
        
        private RaycastHit2D raycastHit2D;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float raycastDistance = 1.3f;

        private bool isJumpAnimation = false;
        
        private void Awake()
        {
            if (rigidbody2D != null) return; 
            rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        }

        private void Update() => SetAnimator();

        private void SetAnimator() => this.lizzardAnimator.SetBool(IS_JUMP, isJumpAnimation);
        private void FixedUpdate() => Behaviour();

        private void Behaviour()
        {
            if (IsGroundedRayCast2D())
            {
                isJumpAnimation = false;
                float distancePlayerToTarget;
                Vector3 targetPosition;

                if (isMoveToA)
                {
                    targetPosition = pointA.position;
                    distancePlayerToTarget = Vector3.Distance(transform.parent.position, pointA.position);
                }
                else
                {
                    targetPosition = pointB.position;
                    distancePlayerToTarget = Vector3.Distance(transform.parent.position, pointB.position);
                }

                MoveToTargetPosition(targetPosition);
                CheckFlip(distancePlayerToTarget);
            }
        }
        
        public bool IsGroundedRayCast2D()
        {
            Vector2 originPosition = transform.parent.position;
            Vector2 direction = Vector2.down;
            raycastHit2D = Physics2D.Raycast(originPosition, direction, raycastDistance, groundLayer); 
        
            return (raycastHit2D.collider != null);
        }
        
        private void MoveToTargetPosition(Vector3 targetVector3) 
            => StartCoroutine(DelayJump(targetVector3));

        private IEnumerator DelayJump(Vector3 targetVector3)
        {
            yield return new WaitForSeconds(timeDelayJump);
            isJumpAnimation = true;
            Vector3 directMoveX = (targetVector3 - transform.parent.position).normalized;
            Vector3 directMoveY = Vector3.up;
            Vector3 directionMove = (directMoveX + directMoveY).normalized;
            rigidbody2D.velocity = directionMove * forceJump;
        }

        private void CheckFlip(float distancePlayerToTarget)
        {
            if (distancePlayerToTarget < distanceToTarget)
                Flip();
        }

        private void Flip()
        {
            isMoveToA = !isMoveToA;
            if (transform.parent.rotation.y == 0) transform.parent.Rotate(0, 180, 0);
            else  transform.parent.Rotate(0, -180, 0);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 originPosition = transform.parent.position;
            Vector3 targetPosition = originPosition + new Vector3(0f, -raycastDistance, 0f);
            Gizmos.DrawLine(originPosition, targetPosition);
        }
    }
}