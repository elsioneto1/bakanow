using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsASword : IsADamager {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override int GetDamage()
    {
        base.GetDamage();
        int totalDamage = 0;
        totalDamage = mechaMainPart.BaseAttack + myAttack;
        return totalDamage;
    }

}
