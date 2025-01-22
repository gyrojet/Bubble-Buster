using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;


    private Rigidbody2D theRB;


    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);


        if(Input.GetKey(left)){
            theRB.linearVelocity = new Vector2(-moveSpeed, theRB.linearVelocity.y);
        }
        else if(Input.GetKey(right))
        {
             theRB.linearVelocity = new Vector2(moveSpeed, theRB.linearVelocity.y);
        }
        else 
        {
            theRB.linearVelocity = new Vector2(0, theRB.linearVelocity.y);
        }

        if(Input.GetKeyDown(jump) && isGrounded)
        {
            theRB.linearVelocity = new Vector2(theRB.linearVelocity.x, jumpForce);
        }
    }

}