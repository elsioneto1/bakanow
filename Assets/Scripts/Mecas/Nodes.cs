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

    public bool active;

    // system management
    private bool setRays = false;
	// Use this for initialization
	void Start () {
        characters = FindObjectsOfType<Character>();
       

	}
	
	// Update is called once per frame
	void Update () {

        // system management 
        if ( Ray.INITIALIZED_ON_SCENE && !setRays)
        {
            setRays = true;
            for (int i = 0; i < Ray.MY_RAYS.Length; i++)
            {
                Ray.MY_RAYS[i].AddNodeToList(this);
            }
        }

       // VerifyHitting();



        if ( hittingFriend)
        {
            //Debug.Log("friend hit");
            active = true;
          
            if (HitStay != null)
                HitStay.Invoke();
        }

    


	}


    void LateUpdate()
    {
        hittingEnemy = false;
        hittingFriend = false;

    }


    public bool Hitting(Point p)
    {
        float distance = Vector3.Distance(new Vector3(p.SpawnPosition.x, p.SpawnPosition.y, 0),
            new Vector3(transform.position.x, transform.position.y, 0));




        if (distance < hitRadius)
        {
            p.hit = true;
            p.SpawnPosition = new Vector3(transform.position.x, transform.position.y, p.SpawnPosition.z);
            p.Direction = Vector3.zero;

            return true;
        }
        
          
        return false;
    }


    public bool VerifyHitting(Point p, Ray r)
    {
        hittingEnemy = false;
        hittingFriend = false;
        
        Character c = r.GetComponent<Character>();
        // enemy or friend?
        if (c.playerType == mechaType)
        {
            if (Hitting(p))
            {
                hittingFriend = true;
                return true;
            }
        }
        else
        {
            if (Hitting(p))
            {
                hittingEnemy = true;
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, hitRadius);
    }



}
