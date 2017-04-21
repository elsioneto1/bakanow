using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsADamager : MonoBehaviour {

    private IsAMecha _mecha;
    public MecaAnimationHandler mecaAnimationHandler;

    public IsAMecha mechaMainPart
    {
        get { return _mecha; }
        set { if (_mecha == null)_mecha = value; } // adds a little bit of protection
    }

    public int myAttack;

    protected bool _hit;
    public bool hit
    {
        get { return _hit; }   
    }

	// Use this for initialization
	void Start () {
		if (mecaAnimationHandler != null)
        {

        }
        else
        {
            Debug.Log("mecaAnimationHandler is null at " + name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual int GetDamage()
    {
        // if the calculation it's considering the damage, it already hit
        _hit = true;
        return 0;
    }

    public void SetHit()
    {
        
        _hit = true;
    }

   
    public virtual void HitEffect()
    {


    }


}
