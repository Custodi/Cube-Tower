using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class MenuCameraTransition : MonoBehaviour
{
    [SerializeField]
    private Vector3 _firstPosition;

    [SerializeField]
    private Vector3 _firstRotation;

    [SerializeField]
    private Vector3 _secondPosition;

    [SerializeField]
    private Vector3 _secondRotation;

    [SerializeField]
    private Vector3 _thirdPosition;

    [SerializeField]
    private Vector3 _thirdRotation;

    [SerializeField]
    private Vector3 _fourthPosition;

    [SerializeField]
    private Vector3 _fourthRotation;

    [SerializeField]
    private GameObject _camera;

    public void CameraTransition(int goalTransition)
    {
        switch(goalTransition)
        {
            case 1:
                {
                    _camera.transform.DOMove(_firstPosition, 2f);
                    _camera.transform.DORotate(_firstRotation, 2f);
                    break;
                }
            case 2:
                {
                    _camera.transform.DOMove(_secondPosition, 2f);
                    _camera.transform.DORotate(_secondRotation, 2f);
                    break;
                }
            case 3:
                {
                    //Debug.Log(_camera);
                    _camera.transform.DOMove(_thirdPosition, 2f);
                    _camera.transform.DORotate(_thirdRotation, 2f);
                    break;
                }
            case 4:
                {
                    _camera.transform.DOMove(_fourthPosition, 2f);
                    _camera.transform.DORotate(_fourthRotation, 2f);
                    break;
                }
        }
    }
}
