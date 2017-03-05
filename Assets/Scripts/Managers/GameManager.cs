using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool DEBUG;
    static GameManager _Instance;
    public static GameManager Instance
    {
        get {
            return _Instance;
        }
        set
        {
            if (_Instance == null) // blocks aditional instances
                _Instance = value;
        }
    }
	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
