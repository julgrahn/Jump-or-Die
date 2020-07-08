using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    private bool isJumping = false;
    public float jumpVelocity = 10f;

    [SerializeField] private LayerMask platformsLayerMask;
    private PlayerMovement playerMovement;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;

    void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        PlayerJump();
    }

    public void PlayerJump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }

        if(other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = other.transform;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }

        if(other.gameObject.CompareTag("Platform"))
        {
            player.transform.parent = null;
            isJumping = true;
        }
    }

    private bool IsGrounded()
    {
        
        float extraHeightText = .1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformsLayerMask);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        //Debug.Log(raycastHit.collider != null);
        return raycastHit.collider != null;
    }
}
