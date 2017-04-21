using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class IsABigFucknFish : IsADamager {



    public const float maxTimeAlive = 4;
    float _timeAlive = 0;
    IsAProjectile projectile;

	// Use this for initialization
	void Start () {
        projectile = GetComponent<IsAProjectile>();
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
        _hit = false;
        projectile.SetUnactive();
    }

}
