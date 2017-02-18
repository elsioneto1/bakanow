using UnityEngine;
using System.Collections;

public class CameraWeights : MonoBehaviour {

    public float weight;
    public Transform myTransform;
    public float cameraDot;

    [Range(0,1)]
    public float MoveInfluence = 1;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
