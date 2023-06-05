using System;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private const string RAW_INPUT = "rawInput";
        private const string SHOOT_TRIGGER = "shootTrigger";

        private void Awake()
        {
            LoadAnimator();
        }

        private void LoadAnimator()
        {
            if (animator != null) return;
            animator = GetComponent<Animator>();
        }
        
        public void CheckRunAnimation(InputManager inputManager)
        {
            animator.SetFloat(RAW_INPUT, Mathf.Abs(inputManager.GetInputDirection()));
        }
        
        public void TriggerShootAnimation()
        {
            animator.SetTrigger(SHOOT_TRIGGER);
        }
    }
}