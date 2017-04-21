using UnityEngine;
using System.Collections;

using DragonBones;

public class IsAMecha : MonoBehaviour {

    public UnityArmatureComponent armature;

    public AnimationCurve SpriteBlink;
    
    public Material mechaMaterial; // to be set in inspector!
    public int Health = 100;
    public int BaseAttack = 10;
    public float Speed = 1;
    public float ActivationTime = 1;

    public float blinkAdd = 0.15f;
    public bool takingDamage = false;


	// Use this for initialization
	void Start () {
        // set the mecha part in relevant components
        IsADamager[] dmgrs = GetComponentsInChildren<IsADamager>();
        IsDamageable[] dmgbls = GetComponentsInChildren<IsDamageable>();
        for (int i = 0; i < dmgrs.Length; i++)
        {
            dmgrs[i].mechaMainPart = this;
        }
        for (int i = 0; i < dmgbls.Length; i++)
        {

            dmgbls[i].mechaMainPart = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(0);
        }
	}

    // Ouch
    public void TakeDamage(int dmg)
    {
     // damageable component is verifying if its taking damage already or not  
            StartCoroutine(BlinkSprite());
            Health -= dmg;
           
       
    }

    IEnumerator BlinkSprite()
    {
        float _time = 0;
        float _spriteBlinkValue;
        Color c = new Color(1,1,1);
        while (_time < 1)
        {
            yield return new WaitForEndOfFrame();
            _time += blinkAdd;
            if (_time > 1) _time = 1;
            _spriteBlinkValue = SpriteBlink.Evaluate(_time);



            c.r = 1;
            c.g = 1 - _spriteBlinkValue;
            c.b = 1 - _spriteBlinkValue;
            mechaMaterial.color = c;


        }
        takingDamage = false;
    }
}
