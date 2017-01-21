using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ray : MonoBehaviour {

    LineRenderer lineRenderer;

    public float LineDrawSpeed;
    Vector3 StartingPoint;

    public List<Point> points = new List<Point>();

    public float Frequency = 120;

    public Point LatestPoint;

	// Use this for initialization
	void Start () {

        lineRenderer = GetComponent<LineRenderer>();
        

	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < points.Count; i++)
        {
                
        }

        

	}


    public void SpawnSpoint()
    {

    }

}

public class Point
{
    public float DistanceTraveled;
    public Vector3 SpawnPosition;
    public Point PointSpawned;
    public Vector3 Direction;

}
