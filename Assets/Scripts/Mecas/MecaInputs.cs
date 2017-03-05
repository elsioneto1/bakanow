using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MecaInputs : MonoBehaviour {

    public string DEBUG_MOVE_RIGHT_LEFT;
   // public string DEBUG_MOVE_LEFT;
    public string ATTACK_SWORD;
    public string ATTACK_CANNON;
    public string ATTACK_FISH;
    public string DEFEND;

    MyInputs_Meca inputs;


	// Use this for initialization
	void Start () {

        inputs = GetComponent<MyInputs_Meca>();

	}
	

	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.DEBUG)
        {
            inputs.InputX = Input.GetAxis(DEBUG_MOVE_RIGHT_LEFT);
            //inputs.attackCannon = Input.GetAxis(DEBUG_MOVE_RIGHT);
            //inputs.attackFish = Input.GetAxis(DEBUG_MOVE_RIGHT);
            //inputs.attackSword = Input.GetAxis(DEBUG_MOVE_RIGHT);
            //inputs.defend = Input.GetAxis(DEBUG_MOVE_RIGHT);
            // inputs.attackCannon = Input.GetAxis(DEBUG_MOVE_RIGHT);
        }
	}


}
