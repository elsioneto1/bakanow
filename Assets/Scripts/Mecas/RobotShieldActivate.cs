using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotShieldActivate : RobotModule {


    public TiltPart tilt;


 
    // Use this for initialization
    void Start()
    {
        inputs = GetComponent<MyInputs_Meca>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( TimeWithouHit > 0)
        {
            TimeWithouHit -= Time.deltaTime;
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
            TimeWithouHit = HittingCoolDown;
            inputs.defend += 0.05f;
            if (tilt != null && inputs.defend < 0.9f)
                tilt.Tilt(inputs.defend);
            if (inputs.defend > 1)
            {
                inputs.defend = 1;
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

            inputs.defend -= 0.1f;
            if (inputs.defend < 0)
                inputs.defend = 0;
        }
        else
        {
            Debug.Log("No Inputs component registered");
        }
    }



}
