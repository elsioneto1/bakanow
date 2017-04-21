using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class IAFishThrower : MonoBehaviour {

    public UnityArmatureComponent armature;

    public int AttackImpulse_X = 2000;
    public int AttackImpulse_Y = 0;
    bool _attacked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (armature != null)
        {

            if (armature.animation.GetState("fishsile") != null)
            {

                if (armature.animation.GetState("fishsile")._timeline._currentTime > 2.2f && !_attacked)
                {
                    _attacked = true;
                    
                    CannonBallManager.Instance.GetBigFucknFish().SetAttack(new Vector2(AttackImpulse_X,AttackImpulse_Y), transform.position);
                }
                else if (_attacked && armature.animation.GetState("fishsile")._timeline._currentTime < .2f)
                {
                    _attacked = false;
                }

            }
        }
	}
}
