using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsShootable : MonoBehaviour {

    float angleToCharacter;
    public float angleRange = 10;
    // based on the vector length, the importance is escalated
    public float vectorLenght;

    bool isVisible = false;
    public List<ShootingCircle> circles = new List<ShootingCircle>();
    
    public Dictionary<ShootingCircle, ShootableProperties> properties = new Dictionary<ShootingCircle,ShootableProperties>();

    public Color randomColor;
    public float angleTest = 0;
	// Use this for initialization
	void Start () {
        randomColor = new Color(
              Random.Range(0f,1f)
            , Random.Range(0f,1f)
            , Random.Range(0f,1f)
            );
	}
	
	// Update is called once per frame
	void Update () {
        CalculateDistance();
        GetVectors();
        CalculateAngle();
      //  PrintVariables();
	}


    public void CalculateDistance()
    {
        Vector2 v1 = Vector2.zero;
        Vector2 v2 = Vector2.zero;
        for (int i = 0; i < circles.Count; i++)
        {
            // asign the proper data for calculations
            v1.x = transform.position.x;
            v1.y = transform.position.y;
            v2.x = circles[i].transform.position.x;
            v2.y = circles[i].transform.position.y;

            properties[circles[i]].distanceToPlayer = Vector2.Distance(v1, v2);

        }
    }

    // calculate the angle between the character and the shootable
    public void CalculateAngle()
    {
        Vector2 v1 = Vector2.zero;
        Vector2 v2 = Vector2.zero;
        for (int i = 0; i < circles.Count; i++)
        {
            // asign the proper data for calculations
            v1.x = transform.position.x;
            v1.y = transform.position.y;
            v2.x = circles[i].transform.position.x;
            v2.y = circles[i].transform.position.y;

            properties[circles[i]].angleToPlayer = Mathf.Atan2(v1.y - v2.y, v1.x - v2.x);

            

        }
    }

    public void GetVectors()
    {

        Vector2 v1 = Vector2.zero;
        Vector2 v2 = Vector2.zero;
        float angle = (25 * Mathf.PI) / 180;

       
        for (int i = 0; i < circles.Count; i++)
        {
            // get the range of the shooting target
            Vector3 displacement =  transform.position - circles[i].transform.position;
            Vector3 extensionVector = Ray.RotateVector(displacement, -angle);
            // asign the proper data for calculations
            properties[circles[i]].displacement = displacement;
            properties[circles[i]].extendedDisplacement = extensionVector;




        }
    }

    public void AddSoootingCircleProperties(ShootingCircle sc)
    {
        // register it for calculations
        if(!circles.Contains(sc))
        {
            circles.Add(sc);
        }

        properties.Add(sc, new ShootableProperties());
    }
   

    void PrintVariables()
    {
        for (int i = 0; i < circles.Count; i++)
        {

            Debug.Log(properties[circles[i]].dotProductToPlayer);
            Debug.Log(properties[circles[i]].angleToPlayer);
            Debug.Log(properties[circles[i]].distanceToPlayer);



        }
    }

    void OnGUI()
    {

        for (int i = 0; i < circles.Count; i++)
        {
            float angle = (25 * Mathf.PI )/ 180;

            Vector3 displacement = transform.position - circles[i].transform.position;
            Vector3 displacementRotatedMinus = Ray.RotateVector(displacement, -angle);
            Vector3 displacementRotatedPlus = Ray.RotateVector(displacement, angle);
            // measure the angle between two points based on my position
            Debug.DrawLine(transform.position, circles[i].transform.position, randomColor);
            Debug.DrawLine(circles[i].transform.position, circles[i].transform.position + displacementRotatedMinus, randomColor);
            Debug.DrawLine(circles[i].transform.position, circles[i].transform.position + displacementRotatedPlus, randomColor);



        }
        
    }

}

// different for each player
public class ShootableProperties
{
    public bool visible;
    public float distanceToPlayer;
    public float angleToPlayer;
    public float dotProductToPlayer;
    public float dotProductExtendeAngle;
    public Vector2 displacement;
    public Vector2 extendedDisplacement;

}