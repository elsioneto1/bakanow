using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCorrection : MonoBehaviour {

    Transform myCachedtransform;
    Transform parentTransform;
	// Use this for initialization
	void Start () {
        myCachedtransform = transform;
        parentTransform = transform.root;
	}
	
	// Update is called once per frame
	void Update () {
       // if ( )
       // transform.localRotation = Quaternion.Euler();
	}
}
