using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltPart : MonoBehaviour {


    public Transform myTransofrm;
    float originalZ = 0;

    public float desiredTiltAngle;
    // up and down
    public float randomRange = 15;
    public bool tilting;

    float _time = 0;
    float increment = 0.2f;

    Vector3 newEuler; 
    Vector3 oldEuler;

    public bool SimulateTilt = false;

	// Use this for initialization
	void Start () {
        myTransofrm = transform;
        originalZ = transform.localRotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {
		
        if (SimulateTilt)
        {
            SimulateTilt = false;
            Tilt(1);
        }

        if (tilting)    
        {


            _time += increment;
            if (_time > 1)
            {
                tilting = false;
              
                _time = 1;
            }
            Vector3 euler = Vector3.Lerp(oldEuler,newEuler,_time); ;
              originalZ = euler.z;
            myTransofrm.rotation = Quaternion.Euler(euler);
        }
        else
        {

            _time = 0;
        }

	}

    public void Tilt(float Intensity)
    {
        if (!tilting)
        {
            desiredTiltAngle = Random.Range(0, randomRange) * Intensity;
            desiredTiltAngle += myTransofrm.localRotation.z;
            newEuler = myTransofrm.localRotation.eulerAngles;
            oldEuler = newEuler;
            newEuler.z = desiredTiltAngle;
            oldEuler.z = originalZ;
            _time = 0;
           
            tilting = true;
        }
    }

    public void EndTilt()
    {


    }
}
