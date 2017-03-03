using UnityEngine;
using System.Collections;

public class CameraWeights : MonoBehaviour {

    public static int TOTAL_WEIGHTS = 0;
    public static int EDGE_WEIGHTS_COUNTER = 0;
    public static int MIDDLE_WEIGHTS_COUNTER = 0;
 
    public CameraBehaviour camBehav;
    public float weight;
    public Transform myTransform;
    public float cameraDot;

    [Range(0.001f,100f)]
    public float MoveInfluence = 1f;
    public float OutOfScreenEdgeDot = -0.9f;
    public float MaxWeightToBeAdd = 50;
    public float ScreenEdgeEnterDot = -0.83f;
    public bool OnEdge = false;

    public Color RAY_COLOR_ON_SCREEN = Color.blue;

	// Use this for initialization
	void Start () {
        myTransform = transform;
        TOTAL_WEIGHTS++;
	}
	
	// Update is called once per frame
	void Update () {
        if (camBehav == null)
            camBehav = FindObjectOfType<CameraBehaviour>();
	}

    // <asigned to delegates on camera behaviour>
    public void ScreenEdgeEnter()
    {
        if (camBehav == null)
            return;

        MoveInfluence = 12;

        Debug.Log("adasd");
        EDGE_WEIGHTS_COUNTER++;
        RAY_COLOR_ON_SCREEN = Color.red;
        camBehav.screenEdgeBehaviour -= ScreenEdgeEnter;
        camBehav.screenEdgeBehaviour += ScreenEdgeStay;
    }

    public void ScreenEdgeStay()
    {

        if (camBehav == null)
        {
            Debug.Log("Camera null");
            camBehav.screenEdgeBehaviour -= ScreenEdgeStay;
            return;
        }


        Debug.Log("executing logic");

        if  (cameraDot <  OutOfScreenEdgeDot)
        {
            Debug.Log("getting out of stay");
            camBehav.screenEdgeBehaviour -= ScreenEdgeStay;
            camBehav.screenEdgeBehaviour += ScreenEdgeExit;
        }


    }

    public void ScreenEdgeExit()
    {
        if (camBehav == null)
        {

            camBehav.screenEdgeBehaviour -= ScreenEdgeExit;
            return;
        }


        OnEdge = false;

        MoveInfluence = 5;

        EDGE_WEIGHTS_COUNTER--;
        RAY_COLOR_ON_SCREEN = Color.blue;
        camBehav.screenEdgeBehaviour -= ScreenEdgeExit;
    }

    // </asigned to delegates on camera behaviour>
}
