using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevelInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Button[] _difficultButtons;

    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private TextMeshProUGUI _wavesCount;

    private int _selectedDifficulty;
    private int _levelScene;
    
    void Start()
    {
        OnDifficultChanged(1);
    }

    public void Instantiate(int level, string description, int wavesCount)
    {
        Debug.Log(level);
        _levelText.text = "Level " + level.ToString();
        _description.text = description.ToString();
        _wavesCount.text = wavesCount + " waves";
        _levelScene = level;
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void OnDifficultChanged(int difficult)
    {
        _selectedDifficulty = difficult;
        for(int i = 0; i < _difficultButtons.Length; i++)
        {
            if(difficult == i)
            {
                _difficultButtons[i].interactable = false;
                _difficultButtons[i].GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 1f);
                _difficultButtons[i].GetComponent<Image>().color = new Color(0.3333333f, 0.372549f, 0.4509804f);
                _difficultButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
            else
            {
                _difficultButtons[i].interactable = true;
                _difficultButtons[i].GetComponent<RectTransform>().localScale = Vector3.one;
                _difficultButtons[i].GetComponent<Image>().color = Color.white;
                _difficultButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = new Color(0.1764706f, 0.254902f, 0.3333333f);
            }
        }
    }

    public void StartLevel()
    {
        GameManager.instance.LoadLevel(_levelScene, _selectedDifficulty);
    }
}
