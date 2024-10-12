
using System.Runtime.CompilerServices;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    public Vector2 direction {get; private set;}
    public float speed=30f;
    public float maxBounceAngle = 60;
    public float minSpeed =3f;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update() 
    {
        //check if the player wants to move the paddle
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }
    private void FixedUpdate() 
    {
        //add force to move paddle in wanted direction
        if(this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce(this.direction * this.speed);
        }     
      
    }
    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f,this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }
    //handle case of hitting the ball with paddle
    // changing the direction and the speed of the padddle based on the area the ball hit on paddle
    private void OnCollisionEnter2D(Collision2D other) 
    {
        BallScript ball = other.gameObject.GetComponent<BallScript>();
        float offset, paddleWidth,angle,bounceAngle,newAngle;
        Vector3 paddlePosition;
        Vector2 contact;
        Quaternion rotate;
        if( ball != null)
        {
            paddlePosition = this.transform.position;
            contact = other.GetContact(0).point;
            offset = paddlePosition.x - contact.x;
            paddleWidth = other.otherCollider.bounds.size.x /2;
            angle =Vector2.SignedAngle(Vector2.up,ball.rigidbody.velocity);
            bounceAngle = (offset/ paddleWidth) * maxBounceAngle;
            newAngle = Mathf.Clamp(angle + bounceAngle,(-1)*maxBounceAngle,maxBounceAngle); 
            rotate = Quaternion.AngleAxis(newAngle,Vector3.forward);
            ball.rigidbody.velocity = rotate * Vector2.up * ball.rigidbody.velocity.magnitude;

             Vector2 ballVelocity = ball.rigidbody.velocity;

            if (ballVelocity.y > 0 && ballVelocity.y < minSpeed)
            {
                ballVelocity.y = minSpeed;
            }

            ball.rigidbody.velocity = ballVelocity*(1.1f);
             
        }
    }
    
}
