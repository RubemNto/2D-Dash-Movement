using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementDash : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float dashSpeed;
    private float dashTime;
    public float timeOfDash;
    float horizontal;
    bool lookingRight;
    public Rigidbody2D rb;
    public bool dash;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        dashTime = timeOfDash;     
    }

    // Update is called once per frame
    void Update()
    {
        if(!dash)
        {
            horizontal = Input.GetAxisRaw("Horizontal");//gets value of direction of player
        }
        if(horizontal > 0 && lookingRight == false)//check the facing of the player to flip to left and right
        {
            Flip();
        }else if(horizontal < 0 && lookingRight)
        {
            Flip();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))//press button to start dash
        {
            dash = true;
            dashTime = timeOfDash;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed*Time.fixedDeltaTime);
        }      
    }

    void FixedUpdate()
    {
        if(!dash)
        {
            horizontalMovement(speed);
        }else if(dash)
        {
            if(dashTime > 0)
            {
                dashMovement(dashSpeed);
                dashTime-=Time.fixedDeltaTime;
            }else if(dashTime <= 0)
            {
                dash = false;
            }
        }
    }

    void horizontalMovement(float default_Speed)
    {
        rb.velocity = new Vector2(horizontal*speed*Time.fixedDeltaTime,rb.velocity.y);                
    }

    void dashMovement(float speed_of_dash)
    {
        if(lookingRight)
        {
            rb.velocity = new Vector2(1f*dashSpeed*Time.fixedDeltaTime,rb.velocity.y);               
        }else if(!lookingRight)
        {
            rb.velocity = new Vector2(-1*dashSpeed*Time.fixedDeltaTime,rb.velocity.y);               
        }
    }

    void Flip()
    {
        lookingRight = !lookingRight;
        transform.Rotate(0,180,0);
    }
    
}
