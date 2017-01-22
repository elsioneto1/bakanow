using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {



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



    bool negativeVelocity;
	// Use this for initialization
	void Start () {

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


        Vector2 inputRight = new Vector2(rightStickX,rightStickY);
        Vector2 inputDis = inputRight.normalized;


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
        

        ray.angle = angle;
        if (grounded)
        {
            transform.position += new Vector3(InputX * 0.2f, 0, 0);
        }
        else
        {
            transform.position += new Vector3(jumpingX,0,0);
            // is he flying to the left or right?
            bool isLeft = jumpingX > 0 ? false : true;
            if  (isLeft)
            {
                if (InputX > 0.5f)
                {
                    jumpingX = jumpingX * 0.95f;
                   // if (jumpingX < -0.05f)
                      //  jumpingX = -0.05f;
                }
            }
            else
            {
                 if (InputX < -0.5f)
                 {
                     jumpingX = jumpingX * 0.95f;
                   //  if (jumpingX > 0.05f)
                       //  jumpingX = 0.05f;
                 }
            }
        }


        if ( jumping > 0)
        {
            if ( grounded )
            {
                grounded = false;
                body.velocity = new Vector3(body.velocity.x, jumpForce);
                jumpingX = InputX * 0.15f ;
            }

        }

        if (!grounded)
        {
            float isUp = body.velocity.y > 0? 1 : -1;
            
            body.AddForce(new Vector2(0, body.velocity.y * addForceOnJump  * isUp));

        }


	}


    public void OnCollisionEnter2D(Collision2D col)
    {

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
