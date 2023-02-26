using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float horizontalMove;
    public float speed;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 2f;
    public float jumpLimit = 2f;

    bool jump = false;

    ///Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        ///myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = horizontalMove * speed;

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);


        horizontalMove = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        if(horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
            //myAnim.SetBool("isRunning", true);
        }
        else
        {
            ///myAnim.SetBool("isRunning", false);
        }

    }

    void FixedUpdate()
    {
        ///float moveSpeed = horizontalMove * speed;
        ///

        if (jump)
        {
            float jumpForce = jumpLimit * Time.deltaTime;

            myBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
            jump = false;
        }

        ///if (jump)
        {
            ///myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            ///jump = false;
        }

        if(myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }else if(myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);

        if(hit.collider != null && hit.transform.name == "ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        ////myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
}
