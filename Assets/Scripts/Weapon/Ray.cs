using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ray : MonoBehaviour {

    Transform cachedTransform;

    LineRenderer lineRenderer;

    public float LineDrawSpeed;
    Vector3 StartingPoint;



    public List<Point> points = new List<Point>();
    public Point LatestPoint;

    


    public float Frequency = 120;


    public bool firing;
    public float fireRate  = 0.5f;
    float lastShot;

	// Use this for initialization
	void Start () {

        lineRenderer = GetComponent<LineRenderer>();
        cachedTransform = transform;
        lineRenderer.sortingLayerName = "OnTop";
        lineRenderer.sortingOrder = 5;
        lineRenderer.SetVertexCount(200);
         for (int i = 0; i <200; i++)
         {
             lineRenderer.SetPosition(i, Vector3.zero);
         }
	}
	
	// Update is called once per frame
	void Update () {


        if (points != null)
        {
            
            for (int i = 0; i < points.Count; i++)
            {
                points[i].MyPosition += points[i].Direction * Time.deltaTime * LineDrawSpeed ;
                lineRenderer.SetPosition(i, points[i].MyPosition);
            }

            if (firing)
            {
                if (lastShot < 0)
                {

                    SpawnPoint();
                    lastShot = fireRate;
                }

                lastShot -= Time.deltaTime;


            }
        }
        else
        {
         //   points = null;
        }




	}


    public void SpawnPoint()
    {
        points.Add(new Point(cachedTransform.position,points.Count == 0? null : points[points.Count -1], cachedTransform.right ));
    }

}

[System.Serializable]
public class Point
{
    public float DistanceTraveled;
    public Vector3 SpawnPosition;
    public Point PointSpawned;
    public Vector3 Direction;
    public Vector3 MyPosition;

    public Point(Vector3 spawnPosition, Point pointSpawned, Vector3 direction)
    {

        SpawnPosition = spawnPosition;
        PointSpawned = pointSpawned;
        Direction = direction;

    }

}
