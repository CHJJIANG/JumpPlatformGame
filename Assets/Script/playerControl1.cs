using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControl1 : MonoBehaviour
{
    float horizontalMove;
    public float speed = 10f;
    Rigidbody2D myBody;
    bool grounded = false;
    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 30f;
    public float jumpLimit = 100f;
    bool jump = false;
    int jumpCount = 0;
    public int maxJumpCount = 2;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && (grounded || jumpCount < maxJumpCount))
        {
            jump = true;
            jumpCount++;
        }
    }

    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.velocity = new Vector2(myBody.velocity.x, jumpLimit);
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


        Collider2D groundCollider = Physics2D.OverlapCircle(transform.position, castDist, LayerMask.GetMask("Ground"));

        if (groundCollider != null)
        {
            grounded = true;
            jumpCount = 0;
        }
        else
        {
            grounded = false;
        }


        ///  RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        ///Debug.DrawRay(transform.position, Vector2.down, Color.red);

        ///Debug.Log("hit.collider: " + hit.collider);

       /// if (hit.collider != null && hit.collider.CompareTag("ground"))
        {
           /// Debug.Log("hit");
          ///  grounded = true;
           /// jumpCount = 0;
        }
        ///else
        {
           /// grounded = false;
        }

        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("enemy"))
        {
            SceneManager.LoadScene("Lose");
        }

        if (other.collider.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Level2");
        }

        if (other.collider.CompareTag("Finish2"))
        {
            SceneManager.LoadScene("Win");
        }
    }

    
}