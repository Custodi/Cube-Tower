using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _edgePadding;

    [SerializeField]
    private float _maximumDamping;

    private float _screenWidth;
    private Vector3 _speedVector;

    // Start is called before the first frame update
    void Start()
    {
        _screenWidth = Screen.currentResolution.width;
        _speedVector = new Vector3(_speed, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < _maximumDamping)
        {
            if (Input.mousePosition.x > _screenWidth - _edgePadding)
            {
                transform.Translate(_speedVector);
            }
            //Debug.Log(Input.mousePosition.x + " || " + (_screenWidth - _edgePadding));
        }
        else if(transform.position.x > -_maximumDamping)
        {
            if (Input.mousePosition.x < _edgePadding)
            {
                transform.Translate(-_speedVector);
            }

        }
    }
}
