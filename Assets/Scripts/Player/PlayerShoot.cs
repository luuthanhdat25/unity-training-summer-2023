using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerShoot : RepeatMonoBehaviour
    {    
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private Transform projectilePrefab; 
        [SerializeField] private Transform firePoint; // 
        [SerializeField] private float fireRate = 0.5f; 
        private bool canShoot = true;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadInputManager();
        }

        private void LoadInputManager()
        {
            if (this.inputManager != null) return;
            this.inputManager = FindObjectOfType<InputManager>();
            Debug.LogWarning(gameObject.name + "Load InputManager Component");
        }

        private void Update()
        {
            if (inputManager == null) return;
            if (inputManager.GetFiringInput() && canShoot)
            {
                StartCoroutine(Shoot());
                playerAnimator.TriggerShootAnimation();
            }
        }

        private IEnumerator Shoot()
        {
            canShoot = false;
            yield return new WaitForSeconds(0.3f);
            Transform newProjectile = Instantiate(projectilePrefab);
            newProjectile.position = firePoint.position;
            yield return new WaitForSeconds(fireRate);
            canShoot = true; 
        }
    }
}