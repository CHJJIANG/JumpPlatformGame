using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float horizontalMove;
    public float speed = 8f;

    Rigidbody2D myBody;

    bool grounded = true;

    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 20f;
    public float jumpLimit = 30f;

    bool jump = false;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            myAnim.SetTrigger("jump_off");
            myAnim.SetBool("isJumping", true);
        }

        if(grounded == true)
        {
            myAnim.SetBool("isJumping", false);
        }
        else
        {
            myAnim.SetBool("isJumping", true);
        }

        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            myAnim.SetBool("isRunning", true);
        }
        else
        {
            myAnim.SetBool("isRunning", false);
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }

        if (myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
}
