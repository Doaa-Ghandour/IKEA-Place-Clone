using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;


public class InputManager : ARBaseGestureInteractable
{
    //[SerializeField] private GameObject arObj;
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject crosshair;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Touch touch;
    private Pose pose;

    /*private void Update()
    {
        CrosshairCalculation();

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }  

        if (Input.touchCount < 0 || touch.phase != TouchPhase.Began)
        {
            return;
        }

        if (IsPointerOverUI(touch))
        {
            return;
        }

        Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

    }*/

    protected override bool CanStartManipulationForGesture(TapGesture gesture)
    {
        if (gesture.targetObject == null)
            return true;
        return false;    
    }

    [System.Obsolete]
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member
    protected override void OnEndManipulation(TapGesture gesture)
#pragma warning restore CS0809 // Obsolete member overrides non-obsolete member
    {
        if (gesture.isCanceled)
            return;

        if (gesture.targetObject != null || IsPointerOverUI(gesture))
            return;

        if (GestureTransformationUtility.Raycast(gesture.startPosition, hits, trackableTypes: TrackableType.PlaneWithinPolygon))
        {
            GameObject placeObj = Instantiate(DataHandler.Instance.GetFurniture(), pose.position, pose.rotation);

            var anchorObj = new GameObject("PlacementAnchor");
            anchorObj.transform.position = pose.position;
            anchorObj.transform.rotation = pose.rotation;
            placeObj.transform.parent = anchorObj.transform;
        }
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        CrosshairCalculation(); 
    }

    private bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    [System.Obsolete]
    private void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (GestureTransformationUtility.Raycast(origin, hits, trackableTypes: TrackableType.PlaneWithinPolygon))
        {
            pose = hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90, 0, 0);
        }
    }
}
