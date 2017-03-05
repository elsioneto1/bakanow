using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSwordActivate : RobotModule {

    public TiltPart tilt;

	// Use this for initialization
	void Start () {
        inputs = GetComponent<MyInputs_Meca>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Hitting()
    {
        if (inputs != null)
        {
            TimeWithouHit = HittingCoolDown;
            inputs.attackSword += 0.05f;
            if (tilt != null && inputs.attackSword < 0.9f)
                tilt.Tilt(inputs.attackSword);
            if (inputs.attackSword > 1)
            {
                inputs.attackSword = 1;
            }
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }

    public override void NotHitting()
    {

        if (inputs != null)
        {
            //Debug.Log("asdasd");

            inputs.attackSword -= 0.1f;
            if (inputs.attackSword < 0)
                inputs.attackSword = 0;
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }

}
