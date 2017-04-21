using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class DragonBonesTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        UnityFactory.factory.LoadDragonBonesData("PirateShip/ggj_robot"); // DragonBones file path (without suffix)
        UnityFactory.factory.LoadTextureAtlasData("PirateShip/texture");  //Texture atlas file path (without suffix) 
        // Create armature.
        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("Armature"); // Input armature name
        // Play animation.
        armatureComponent.animation.Play("sword");

        // Change armatureposition.
        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
