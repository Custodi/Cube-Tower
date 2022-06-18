using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextView : View
{
    private string _message;
    public string message { get => _message; set => _message = value; }

    private void Start()
    {
        viewType = ViewType.Text;
    }
}
