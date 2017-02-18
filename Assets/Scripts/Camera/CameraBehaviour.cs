using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public CameraWeights[] weights;
    Transform myTransform;

    public bool debugs;
    
    public float dotValueTolerance_Back;
    public float dotValueTolerance_Forward;

     Vector3 VectorFoo = new Vector3(1000000, 100000, 100000);

	// Use this for initialization
	void Start () {

        weights = FindObjectsOfType<CameraWeights>();
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

        Debugs(debugs);


        CameraMove();
        
//        GetCameraDotOnWeights();
	    
	}


    void Debugs(bool debugs)
    {
        if (debugs)
        {
            for (int i = 0; i < weights.Length; i++)
            {

                Debug.DrawLine(transform.position, weights[i].transform.position, Color.blue);
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

            if (weights[i].cameraDot > -0.83f)
            {

                inCameraQuantity--;
                newPosition = myTransform.forward;
            }


            
            if ( weights[i].cameraDot < -0.9f && inCameraQuantity == weights.Length)
            {
                if (myTransform.position.z < -20f)
                {
                  

                    newPosition = -transform.forward;
                }
            }
        }
      //  if (myTransform.position.z < -20f)
        Debug.Log(newPosition);
        myTransform.position -= newPosition * 0.2f;
    }
    
    public void CameraMove()
    {

        Vector3 cameraPos = VectorFoo;
        float previousWeight = -1;
        int factorCount = 0;
        float sumWeight = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (cameraPos == VectorFoo)
            {
                cameraPos = weights[i].transform.position; // our start point for positioning the camera. The weights have no relevance at this point
                previousWeight = weights[i].MoveInfluence;
                if (previousWeight != 0)
                    sumWeight =   previousWeight;

            }
            else if (i == 1)
            {
               
                Vector3 vecToBeAdd = weights[i].transform.position - cameraPos; // displacement vector between the two weights
                vecToBeAdd *= 0.5f;
                // each side can claim up to 50% of the vector, based on their weights
                cameraPos = (cameraPos + vecToBeAdd) - ((vecToBeAdd ) * previousWeight) + ((vecToBeAdd ) * weights[i].MoveInfluence);
                Debug.Log(vecToBeAdd);
                // the new wieght is the half of the sum of the weights
                
                previousWeight = previousWeight + weights[i].MoveInfluence;
                sumWeight = weights[i].MoveInfluence > sumWeight ? weights[i].MoveInfluence : sumWeight;

            }
            else
            {
               // float pushingFactor = 0.5f * 
                Debug.Log(sumWeight);
                Vector3 vecToBeAdd = weights[i].transform.position - cameraPos; // displacement vector between the two weights
                vecToBeAdd *= 0.5f;

                cameraPos = (cameraPos + vecToBeAdd) - ((vecToBeAdd) * sumWeight) + ((vecToBeAdd) * weights[i].MoveInfluence);
                sumWeight = weights[i].MoveInfluence > sumWeight ? weights[i].MoveInfluence : sumWeight;
            }
        }
        // the z axis has it's own logic
       
        cameraPos += transform.up *2;
        cameraPos.z = myTransform.position.z;
        // move the camera with easing
        myTransform.position -= (myTransform.position - cameraPos) * 0.5f;

    }

}


