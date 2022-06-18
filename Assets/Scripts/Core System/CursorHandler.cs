using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    protected Camera rayCamera;
    protected Ray outputRay;
    protected RaycastHit rayCastInfo;
    protected Vector3 rayCastPosition;

    protected IInteractable previousInteractableObject = null, currentInteractableObject = null, previousClickedObject = null;

    protected bool isGamePaused;

    private void Start()
    {
        rayCamera = Camera.main;
        //LevelController.instance.OnPauseGame += OnGamePaused;
        //LevelController.instance.OnUnpauseGame += OnGameUnpaused;
    }

    void Update()
    {
        //if (isGamePaused == false) return;
        outputRay = rayCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(outputRay, out rayCastInfo))
        {
            //Debug.Log(rayCastInfo.collider.gameObject.name);
            rayCastPosition = rayCastInfo.point;
            if (rayCastInfo.transform.TryGetComponent(out currentInteractableObject))
            {
                currentInteractableObject.OnSelected();
                if (currentInteractableObject != previousInteractableObject && previousInteractableObject != null) previousInteractableObject.OnUnselected();
                previousInteractableObject = currentInteractableObject;

                if (Input.GetMouseButtonDown(0))
                {
                    if (previousClickedObject != null && previousClickedObject != currentInteractableObject) previousClickedObject.OnUnclicked();
                    currentInteractableObject.OnClicked();
                    previousClickedObject = currentInteractableObject;
                }
            }
            else
            {
                previousInteractableObject?.OnUnselected();
                previousInteractableObject = null;
            }
        }
    }

    private void OnDestroy()
    {
        //LevelController.instance.OnPauseGame -= OnGamePaused;
        //LevelController.instance.OnUnpauseGame -= OnGameUnpaused;
    }

    public Vector3 GetRaycastPosition()
    {
        return rayCastPosition;
    }

    protected void OnGamePaused()
    {
        isGamePaused = true;
    }
    protected void OnGameUnpaused()
    {
        isGamePaused = false;
    }
}
