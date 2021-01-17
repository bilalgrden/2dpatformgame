using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public int jumpSpeed;
    Rigidbody2D rb;
    Animator animator;

    public GameObject DeathScreen;

    protected Joystick joystick;

    bool canJump = true;
    bool faceRight = true;
    
    // All Code is down
    #region Codes
    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {

        float moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput*speed ,rb.velocity.y);

        if (moveInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }
        if(faceRight == true && moveInput < 0)
        {
            Flip();
        }else if (faceRight== false && moveInput > 0)
        {
            Flip();
        }
    }
    #endregion
    
    // Trigger anything code is down
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            canJump = true;
        }
        if(collision.transform.tag== "Death" || collision.transform.tag == "Engel")
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Jump()
    {
        if (canJump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpSpeed);
            canJump = false;
            FindObjectOfType<AudioManager>().Play("JumpVoice");
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    
    
}
