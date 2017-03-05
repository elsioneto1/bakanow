using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFishActivate : RobotModule {
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
            inputs.attackFish += 0.05f;
            if (tilt != null && inputs.attackFish < 0.9f)
                tilt.Tilt(inputs.attackFish);
            if (inputs.attackFish > 1)
            {
                inputs.attackFish = 1;
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

            inputs.attackFish -= 0.1f;
            if (inputs.attackFish < 0)
                inputs.attackFish = 0;
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }

}
