using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControls : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public LayerMask wall;
    public Collider2D triggerCollider;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private float FlipCooldown = 0;
    private bool HasFliped;
    public int EnemyHealth;
    private Slider EnemyHealthBar;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyHealthBar = gameObject.GetComponentInChildren<Slider>();
        EnemyHealthBar.maxValue = EnemyHealth;
        EnemyHealthBar.value = EnemyHealth;
    }
    private void FixedUpdate()
    {
        if (triggerCollider.IsTouchingLayers(wall))
        {
            Flip();
        }
    }
    private void Update()
    {
        // Check if the enemy is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Move the enemy horizontally
        Move();

        // Check if the enemy should jump
        if (isGrounded)
        {
            Jump();
        }
        if (EnemyHealth <= 0) 
        {
            EnemyDeath();
        }
    }
    public void DamageEnemy(int DamageAmount)
    {
        EnemyHealth = EnemyHealth - DamageAmount;
        EnemyHealthBar.value = EnemyHealth;
    }
    private void Move()
    {
        float horizontalInput = isFacingRight ? 1f : -1f;
        Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip the enemy sprite based on direction
        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            Flip();
        }
    }
    private void Jump()
    {
        // Add vertical force to make the enemy jump
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Flip()
    {
        // Flip the enemy's facing direction
        if (FlipCooldown == 0 )
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            FlipCooldown = 1;
            Invoke("ResetFlip", 0.3f);
        }
    }
    private void ResetFlip()
    {
        FlipCooldown = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Sword"))
        {
            Debug.Log("EnemyHasTakenDamage");
            DamageEnemy(1);
            Debug.Log("EnemyHpIsNow" + EnemyHealth);
        }
    }
    public void EnemyDeath()
    {
        GameObject.Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with the player or other objects
        if (collision.gameObject.CompareTag("Player"))
        {
            // Implement your combat logic here (e.g., reducing player's health)
            // You may want to use events or other mechanisms to communicate with the player script.
        }
    }
}
