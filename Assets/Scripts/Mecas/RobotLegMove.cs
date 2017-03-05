using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLegMove : RobotModule {



    bool active = false;

	// Use this for initialization
	void Start () {
        inputs = GetComponent<MyInputs_Meca>();
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {

            TimeWithouHit -= Time.deltaTime;
            if (TimeWithouHit < 0)
                active = false;

        }
        else
        {
       
                NotHitting();
        }
	}

    public override void Hitting()
    {
        if (inputs != null)
        {
            active = true;
            inputs.InputX += 0.05f;
            TimeWithouHit = HittingCoolDown;
            if (inputs.InputX > 1)
                inputs.InputX = 1;
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

            inputs.InputX -= 0.1f;
            if (inputs.InputX < 0)
                inputs.InputX = 0;
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }

}
