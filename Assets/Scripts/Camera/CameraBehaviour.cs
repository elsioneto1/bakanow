using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public CameraWeights[] weights;
    Transform myTransform;

    public bool debugs;
    
    public float dotValueTolerance_Back;
    public float dotValueTolerance_Forward;

     Vector3 VectorFoo = new Vector3(1000000, 100000, 100000);

    public delegate void ScreenEdge();
    public ScreenEdge screenEdgeBehaviour;

    // the amount of Z that needs to be reallocated forward or back  
    public Vector3 ReallocateZ = Vector3.zero;
    

	// Use this for initialization
	void Start () {

        weights = FindObjectsOfType<CameraWeights>();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i].camBehav = this;
        }
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

        Debugs(debugs);


        CameraMoveXY();
        
        GetCameraDotOnWeights();

        if (screenEdgeBehaviour != null)
            screenEdgeBehaviour();

        // resets the Z translocating vector
        ReallocateZ = Vector3.zero;

	}


    void Debugs(bool debugs)
    {
        if (debugs)
        {
            for (int i = 0; i < weights.Length; i++)
            {

                Debug.DrawLine(transform.position, weights[i].transform.position, weights[i].RAY_COLOR_ON_SCREEN);
            }

        }

    }

    public void GetCameraDotOnWeights()
    {

        Vector3 newPosition = Vector3.zero;

        int inCameraQuantity = weights.Length;
       // bool bac = false;
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i].cameraDot = Vector3.Dot(transform.forward,
                (transform.position -  weights[i].transform.position).normalized);
           
            if (weights[i].cameraDot > weights[i].ScreenEdgeEnterDot)
            {

                inCameraQuantity--;
                newPosition = myTransform.forward;
                // verify if the weight it's already on edge
                if (!weights[i].OnEdge)
                {
                    // asign the behaviour to it
                    weights[i].OnEdge = true;
                    screenEdgeBehaviour += weights[i].ScreenEdgeEnter;
                }
                else
                {

                }
            }
            

          
        }

        //if (inCameraQuantity == weights.Length)
        //{
        //    for (int i = 0; i < weights.Length; i++)
        //    {

        //        if (weights[i].cameraDot <  weights[i].OutOfScreenEdgeDot)
        //        {
        //            if (myTransform.position.z < -20f)
        //            {


        //                newPosition = -transform.forward;
        //            }
        //        }
        //    }
        //}
      //  if (myTransform.position.z < -20f)

        myTransform.position -= newPosition * 0.2f;
    }


    public void CameraZReallocate(Vector3 reallocation)
    {
        

    }
    
    public void CameraMoveXY()
    {

        Vector3 cameraPos = VectorFoo;
		Vector3 baricenter;
        float sumWeight = 0;
		
		float sumX = 0;
		float sumY = 0;
		for (int i = 0; i < weights.Length ; i++) {
			
			sumX += weights[i].transform.position.x;
			sumY += weights[i].transform.position.y;

		}
        // define the baricenter
		cameraPos = baricenter = new Vector3 (sumX / weights.Length,sumY / weights.Length,0);
		sumX = 0;
		sumY = 0;

        // from the baricenter, each vertex point will claim an vector from the center to position the camera. Later, everything will be divided by the sum of all the weights
        for (int i = 0; i < weights.Length; i++)
        {
			
			
		    Vector3 displacement =  weights[i].transform.position - baricenter;
            Vector3 aresta = (weights[i].transform.position + displacement )
                + (displacement * weights[i].MoveInfluence );
            sumX += displacement.x * weights[i].MoveInfluence;
            sumY += displacement.y * weights[i].MoveInfluence;
            sumWeight += weights[i].MoveInfluence;
			
           
        }

        // sum up to the baricenter and divide by the sum
        cameraPos = baricenter + new Vector3(sumX / sumWeight, sumY / sumWeight, 0); 

		// cameraPos += transform.up *2;
        cameraPos.z = myTransform.position.z;
        // move the camera with easing
        myTransform.position -= (myTransform.position - cameraPos) * 0.5f;

    }

    public void OnScreenEdgeEnter()
    {

    }

    public void OnScreenEdgeStay()
    {

    }

    public void OnScreenEdgeExit()
    {

    }


}


