using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour {

    public Collider2D col;
    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
        if ( body != null && col != null)
        {
            if(body.velocity.y > 0 )
            {
                col.enabled = false;
            }
            else
            {
                col.enabled = true;
            }

        }
        else
        {

            Debug.Log("Error on Ground collider");
        }

	}
}
