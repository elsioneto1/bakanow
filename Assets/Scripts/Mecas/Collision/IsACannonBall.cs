using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsACannonBall :  IsADamager {


    public const float maxTimeAlive = 4;
    float _timeAlive = 0;
    IsAProjectile projectile;

	// Use this for initialization
    void Start() 
    {
      
        projectile = GetComponent<IsAProjectile>();

	}
	
	// Update is called once per frame
	void Update () {

    


	}

    public void SetAttack(Vector2 force,  Vector3 pos)
    {
        GetComponent<IAttackAux>().ActivateProjectileProperties(force, pos);



    }

    public override int GetDamage()
    {
 	     return base.GetDamage();
    }


    public override void HitEffect()
    {
        base.HitEffect();
        Debug.Log("hit effect");
        _hit = false;
        projectile.SetUnactive();
    }
}
