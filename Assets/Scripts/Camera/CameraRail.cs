using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRail : MonoBehaviour {

    // FAZ UM SCRIP OBJECT NESSA BUESTA PAS CONFIGURACAO QUERIDON
    // BJS ME LIGA

    // up and down
    public Vector3 CameraMaxClose;
    public Vector3 CameraMaxFar;
    private Vector3 DisplacamentUpAndDown;

    [Range(0, 1)]
    public float CameraPercentageUpAndDown = 0;
    float AddPercentage = 0;
    float AddPercentageAcceleration = 0.001f;

    public bool SnapUpAndDown_Close;
    public bool SnapUpAndDown_Far;

    CameraBehaviour camBehav;

    public static CameraRail Instance;
    public bool GoBack = false;
    bool _goingBack = false;
    public bool GoForward = false;
    bool _goForward = false;

	// Use this for initialization
	void Start () {
        DisplacamentUpAndDown = CameraMaxFar - CameraMaxClose;
        camBehav = GetComponent<CameraBehaviour>();
        Instance = this;

	}
	
	// Update is called once per frame
	void Update () {
        // DEAL WITH THIS LATER
        if (SnapUpAndDown_Close)
        {
            SnapUpAndDown_Close = false;
            CameraMaxClose = transform.position;
        }

        if (SnapUpAndDown_Far)
        {
            SnapUpAndDown_Far = false;
            CameraMaxFar = transform.position;
        }

        CameraBack();
        CameraForward();


        SetCameraPos();


        CameraPercentageUpAndDown += AddPercentage;
        SetSnaps();

     
	}

    void OnGUI()
    {
        Debug.DrawLine(CameraMaxClose, CameraMaxFar,Color.cyan);

    }

    private void SetCameraPos()
    {
        if (camBehav != null)
        {
            // Debug.Log();
            // camera position on the rail
            camBehav.CameraYZReallocate(CameraMaxClose + DisplacamentUpAndDown * CameraPercentageUpAndDown);
        }
    }

    private void CameraBack()
    {
        if (GoBack && !_goingBack)
        {
            _goingBack = true;
        }
        else if (!GoBack && _goingBack)
        {
            _goingBack = false;
          //  AddPercentage = 0;


        }


        if (_goingBack)
        {

            AddPercentage += AddPercentageAcceleration;
        }
        else
        {
            if (!_goForward)
            {
                AddPercentage *= 0.9f;
            }
        }


    }

    void CameraForward()
    {
        if (GoForward && !_goForward)
        {
            _goForward = true;
        }
        if (!GoForward && _goForward)
        {
            _goForward = false;
         //   AddPercentage = 0;

        }

        if (_goForward)
        {

            AddPercentage -= AddPercentageAcceleration;
        }
        else
        {
            if ( !_goingBack)
            {
                AddPercentage *= 0.9f;
            }
        }

    }

    void SetSnaps()
    {
        if (CameraPercentageUpAndDown > 1)
            CameraPercentageUpAndDown = 1;
        if (CameraPercentageUpAndDown < 0)
            CameraPercentageUpAndDown = 0;
    }


    public void ScreenCenterEnter()
    {
        if (camBehav == null)
            return;
        camBehav.screenEdgeBehaviour -= ScreenCenterEnter;
        camBehav.screenEdgeBehaviour += ScreenCenterStay;


    }
    public void ScreenCenterStay()
    {
        if (camBehav == null)
        {
            Debug.Log("Camera null");
            camBehav.screenEdgeBehaviour -= ScreenCenterStay;
            return;
        }
        CameraRail.Instance.GoForward = true;
        if (CameraWeights.MIDDLE_WEIGHTS_COUNTER != CameraWeights.TOTAL_WEIGHTS)
        {
            camBehav.screenEdgeBehaviour -= ScreenCenterStay;
            camBehav.screenEdgeBehaviour += ScreenCenterExit;
        }

    }

    public void ScreenCenterExit()
    {


        CameraRail.Instance.GoForward = false;

        camBehav.screenEdgeBehaviour -= ScreenCenterExit;

    }

}
