using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteMaterials : MonoBehaviour {

    // all materials must be on the Resources folder to be accesed
    public string MaterialName;
    public Material material;

    MeshRenderer[] Meshs;
    SpriteRenderer[] Sprites;

	// Use this for initialization
    void Start()
    {
        Meshs = GetComponentsInChildren<MeshRenderer>();
        Sprites = GetComponentsInChildren<SpriteRenderer>();
        Debug.Log(Meshs.Length);

        OverwriteMaterial();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // extremely slow operation. Use carefully
    public void OverwriteMaterial()
    {

        // fetch the material
        material = Resources.Load("Material/" + MaterialName, typeof(Material)) as Material;


        if (material == null)
        {
            Debug.Log("No material selected on " + name.ToString());
            return;
        }


        for (int i = 0; i < Meshs.Length; i++)
        {

            if ( Meshs[i].GetComponent<DontOverwriteMe>() == null)
            {
                Meshs[i].material = material;
            }
        }
        for (int i = 0; i < Sprites.Length; i++)
        {
            if ( Sprites[i].GetComponent<DontOverwriteMe>() == null)
            {
                Sprites[i].material = material;
            } 
        }

    }

}
