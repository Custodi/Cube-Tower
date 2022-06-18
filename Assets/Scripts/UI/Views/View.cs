using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour
{
    [SerializeField] protected ViewType viewType;
    public delegate void ViewEvent(ViewType type, View obj);
    public ViewEvent OnUpdateUI;
}
