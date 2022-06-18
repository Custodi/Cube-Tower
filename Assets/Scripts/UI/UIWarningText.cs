using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIWarningText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textWarning;

    private RectTransform _rectTransform;
    private bool _isTweenComplete;
    private Tween _textMovement, _textBluring;
    private Color _initialColor = new Color(1f, 1f, 1f, 1f), _finishColor = new Color(1f, 1f, 1f, 0f);
    private Vector3 _initialPosition = new Vector3(0, 120, 0), _finishPosition = new Vector3(0, 320, 0);
    private float _duration = 1.5f;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    public void UpdateText(string str)
    {
        _textMovement.Kill();
        _textBluring.Kill();
        _rectTransform.anchoredPosition = _initialPosition;
        _textWarning.color = _initialColor;
        _textWarning.text = str;
        _textMovement = _rectTransform.DOAnchorPosY(_finishPosition.y, _duration).SetEase(Ease.Linear);
        _textBluring = _textWarning.DOColor(_finishColor, _duration).SetEase(Ease.Linear);
    }
}
