using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable
{
    private bool _isBusy;

    public bool isBusy { get => _isBusy; private set => _isBusy = value; }

    public void TakeTile()
    {
        isBusy = true;
    }

    public void FreeTile()
    {
        isBusy = false;
    }

    public void OnClicked()
    {
        
    }

    public void OnSelected()
    {
        //GetComponent<MeshRenderer>().material.color = Color.black;
    }

    public void OnUnselected()
    {
        //GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void OnUnclicked()
    {

    }
}
