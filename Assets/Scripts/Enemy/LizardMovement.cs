using System;
using UnityEngine;

namespace DefaultNamespace.Enemy
{
    public class LizardMovement : MonoBehaviour
    {
        [SerializeField] private Transform pointA;
        [SerializeField] private Transform pointB;
        [SerializeField] private float speedMove = 4f;
        private Rigidbody2D rigidbody2D;
        private bool isMoveToA = true;
        private float distancePlayerToTarget;
        private void Awake()
        {
            if (rigidbody2D != null) return; 
            rigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            Behaviour();
        }

        private void Behaviour()
        {
            if (isMoveToA)
            {
                MoveToTargetPosition(pointA.position);
                distancePlayerToTarget = Vector3.Distance(transform.parent.position, pointA.position);
                if (distancePlayerToTarget < 0.3) Flip();
            }
            else
            {
                MoveToTargetPosition(pointB.position);
                distancePlayerToTarget = Vector3.Distance(transform.parent.position, pointB.position);
                if (distancePlayerToTarget < 0.3) Flip();
            }
        }

        private void Flip()
        {
            isMoveToA = !isMoveToA;
            if (transform.parent.rotation.y == 0) transform.parent.Rotate(0, 180, 0);
            else  transform.parent.Rotate(0, -180, 0);
        }


        private void MoveToTargetPosition(Vector3 targetVector3)
        {
            Vector3 directMove = targetVector3 - transform.parent.position;
            rigidbody2D.velocity = directMove.normalized * speedMove;
        }
    }
}