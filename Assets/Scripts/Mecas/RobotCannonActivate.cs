using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCannonActivate : RobotModule {

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
            inputs.attackCannon += 0.05f;
            if (tilt != null && inputs.attackCannon < 0.9f)
                tilt.Tilt(inputs.attackCannon);
            if (inputs.attackCannon > 1)
            {
                inputs.attackCannon = 1;
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

            inputs.attackCannon -= 0.1f;
            if (inputs.attackCannon < 0)
                inputs.attackCannon = 0;
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }



}
