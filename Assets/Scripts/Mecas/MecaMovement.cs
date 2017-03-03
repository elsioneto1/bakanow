using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecaMovement : MonoBehaviour {

    public float MecaSpeed;
    public float MecaSpeedThreshold;
    public float MecaSpeedIncrease;

    MyInputs myInputs;


	// Use this for initialization
	void Start () {
        myInputs = GetComponent<MyInputs>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetXSpeed()
    {
        if (Mathf.Abs(myInputs.InputX) < 0.2f)
        {
            MecaSpeed += 0;
        }
        else if (((Mathf.Abs(myInputs.InputX) > 0.2f)) && (Mathf.Abs(myInputs.InputX) < 0.4f) && Mathf.Abs(MecaSpeed) < 0.3f)
        {

            MecaSpeed = myInputs.InputX < 0 ? -0.28f : 0.28f;
        }
        else if (Mathf.Abs(myInputs.InputX) > 0.4f)
        {

            if (MecaSpeed > myInputs.InputX)
            {


                MecaSpeed -= MecaSpeedIncrease;
                if (MecaSpeed < -MecaSpeedThreshold)
                    MecaSpeed = -MecaSpeedThreshold;
            }
            else
            {

                MecaSpeed += MecaSpeedIncrease;

                if (MecaSpeed > MecaSpeedThreshold)
                    MecaSpeed = MecaSpeedThreshold;
            }
        }
        else
            MecaSpeed = 0;


    }
}
