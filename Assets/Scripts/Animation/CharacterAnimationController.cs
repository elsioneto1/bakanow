using UnityEngine;
using System.Collections;

public class CharacterAnimationController : MonoBehaviour {

    Character charac;
    
    public Animator fronLeg;
    public Animator backLeg;
    public Animator body;
    public Animator arm;

    public int direction = 1;
    public bool jumping;

	// Use this for initialization
	void Start () {
	    charac = GetComponent<Character>();
	}
	

	// Update is called once per frame
	void Update () {



	    if ( charac != null)
        {

            if (!charac.grounded)
            {
                if (!jumping)
                {
                    jumping = true;
                    SetAnimations("Running", false);
                    SetAnimations("Idle", false);
                    SetTriggers("Jumping");
                }
            }
            else
            {




                if (Mathf.Abs(charac.InputX) > 0.5f || Mathf.Abs(charac.InputY) > 0.5f)
                {
                    jumping = false;
                    SetAnimations("Running", true);
                    SetAnimations("Idle", false);

                }
                else
                {
                    jumping = false;
                    SetAnimations("Running", false);
                    SetAnimations("Idle", true);
                }
            }
        }



        if (Mathf.Abs(charac.InputX )> 0.4f)
        {

            if (charac.InputX < -0.5f)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
        }
        transform.localScale = new Vector3(direction,1,1);
	}

    public void SetAnimations(string s, bool b = false)
    {
        if (b)
        {
            fronLeg.SetBool(s,b);
            backLeg.SetBool(s,b);
            body.SetBool(s,b);
            arm.SetBool(s, b);
        }
        else
        {
            fronLeg.SetBool(s, b);
            backLeg.SetBool(s, b);
            body.SetBool(s, b);
            arm.SetBool(s, b);
        }
        //else
        //{
        //    fronLeg.SetTrigger(s);
        //    backLeg.SetTrigger(s);
        //    body.SetTrigger(s);
        //    arm.SetTrigger(s);

        //}
    }
    public void SetTriggers(string triggerName)
    {
        backLeg.SetTrigger(triggerName);
        fronLeg.SetTrigger(triggerName);


    }

}
