﻿using UnityEngine;
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



    public float OnScreenCenter = -0.93f;

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

       // MoveInfluence = 12;

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
        CameraRail.Instance.GoBack = true;

        //Debug.Log(name);
       // Debug.Log(cameraDot);
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

        CameraRail.Instance.GoBack = false;

        OnEdge = false;

       // MoveInfluence = 5;

        EDGE_WEIGHTS_COUNTER--;
        RAY_COLOR_ON_SCREEN = Color.blue;
        camBehav.screenEdgeBehaviour -= ScreenEdgeExit;
    }

    public void ScreenCenterEnter()
    {
        if (camBehav == null)
            return;
        camBehav.screenEdgeBehaviour -= ScreenCenterEnter;
        camBehav.screenEdgeBehaviour += ScreenCenterStay;


    }
    public void ScreenCenterStay()
    {
        if (camBehav == null)
        {
            Debug.Log("Camera null");
            camBehav.screenEdgeBehaviour -= ScreenCenterStay;
            return;
        }
        CameraRail.Instance.GoForward = true;
        if ( MIDDLE_WEIGHTS_COUNTER != TOTAL_WEIGHTS)
        {
            camBehav.screenEdgeBehaviour -= ScreenCenterStay;
            camBehav.screenEdgeBehaviour += ScreenCenterExit;
        }

    }

    public void ScreenCenterExit()
    {


        CameraRail.Instance.GoForward = false;

        camBehav.screenEdgeBehaviour -= ScreenCenterExit;

    }

    // </asigned to delegates on camera behaviour>
}
