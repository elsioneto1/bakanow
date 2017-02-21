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
        
        GetCameraDotOnWeights();
	    
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

        myTransform.position -= newPosition * 0.2f;
    }
    
    public void CameraMove()
    {

        Vector3 cameraPos = VectorFoo;
		Vector3 baricenter;
        float previousWeight = 0;
        int factorCount = 0;
        float sumWeight = 0;
		
		float sumX = 0;
		float sumY = 0;
		for (int i = 0; i < weights.Length ; i++) {
			
			sumX += weights[i].transform.position.x;
			sumY += weights[i].transform.position.y;

		}
        
		cameraPos = baricenter = new Vector3 (sumX / weights.Length,sumY / weights.Length,0);
		sumX = 0;
		sumY = 0;
        for (int i = 0; i < weights.Length; i++)
        {
			
			float _sumX = 0;
			
            float _sumY = 0;
            previousWeight += weights[i].MoveInfluence;
			//for (int j = 0;  j < weights.Length; j++) 
            {
            	
				//if ( i != j)
				{

					Vector3 displacement =  weights[i].transform.position - baricenter;
					//displacement ;
                    Vector3 aresta = (weights[i].transform.position + displacement )
                        + (displacement * weights[i].MoveInfluence );
                    sumX += displacement.x * weights[i].MoveInfluence;
                    sumY += displacement.y * weights[i].MoveInfluence;
				}
                    
            }
           
			
           
        }        // the z axis has it's own logic

        cameraPos = baricenter + new Vector3(sumX / previousWeight, sumY / previousWeight, 0); 

		// cameraPos += transform.up *2;
        cameraPos.z = myTransform.position.z;
        // move the camera with easing
        myTransform.position -= (myTransform.position - cameraPos) * 0.5f;

    }

}


