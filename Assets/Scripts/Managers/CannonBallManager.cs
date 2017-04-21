using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallManager : MonoBehaviour {

    public IsACannonBall[] cannonBalls; // store all the game cannonballs
    public IsABigFucknFish[] fishs; // store all the fishes (see you and thanks for the fishes!)
    public static CannonBallManager Instance;


	// Use this for initialization
	void Start () {
        Instance = this; 
		cannonBalls = FindObjectsOfType<IsACannonBall>();
        fishs = FindObjectsOfType<IsABigFucknFish>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IsACannonBall GetCannonBall()
    {
        IsACannonBall cb = null;

        for (int i = 0; i < cannonBalls.Length; i++)
        {
            if (!cannonBalls[i].GetComponent<IAttackAux>().GetActive())
                cb = cannonBalls[i];
        }

        return cb;
    }

    public IsABigFucknFish GetBigFucknFish()
    {
        IsABigFucknFish fish = null;

        for (int i = 0; i < cannonBalls.Length; i++)
        {
            if (!cannonBalls[i].GetComponent<IAttackAux>().GetActive())
                fish = fishs[i];
        }

        return fish;
    }

}
