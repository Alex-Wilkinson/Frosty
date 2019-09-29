using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float speed = 4;
    public float runMultiplier = 1.5f;
    public float jumpForce = 6;
    public float fallMultiplier = 4;
    public float minJumpTime = 0.3f;

    float startOfJump;
    bool isGrounded = false;
    bool isFastFalling = false;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Platform")
        {
            isGrounded = true;
        }
    }


    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Platform")
        {
            isGrounded = false;
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            float moveBy = x * speed * runMultiplier;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
        else
        {
            float moveBy = x * speed;
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isFastFalling = false;
            startOfJump = Time.time;
        }
    }

    void FastFall()
    {
        if (Time.time - startOfJump >= minJumpTime && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)))
        {
            isFastFalling = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        if (isFastFalling)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        FastFall();
    }
}
