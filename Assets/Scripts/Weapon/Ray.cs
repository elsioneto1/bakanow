using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ray : MonoBehaviour {



    Transform cachedTransform;
    CharacterAnimationController chracAnim;
    public LineRenderer lineRenderer;

    public float LineDrawSpeed;
    Vector3 StartingPoint;

    int lastPointFiredIndex = -1;

    public List<Point> points = new List<Point>();
    
    public Point LatestPoint;


    public GameObject Arm;

    public float Frequency = 120;

    public Transform raySpawnPoint;
    public bool firing;
    public float fireRate  = 0.5f;
    float lastShot;

    public float angle;

    // overheat variables
    public float overheatTime   = 0;
    public float overheatLimit  = 3;
    public bool overheated = false;

    public Vector3 aimingVector;

    public static Ray[] MY_RAYS; // keep a static access to the rays
    List<Nodes> Nodes = new List<Nodes>();


    public float RaySpacing = 5;

    
    // system management
    public static bool INITIALIZED_ON_SCENE = false;

	// Use this for initialization
	void Start () {

        chracAnim = GetComponent<CharacterAnimationController>();
        cachedTransform = transform;
        lineRenderer.sortingLayerName = "OnTop";
        lineRenderer.sortingOrder = 5;
        SpawnPoints();

        lineRenderer.SetVertexCount(points.Count);

        MY_RAYS = FindObjectsOfType<Ray>();

	}
	void OnDestroy()
    {
        INITIALIZED_ON_SCENE = false;
    }

    public void SpawnPoints()
    {
        if (points == null)
            points = new List<Point>();
        for (int i = 0; i < 50; i++)
		{
            points.Add(new Point(raySpawnPoint.position, points.Count == 0 ? null : points[points.Count - 1], RotateVector(cachedTransform.right * chracAnim.direction, (angle * Mathf.PI) / 180)));
		}
    }


    // all the nodes we need to have access are addeds by the own nodes when they initialize
    public void AddNodeToList(Nodes n)
    {
        Nodes.Add(n);
    }

	// Update is called once per frame
	void Update () {

        // system management
        INITIALIZED_ON_SCENE = true;
        lineRenderer.SetVertexCount(points.Count);

        for (int i = 0; i < points.Count; i++)
        {

            lineRenderer.SetPosition(i, raySpawnPoint.position);
        }

        Arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        aimingVector = RotateVector(cachedTransform.right * chracAnim.direction, (angle * Mathf.PI) / 180);

        if (firing)
        {
            if (overheatTime < overheatLimit)
            {

                if (lastShot < 0)
                {

                    Shoot();

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
    


	}

    public static Vector3 RotateVector(Vector3 v, float angle)
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


    public void MovePoint(int i)
    {

//        points[i] =  Point(raySpawnPoint.position, points.Count == 0 ? null : points[points.Count - 1], RotateVector(cachedTransform.right * chracAnim.direction, (angle  * Mathf.PI) / 180)));

    }

    public void Shoot()
    {



        for (int i = 0; i < points.Count; i++)
        {

            points[i].Direction = RotateVector(cachedTransform.right * chracAnim.direction, (angle  * Mathf.PI) / 180);
           
            points[i].SpawnPosition = raySpawnPoint.position + (points[i].Direction * i * RaySpacing);


            // checks if hit any node

            bool hitting = false;
            for (int j = 0; j < Nodes.Count; j++)
            {
                hitting = Nodes[j].VerifyHitting(points[i], this);

                if (hitting)
                {
                    lineRenderer.SetVertexCount(i+1);

                    break;
                }
            }
            lineRenderer.SetPosition(i, points[i].SpawnPosition);

            if (hitting)
                break;

        }

        //if ( points[i].fired == false)
        //{
        //    lastPointFiredIndex = i;
        //    points[i].fired = true;
        //    points[i].SpawnPosition = raySpawnPoint.position;

        //    points[i].PointSpawned = lastPointFiredIndex == -1 ? null : points[lastPointFiredIndex];
        //    points[i].Direction = RotateVector(cachedTransform.right * chracAnim.direction, (angle  * Mathf.PI) / 180);
        //}
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
    public bool hit = false;
    public bool fired = false;

    public Point(Vector3 spawnPosition, Point pointSpawned, Vector3 direction)
    {

        SpawnPosition = spawnPosition;
        PointSpawned = pointSpawned;
        Direction = direction;
        MyPosition = SpawnPosition;
    }

}
