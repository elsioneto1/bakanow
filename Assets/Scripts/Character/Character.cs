using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    CharacterAnimationController characAnim;

    public enum PlayerType { p1, p2 };

    public PlayerType playerType;

    Ray ray;

    public float InputX;
    public float InputY;
    string inputModifier;

    public string jumpingKey;

    public string movementX;

    public string rightStickHorizontal;
    public string rightStickVertical;
    public string FiringKey;

    public float rightStickX;
    public float rightStickY;

    public bool grounded = true;
    public bool onAir;
    public float jumping;
    public bool firing;

    public float angle;
    public float jumpingX;

    Rigidbody2D body;

    public float jumpForce = 8;
    public float addForceOnJump;
   // [HideInInspector]
    public float lerpingSpeed;
    public float lerpingSpeedThreshold = 0.3f;
    public float lerpingSpeedVelocity = 0.01f;
    public float speedMultiplier = 5;

    bool negativeVelocity;
	// Use this for initialization
    public void GetXSpeed()
    {
        if (Mathf.Abs(InputX) < 0.4f && !grounded)
        {
            lerpingSpeed += 0;
        }
        else if (((Mathf.Abs(InputX) > 0.2f)) && (Mathf.Abs(InputX) < 0.4f) && Mathf.Abs(lerpingSpeed) < 0.3f)
        {

            lerpingSpeed = InputX < 0 ? -0.28f : 0.28f;
        }
        else if (Mathf.Abs(InputX) > 0.4f)
        {

            if (lerpingSpeed > InputX)
            {


                lerpingSpeed -= lerpingSpeedVelocity;
                if (lerpingSpeed < -lerpingSpeedThreshold)
                    lerpingSpeed = -lerpingSpeedThreshold;
            }
            else
            {

                lerpingSpeed += lerpingSpeedVelocity;

                if (lerpingSpeed > lerpingSpeedThreshold)
                    lerpingSpeed = lerpingSpeedThreshold;
            }
        }
        else
            lerpingSpeed = 0;


    }

	void Start () {

        characAnim = GetComponent<CharacterAnimationController>();

	    if ( playerType == PlayerType.p1)
        {
            inputModifier = "P1";
        }
        else
        {
            inputModifier = "P2";
        }

        body = GetComponent<Rigidbody2D>();

        movementX = "Horizontal" + inputModifier;
        jumpingKey = "Jump" + inputModifier;
        rightStickHorizontal = "RightHorizontal" + inputModifier;
        rightStickVertical = "RightVertical" + inputModifier;
        FiringKey = "Fire" + inputModifier; 

        ray = GetComponent<Ray>();

	}
    bool lockImpulse = false;
	// Update is called once per frame
	void Update () {
	    
        InputX = Input.GetAxis(movementX);
        jumping = Input.GetAxis(jumpingKey);
        rightStickX = Input.GetAxis(rightStickHorizontal);
        rightStickY = -Input.GetAxis(rightStickVertical);
        
        int angleNegative = rightStickY < 0? -1 : 1;

       
        if (Input.GetAxis(FiringKey) > 0)
        {
            firing = true;
        }
        else
        {
            firing = false;
        }


        Vector2 inputRight = new Vector2(rightStickX, rightStickY);
        Vector2 inputDis = inputRight.normalized *characAnim.direction;

        GetXSpeed();

        if (!ray.overheated)
        {
            ray.firing = firing;
        }
        else
        {
            ray.firing = false;
        }
        Vector2 right = new Vector2(1,0);
        angle = Vector2.Angle(right,inputDis);
        angle *= angleNegative;
        // angle *= transform.localScale.x;

        int mul = 1;
        if (lerpingSpeed < 0)
            mul = -1;

        float _xSpeed = ((lerpingSpeed * lerpingSpeed) * speedMultiplier) * Time.deltaTime * mul;

        ray.angle = angle * transform.localScale.x;
       

        //moving the character on X
        //if (!grounded)
        {
            transform.position += new Vector3(_xSpeed, 0, 0);
        }
        //else
        {

        }
    

        // on jump
        if ( jumping > 0)
        {
            if ( grounded )
            {
                grounded = false;
                body.velocity = new Vector3(body.velocity.x, jumpForce);
                //jumpingX = InputX * 0.15f ;
            }

        }

        if (!grounded)
        {
            float isUp = body.velocity.y > 0? 1 : -1;
            
            body.AddForce(new Vector2(0, body.velocity.y * addForceOnJump  * isUp));

        }

        if ( Mathf.Abs( body.velocity.y ) > 0.1f)
        {
            if (!lockImpulse)
            {
                lockImpulse = true;
                grounded = false;
                jumpingX = _xSpeed;
            }
        }

	}


    public void OnCollisionEnter2D(Collision2D col)
    {
        lockImpulse = false;
        grounded = true;
        float isUp = body.velocity.y > 0? 1 : -1;
        if (body.velocity.y  <= 0)
        {
            if (col.gameObject.GetComponent<IsGround>() != null)
            {

                grounded = true;

            }
        }
       

    }


}
