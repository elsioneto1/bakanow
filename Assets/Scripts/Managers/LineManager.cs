using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour {

    public MyLine[] renderers;
    public float breakAngle; // if the angle between two line is bigger than this, break the line.

	// Use this for initialization
	void Start () {

        LineRenderer[] myRenderers = GetComponentsInChildren<LineRenderer>();
        renderers = new MyLine[myRenderers.Length]; // create owr own renderers with management properties

        for (int i = 0; i < myRenderers.Length; i++)
        {
            renderers[i] = new MyLine(myRenderers[i]);
        }

	}
	
	// Update is called once per frame
	void Update () {
	    	
	}




}

// manager to our line

public class MyLine
{
    public bool isActive = false;
    public LineRenderer myRenderer;


    public MyLine(LineRenderer myRenderer)
    {
        this.myRenderer = myRenderer;
    }

}