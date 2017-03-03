using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class MecaAnimLoader : MonoBehaviour {
    [HideInInspector]
    public UnityArmatureComponent armatureComponent;

    public float size = 6;

	// Use this for initialization
	void Start () {
        // Load data.
        UnityFactory.factory.LoadDragonBonesData("PirateShip/ggj_robot"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("PirateShip/texture"); //Texture atlas file path (without suffix) 
        // Create armature.
        armatureComponent = UnityFactory.factory.BuildArmatureComponent("Armature"); // Input armature name
        // Play animation.
        // armatureComponent.animation.Play("walk");
        
        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        armatureComponent.transform.parent = transform;
        //armatureComponent.slo

        armatureComponent.transform.localScale = new Vector3(armatureComponent.transform.localScale.x * size,
            armatureComponent.transform.localScale.y * size,
            armatureComponent.transform.localScale.z * size);
    

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
