using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursorHandler : CursorHandler
{
    [SerializeField] private BuildSystem _buildSystem;

    private Tile _selectedTile;
    public Tile selectedTile 
    {
        get
        {
            return _selectedTile;
        }
        
        private set
        {
            if (value == null)
            {
                _selectedTile = null;
            }
            else if (value.TryGetComponent<Tile>(out _))
            {
                _selectedTile = value;
            }
        }
    }

    private GameObject _constractionTower;
    public GameObject constractionTower 
    { 
        get
        {
            return _constractionTower;
        }
        set
        {
            if(value == null)
            {
                _constractionTower = null;
            }
            else if (value.TryGetComponent<Tower>(out _))
            {
                _constractionTower = value;
            }
        }
    }

    private void Start()
    {
        rayCamera = Camera.main;
        LevelController.instance.OnPauseGame += OnGamePaused;
        LevelController.instance.OnUnpauseGame += OnGameUnpaused;
    }

    void Update()
    {
        if (isGamePaused == true) return;
        outputRay = rayCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(outputRay, out rayCastInfo))
        {
            //Debug.Log(rayCastInfo.collider.gameObject.name);
            rayCastPosition = rayCastInfo.point;
            if (_buildSystem.isBuildMode)
            {
                if (rayCastInfo.transform.TryGetComponent(out _selectedTile))
                {
                    //Debug.Log(_selectedTile);
                    constractionTower.transform.position = selectedTile.transform.position;
                }
                else
                {
                    constractionTower.transform.position = rayCastInfo.point;
                }
            }
            else
            {
                if (rayCastInfo.transform.TryGetComponent(out currentInteractableObject))
                {
                    //Debug.Log(currentInteractableObject);
                    currentInteractableObject.OnSelected();
                    if (currentInteractableObject != previousInteractableObject && previousInteractableObject != null) previousInteractableObject.OnUnselected();
                    previousInteractableObject = currentInteractableObject;

                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log(previousClickedObject);
                        if (previousClickedObject != null && previousClickedObject != currentInteractableObject) previousClickedObject.OnUnclicked();
                        currentInteractableObject.OnClicked();
                        previousClickedObject = currentInteractableObject;
                    }
                } 
                else
                {
                    //Debug.Log("hey");
                    previousInteractableObject?.OnUnselected();
                    previousInteractableObject = null;

                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log(previousClickedObject);
                        if (previousClickedObject != null) previousClickedObject?.OnUnclicked();
                        previousClickedObject = null;
                    }
                }

            }
        }
    }

    private void OnDestroy()
    {
        LevelController.instance.OnPauseGame -= OnGamePaused;
        LevelController.instance.OnUnpauseGame -= OnGameUnpaused;
    }

    public void ToggleBuildSystem(bool newState)
    {
        if(newState)
        {
            previousInteractableObject?.OnUnselected();
            previousInteractableObject = null;
        }
        else
        {
            constractionTower = null;
        }
    }
}
