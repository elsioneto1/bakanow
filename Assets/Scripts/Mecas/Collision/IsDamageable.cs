using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDamageable : MonoBehaviour {

    // IsAMecha.cs script sets this on its initialization 
    private IsAMecha _mecha;
    public IsAMecha mechaMainPart{
        get { return _mecha; }
        set { if ( _mecha == null)_mecha = value; } // adds a little bit of protection
    }
    public List<IsADamager> damagersActive = new List<IsADamager>();


	// Use this for initialization
	void Start () {
		
	}
	


	// Update is called once per frame
	void Update () {
        // keeps track of what hit the damageable
        for (int i = 0; i < damagersActive.Count; i++)
        {
            if (damagersActive[i] == null || !damagersActive[i].hit  )
            {
                damagersActive.RemoveAt(i); 
            }
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("ITS REAL");
         if (!mechaMainPart.takingDamage)
        {
            IsADamager dmgr = col.gameObject.GetComponent<IsADamager>();
            if (dmgr != null) 
            {
                if (dmgr.mechaMainPart != mechaMainPart && !damagersActive.Contains(dmgr)) // Am I hitting myself? Had this hit me before in the same attack?
                {
                    Debug.Log("Dealing damage");
                    dmgr.SetHit();
                    damagersActive.Add(dmgr);
                    mechaMainPart.takingDamage = true;
                    CallDamage(dmgr);
                    
                }

            }
        }
    }

    // sets anything that needs to be set before dealing damage
    public void CallDamage(IsADamager dmgr)
    {

        mechaMainPart.TakeDamage(dmgr.GetDamage());
        dmgr.HitEffect();

    }

   
}
