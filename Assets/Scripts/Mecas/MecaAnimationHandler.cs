using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;


public class MecaAnimationHandler : MonoBehaviour {

    public UnityArmatureComponent armature;
    public MyInputs_Meca mecaInputs;



	// Use this for initialization
	void Start () {
        mecaInputs = GetComponent<MyInputs_Meca>();
        armature.animation.Reset();
        armature.animation.Stop();

    }
	
	// Update is called once per frame
	void Update () {
		if (armature != null)
        {

            Walking();
            Defending();
            AttackingSword();
            AttackingCannon();
            AttackingFish();

        }
        else
        {
            Debug.Log("armature is null");
        }
	}


    public void Walking()
    {
        if (Mathf.Abs(mecaInputs.InputX) > 0.4f)
        {
            if (!armature.animation.isPlaying || armature.animation.lastAnimationName != "walk")
            {
                if (!armature.animation.isPlaying)
                    armature.animation.Play("walk", 1);
            }

            armature.animation.timeScale = Mathf.Abs(mecaInputs.InputX);
        }
        else if (armature.animation.lastAnimationName == "walk" && armature.animation.isPlaying)
        {
            armature.animation.timeScale = 0.4f;
        }
        else if (armature.animation.isCompleted)
        {
            if (armature.animation.lastAnimationName == "walk")
            {
                armature.animation.Reset();
                armature.animation.Stop();
            }

        }

    }

    public void Defending()
    {

        if (mecaInputs.defend == 1 && (armature.animation.lastAnimationName != "shield" || !armature.animation.isPlaying))
        {
            armature.animation.Stop();
            mecaInputs.defend = 0;
            armature.animation.Play("shield", 1);
            StartCoroutine(SlowShield());
        }

    }

    public void AttackingSword()
    {
        if (mecaInputs.attackSword == 1 && (armature.animation.lastAnimationName != "sword" || !armature.animation.isPlaying))
        {
            mecaInputs.attackSword = 0;

            armature.animation.Stop();
            armature.animation.Play("sword", 1);
         
        }
    }


    public void AttackingCannon()
    {
        if (mecaInputs.attackCannon == 1 && (armature.animation.lastAnimationName != "cannon" || !armature.animation.isPlaying))
        {
            mecaInputs.attackCannon = 0;

            armature.animation.Stop();
            armature.animation.Play("cannon", 1);

        }
    }

    public void AttackingFish()
    {
        if (mecaInputs.attackFish == 1 && (armature.animation.lastAnimationName != "fishsile" || !armature.animation.isPlaying))
        {
            mecaInputs.attackFish = 0;

            armature.animation.Stop();
            armature.animation.Play("fishsile", 1);

        }
    }


    IEnumerator SlowShield()
    {
        yield return new WaitForSeconds(0.3f);
        armature.animation.timeScale = 0.2f;
        yield return new WaitForSeconds(0.2f);
        armature.animation.timeScale = 1;

    }
}
