using System;
using DefaultNamespace;
using Player;
using UnityEngine;

public class PlayerMovement : RepeatMonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerAnimator playerAnimator;
    
    
    [SerializeField] private float moveSpeed = 5f;
    private bool isFacingRight = true;

    [SerializeField] private float jumpForce = 5f;
    
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastDistance = 1.3f;
    

    /*private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;
    [SerializeField] private float paddingLeftRayCast = 0.5f;
    [SerializeField] private float paddingRightRayCast = 0.5f;*/

    [SerializeField] private float overlapDistance = 0.9f;
    [SerializeField] private float overlapRadius = 0.5f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadInputManager();
        LoadRigidbody2D();
        LoadPlayerCollision();
    }
    
    private void LoadInputManager()
    {
        if (this.inputManager != null) return;
        this.inputManager = FindObjectOfType<InputManager>();
        Debug.LogWarning(gameObject.name + "Load InputManager Component");
    }

    private void LoadRigidbody2D()
    {
        if (this.rigidbody != null) return;
        this.rigidbody = transform.parent.GetComponent<Rigidbody2D>();
        Debug.LogWarning(gameObject.name + "Load Rigidbody2D Component");
    }
    
    private void LoadPlayerCollision()
    {
        if (this.playerCollision != null) return;
        this.playerCollision = transform.parent.GetComponent<PlayerCollision>();
        Debug.LogWarning(gameObject.name + "Load PlayerCollision Component");
    }

    private void Update()
    {
        if (inputManager == null) return;
        playerAnimator.CheckRunAnimation(inputManager);
        ChangeRotation();
    }
    
    private void ChangeRotation()
    {
        if (inputManager.GetInputDirection() < 0 && isFacingRight)
            FlipPlayer();
        else if (inputManager.GetInputDirection() > 0 && !isFacingRight)
            FlipPlayer();
    }
    
    private void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        //SwapRaycastPadding();
        if (transform.parent.rotation.y == 0) transform.parent.Rotate(0, 180, 0);
        else  transform.parent.Rotate(0, -180, 0);
    }

    /*private void SwapRaycastPadding()
    {
        (paddingLeftRayCast, paddingRightRayCast) = (paddingRightRayCast, paddingLeftRayCast);
    }*/

    private void FixedUpdate()
    {
        if (inputManager == null) return;
        MoveHorizontal();
        Jump();
    }

    private void MoveHorizontal()
    {
        Vector2 movement = rigidbody.velocity;
        movement.x = inputManager.GetInputDirection() * moveSpeed;
        rigidbody.velocity = movement;
    }

    private void Jump()
    {
        if (!IsGroundedOverLap() || !inputManager.GetJumpInput()) return;
        rigidbody.AddForce(Vector2.up * jumpForce);
    }
    
    //Using Overlap
    public bool IsGroundedOverLap()
    {
        Vector2 originPosition = transform.parent.position;
        originPosition.y -= overlapDistance;
        Collider2D colliders = Physics2D.OverlapCircle(originPosition, overlapRadius, groundLayer);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 originPosition = transform.parent.position;
        originPosition.y -= overlapDistance;
        Gizmos.DrawWireSphere(originPosition, overlapRadius);
    }
    
    //Using Raycast
    /*public bool IsGroundedRayCast2D()
    {
        Vector2 originPosition = transform.parent.position;
        Vector2 positionLeftHit = originPosition;
        positionLeftHit.x -= paddingLeftRayCast;
        Vector2 positionRightHit = originPosition;
        positionRightHit.x += paddingRightRayCast;
        Vector2 direction = Vector2.down;
        
        hitLeft = Physics2D.Raycast(positionLeftHit, direction, raycastDistance, groundLayer); 
        hitRight = Physics2D.Raycast(positionRightHit, direction, raycastDistance, groundLayer); 
        
        return (hitLeft.collider != null) || (hitRight.collider != null);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 originPosition = transform.parent.position;
        Gizmos.DrawLine(originPosition + new Vector3(-paddingLeftRayCast,0f,0f), originPosition + new Vector3(-paddingLeftRayCast, -raycastDistance, 0));
        Gizmos.DrawLine(originPosition + new Vector3(paddingRightRayCast,0f,0f), originPosition + new Vector3(paddingRightRayCast, -raycastDistance, 0));
    }*/

    public bool GetIsFacingRight() => this.isFacingRight;
}
