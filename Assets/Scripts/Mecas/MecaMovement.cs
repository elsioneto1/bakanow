using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecaMovement : MonoBehaviour {

    public float MecaSpeed;
    public float MecaSpeedThreshold;
    public float MecaSpeedIncrease;
    public float MecaSpeedMultiplier;

    MyInputs myInputs;
    Transform myTransform;

	// Use this for initialization
	void Start () {
        myInputs = GetComponent<MyInputs>();

        myTransform = transform;
	}
	

    public void GetXSpeed()
    {
        if (Mathf.Abs(myInputs.InputX) < 0.2f)
        {
            MecaSpeed += 0;
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



    // Update is called once per frame
    void Update()
    {
        GetXSpeed();
        myTransform.position += new Vector3(MecaSpeed,0,0) * MecaSpeedMultiplier * Time.deltaTime;
    }

}
