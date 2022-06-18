using UnityEngine;
using DG.Tweening;

public class LevelMarker : MonoBehaviour, IInteractable
{
    [SerializeField]
    private UILevelInfoPanel _levelWindowRef;

    [SerializeField]
    private int _level;

    [SerializeField]
    private string _description;

    //[SerializeField]
    //private Material _uncompletedLevelColor;

    //[SerializeField]
    //private Material _completedLevelColor;

    [SerializeField]
    private GameObject _completedFlag;

    [SerializeField]
    private GameObject _uncompletedFlag;

    [SerializeField]
    private WaveList _waveList;

    [SerializeField]
    private float _selectionHeight;

    private MenuCameraTransition _cameraTransitionScript;
    private Vector3 _initPosition;
    private bool _isSelectedOnce = false;

    // Start is called before the first frame update
    private void Start()
    {
        _initPosition = transform.position;
        _cameraTransitionScript = Camera.main.GetComponent<MenuCameraTransition>();
        //Debug.Log("Player prefs loaded level " + _level + " to " + PlayerPrefs.GetInt("Level" + _level) + " ||" + "Level" + _level);
        if (PlayerPrefs.GetInt("Level" + _level) == 1)
        {
            //Debug.Log(GetComponent<MeshRenderer>());
            // Issue: Doesn't change material, function was called, component was setted
            // Solution: Set new line of flags with nessesary color, SetActive() for changing
            //GetComponent<MeshRenderer>().materials[1] = _completedLevelColor;
            _completedFlag.SetActive(true);
            _uncompletedFlag.SetActive(false);
        }
        //else GetComponent<MeshRenderer>().materials[1] = _uncompletedLevelColor;
    }

    public void OnClicked()
    {
        _cameraTransitionScript.CameraTransition(4);
        _levelWindowRef.GetComponent<UILevelInfoPanel>().Instantiate(_level, _description, _waveList._wavesList.Length);
        _levelWindowRef.gameObject.SetActive(true);
    }

    public void OnSelected()
    {
        if (_isSelectedOnce == false)
        {
            transform.DOMoveY(_initPosition.y + _selectionHeight, 0.5f).OnComplete(() => _isSelectedOnce = true);
            
        }
    }

    public void OnUnselected()
    {
        transform.DOMoveY(_initPosition.y, 0.5f).OnComplete(() => _isSelectedOnce = false);
    }

    public void OnUnclicked()
    {

    }
}
