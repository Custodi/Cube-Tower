using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaveOfferBar : MonoBehaviour
{
    [SerializeField]
    private Image _progressFill;
    public void SetFill(float to, float duration)
    {
        if (to < 0 || to > 1)
        {

            Debug.LogError("Try to set invalid progress bar value");
        }
        else
        {
            _progressFill.fillAmount = 1;
            _progressFill.DOFillAmount(to, duration).SetEase(Ease.Linear);
        }

    }
}
