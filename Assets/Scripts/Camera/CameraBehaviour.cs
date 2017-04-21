using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    // TODO
    // based on the weights, the camera will always stay on the middle of every camera object
    // if the object get off screen, it'll get adjusted based on the rails

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
    // being set at camera rail
    public Vector3 PositionYZ;

    public Vector3 OutputVector;

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

        GetCameraDotOnWeights();

        CameraMoveXY();
        

        if (screenEdgeBehaviour != null)
            screenEdgeBehaviour();


        SetPosition();
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
        CameraWeights.MIDDLE_WEIGHTS_COUNTER = weights.Length;


        // bool bac = false;
        for (int i = 0; i < weights.Length; i++)
        {

            weights[i].cameraDot = Vector3.Dot(transform.forward,
                (transform.position -  weights[i].transform.position).normalized);
           // Debug.Log(weights[i].cameraDot);
            // entering screen edge
            if (weights[i].cameraDot > weights[i].ScreenEdgeEnterDot)
            {

                inCameraQuantity--;


                // verify if the weight it's already on edge
                if (!weights[i].OnEdge)
                {

                    // asign the behaviour to it
                    weights[i].OnEdge = true;
                    screenEdgeBehaviour += weights[i].ScreenEdgeEnter;
                }
              
            }
            //Debug.Log(weights[i].cameraDot);  
            //WE ARE NOT AT THE CENTER 
           // Debug.Log(weights[i].cameraDot);
            if ( weights[i].cameraDot > weights[i].OnScreenCenter)
            {
               // Debug.Log(weights[i].name);
                CameraWeights.MIDDLE_WEIGHTS_COUNTER--;
            }

            
       

          
        }
        // if everyone is there, then...
        if (CameraWeights.MIDDLE_WEIGHTS_COUNTER == weights.Length)
        {

            screenEdgeBehaviour += CameraRail.Instance.ScreenCenterEnter;


        }

        OutputVector -= newPosition * 0.2f;
    }


    public void CameraYZReallocate(Vector3 vec)
    {
        OutputVector.z = vec.z;
        OutputVector.y = vec.y;
        //transform.position 
    }
    
    void SetPosition()
    {
        myTransform.position = OutputVector;
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
        baricenter.y = 0;
        baricenter.z = 0;
        // sum up to the baricenter and divide by the sum
        cameraPos = baricenter + new Vector3(sumX / sumWeight, 0, 0); 

		// cameraPos += transform.up *2;
        cameraPos.z = myTransform.position.z;
        // move the camera with easing

        OutputVector.x -= (OutputVector.x - cameraPos.x) * 0.5f;
        
    }

    

}


