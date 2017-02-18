using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ray : MonoBehaviour {

    Transform cachedTransform;
    CharacterAnimationController chracAnim;
    public LineRenderer lineRenderer;

    public float LineDrawSpeed;
    Vector3 StartingPoint;



    public List<Point> points = new List<Point>();
    public Point LatestPoint;


    public GameObject Arm;

    public float Frequency = 120;

    public Transform raySpawnPoint;
    public bool firing;
    public float fireRate  = 0.5f;
    float lastShot;

    public float angle;
    public float overheatTime   = 0;
    public float overheatLimit  = 3;
    public bool overheated = false;

	// Use this for initialization
	void Start () {

        chracAnim = GetComponent<CharacterAnimationController>();
        cachedTransform = transform;
        lineRenderer.sortingLayerName = "OnTop";
        lineRenderer.sortingOrder = 5;
        lineRenderer.SetVertexCount(1000);
       
	}
	
	// Update is called once per frame
	void Update () {


        if (firing && ! overheated)
        {
            if (points == null)
                points = new List<Point>();
        }
        else
        {
            for (int i = 0; i < 1000; i++)
            {
                lineRenderer.SetPosition(i, raySpawnPoint.position);
            }
              
        }


        Arm.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));

        overheatTime += Time.deltaTime;

        for (int i = 0; i < 1000; i++)
        {
            lineRenderer.SetPosition(i, raySpawnPoint.position);
        }
              

        if (firing)
        {


            for (int i = 0; i < points.Count; i++)
            {
                points[i].MyPosition += points[i].Direction * Time.deltaTime * LineDrawSpeed;
                lineRenderer.SetPosition(i, points[i].MyPosition);
            }

            if (overheatTime < overheatLimit)
            {

                if (lastShot < 0)
                {

                    SpawnPoint();
                    lastShot = fireRate;
                }

                lastShot -= Time.deltaTime;

            }
            else
            {

                if (!overheated)
                {
                    overheated = true;
                    overheatTime = 5;
                    firing = false;
                }
                      

            }

        }
        else
        {

            if (overheated)
            {
                overheatTime -= Time.deltaTime * 2.5f;
                if (overheatTime < 0)
                {
                    overheatTime = 0;
                    overheated = false;
                    firing = false;
                }
            }
            else
            {
                points = null;

                overheatTime -= Time.deltaTime * 2;
                if (overheatTime < 0)
                {
                    overheatTime = 0;
                }
            }
        }
            
     


	}

    public Vector3 RotateVector(Vector3 v, float angle)
    {

        Vector3 vec = Vector3.zero;

        Matrix4x4 m = new Matrix4x4();
        m.SetRow(0, new Vector4(Mathf.Cos(angle), Mathf.Sin(-angle), 0, 0));
        m.SetRow(1, new Vector4(Mathf.Sin(angle), Mathf.Cos(angle), Mathf.Sin(-angle), 0));
        m.SetRow(2, new Vector4(0, Mathf.Sin(angle), Mathf.Cos(angle), 0));
        m.SetRow(3, new Vector4(0, 0, 0, 1));

        vec = m.MultiplyVector(v);

        return vec;

    }


    public void SpawnPoint()
    {

        points.Add(new Point(raySpawnPoint.position, points.Count == 0 ? null : points[points.Count - 1], RotateVector(cachedTransform.right * chracAnim.direction, (angle * chracAnim.direction * Mathf.PI) / 180)));

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
        MyPosition = SpawnPosition;
    }

}
