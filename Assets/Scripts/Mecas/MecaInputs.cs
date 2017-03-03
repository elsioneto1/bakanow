using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecaInputs : MonoBehaviour {

    public string DEBUG_MOVE_RIGHT;
    public string DEBUG_MOVE_LEFT;


    MyInputs inputs;
	// Use this for initialization
	void Start () {

        inputs = GetComponent<MyInputs>();

	}
	

	// Update is called once per frame
	void Update () {

        inputs.InputX = Input.GetAxis(DEBUG_MOVE_RIGHT);

	}


}
