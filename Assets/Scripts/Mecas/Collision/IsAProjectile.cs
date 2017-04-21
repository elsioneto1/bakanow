using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAProjectile : MonoBehaviour,IAttackAux {

    public bool isActive;
    BoxCollider2D boxCollider;
    Rigidbody2D rBody;
    SpriteRenderer renderer;



    public const float maxTimeAlive = 4;
    float _timeAlive = 0;

	// Use this for initialization
	void Start () {
       // renderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        renderer.enabled = false;
        boxCollider.enabled = false;
        rBody.simulated = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive && boxCollider.enabled)
        {
         //   renderer.enabled = false;
            boxCollider.enabled = false;
            rBody.simulated = false;
            renderer.enabled = false;

        }


        if (isActive)
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > maxTimeAlive)
            {
                
                Debug.Log("going to rest");
                SetUnactive();
            }
        }
	}


    public void SetUnactive()
    {
        _timeAlive = 0;
        isActive = false;
    }

    // from the IAuxAttack interface 
    public void ActivateProjectileProperties(Vector2 force, Vector3 pos)
    {

        isActive = true;
        renderer.enabled = true;

        boxCollider.enabled = true;
        rBody.simulated = true;
        rBody.velocity = new Vector2(0,0);
        rBody.AddForce(force);
        transform.position = pos;

    }

    // from the IAuxAttack interface 
    public bool GetActive()
    {
        return isActive;
    }

 
}
