using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotModule : MonoBehaviour {


    public MyInputs_Meca inputs;


    public float TimeWithouHit = 2;
    public float HittingCoolDown = 2;

	// Use this for initialization
	void Start () {
        inputs = GetComponent<MyInputs_Meca>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public virtual void Hitting()
    {
      
    }

    public virtual void NotHitting()
    {

      
    }



}
