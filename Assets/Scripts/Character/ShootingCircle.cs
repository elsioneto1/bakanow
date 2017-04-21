using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCircle : MonoBehaviour {

    // manages what the character can shoot

    IsShootable[] shootables; // everything will be set on start. If somethings needs to be added later, this has to be changed
    public List<IsShootable> shootablesInRange = new List<IsShootable>();

    Character character;
    Ray characRay;


	// Use this for initialization
	void Start () {
	    
        shootables  = FindObjectsOfType<IsShootable>();
        character   = GetComponent<Character>();
        for (int i = 0; i < shootables.Length; i++)
        {
            // register itself to the shootable
            shootables[i].AddSoootingCircleProperties(this); 
        }
        characRay = GetComponent<Ray>();
	}
	
	// Update is called once per frame
	void Update () {


        Debug.Log(characRay.aimingVector);
        for (int i = 0; i < shootables.Length; i++)
		{
            Vector3 displacement = shootables[i].transform.position - characRay.transform.position;
            float dot = Vector3.Dot(characRay.aimingVector, displacement.normalized) ;
            float dotToShootable = Vector3.Dot(displacement.normalized, shootables[i].properties[this].displacement.normalized);
            float dotToShootableExtension = Vector3.Dot(shootables[i].properties[this].displacement.normalized, shootables[i].properties[this].extendedDisplacement.normalized);

            Debug.Log(dot);

            Debug.Log(dotToShootable);
            Debug.Log(dotToShootableExtension);
            if ( dot < dotToShootable 
                &&
                dot > dotToShootableExtension)
            {
                Debug.Log("yee");
            }

		}
      
	}

    float CalculateVectorLength(Vector3 v)
    {
        float length = -1;

        Vector3 result = transform.position - v;
        Debug.Log(result.magnitude);

        return length;
    }

    // draw line to shootables
    void OnGUI()
    {

        for (int i = 0; i < shootables.Length; i++)
        {
            // measure the angle between two points based on my position
            Debug.DrawLine(transform.position, transform.position + characRay.aimingVector, Color.white);
             
        }
    }

}
