using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    private bool isFacingRight = true;
    private float FlipCooldown = 0;
    private bool HasFliped;
    public float speed;
    public LayerMask wall;
    public Collider2D triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      currentPoint = PointB.transform;
      anim.SetBool("isRunning", true);
    }
    
    private void FixedUpdate()
    {
        if (triggerCollider.IsTouchingLayers(wall))
        {
            Flip();
        }
    }
    private void ResetFlip()
    {
        FlipCooldown = 0;
    }
    private void Flip()
    {
        if(FlipCooldown == 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            FlipCooldown = 1;
            Invoke("ResetFlip", 0.3F);
        }
    }
    // Update is called once per frame
    void Update()
    {
       Vector2 point = currentPoint.position - transform.position;
       if (currentPoint == PointB.transform)
       {
           rb.velocity = new Vector2(speed, 0);
       }
       else
       {
           rb.velocity = new Vector2(-speed, 0);
       }

       if(Vector2.Distance(transform.position, currentPoint.position) < 0.5F && currentPoint == PointB.transform)
            {
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5F && currentPoint == PointA.transform)
        {
            currentPoint = PointB.transform;
        }
    }
}
