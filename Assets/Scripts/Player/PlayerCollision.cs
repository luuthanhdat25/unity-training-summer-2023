using System;
using TMPro;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private bool isOnGround = false;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
            isOnGround = true;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }

    public bool GetIsOnGround() => this.isOnGround;
}
