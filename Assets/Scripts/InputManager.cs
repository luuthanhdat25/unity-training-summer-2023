using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        private float moveVectorHorizontal; 
        private bool jumpInput;
        private bool firingInput;

        private void Update()
        {
            moveVectorHorizontal = Input.GetAxis("Horizontal");
            jumpInput = Input.GetAxis("Jump") > 0;
            firingInput = Input.GetButtonDown("Fire1");
        }

        public float GetInputDirection() => moveVectorHorizontal;
        public bool GetJumpInput() => jumpInput;
        public bool GetFiringInput() => firingInput;
    }
}