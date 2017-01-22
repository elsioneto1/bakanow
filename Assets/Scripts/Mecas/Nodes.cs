using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class Nodes : MonoBehaviour {


    public Character.PlayerType mechaType;
    public Character[] characters;
    public float hitRadius;
    public bool hittingFriend;
    public bool hittingEnemy;

    public UnityEvent HitStay;
    public UnityEvent NotHitting;

    public float TimeWithouHitThreshold = 2;
    public float HittingCoolDown = 0;

    public float ActivationPercentageIncreaseHit = 10;
    public float ActivationPercentageDecreaseHit = 10;
    public float ActivationPercentageDecreaseOnCooldown = 2;
    public float ActivationTotal = 0;
    public bool active;
	// Use this for initialization
	void Start () {
        characters = FindObjectsOfType<Character>();
	}
	
	// Update is called once per frame
	void Update () {

        VerifyHitting();

        if ( hittingFriend )
        {
            HittingCoolDown = TimeWithouHitThreshold;
        }
        else
        {

        }

        if (!hittingFriend && !hittingEnemy)
        {
            HittingCoolDown -= Time.deltaTime;
            if ( HittingCoolDown< 0)
            {
                HittingCoolDown = 0;
                ActivationTotal -= ActivationPercentageDecreaseOnCooldown;
            }
        }

        if ( hittingFriend)
        {
            Debug.Log("friend hit");
            ActivationTotal += ActivationPercentageIncreaseHit;
        }
        if (hittingEnemy )
        {
            Debug.Log("enemy hit");
            ActivationTotal -= ActivationPercentageDecreaseHit;
        }

        if (ActivationTotal < 0)
        {
            ActivationTotal = 0;
        }
        else if ( ActivationTotal > 100)
        {
            ActivationTotal = 100;
            active = true;
        }

	}

    public bool Hitting(Ray ray)
    {
        if ( ray.firing)
        {
            for (int j = 0; j < ray.points.Count; j++)
            {
                float distance =Vector3.Distance(new Vector3( ray.points[j].MyPosition.x,ray.points[j].MyPosition.y,0), 
                    new Vector3 (transform.position.x,transform.position.y,0));
                //Debug.Log(distance);
                if(distance < hitRadius)
                {
                    Debug.Log("HIT");
                    return true;
                }
            }
        }
        return false;
    }


    public void VerifyHitting()
    {
        hittingEnemy = false;
        hittingFriend = false;
        for (int i = 0; i < characters.Length; i++)
        {
            Ray ray = characters[i].GetComponent<Ray>();
            // enemy or friend?
            if (characters[i].playerType == mechaType)
            {
                if (Hitting(ray))
                {
                    hittingFriend = true;
                }
            }
            else
            {
                if (Hitting(ray))
                {
                    hittingEnemy = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, hitRadius);
    }



}
