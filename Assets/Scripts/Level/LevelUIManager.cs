using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextView))]
public class LevelUIManager : MonoBehaviour
{
    [Header("UI Prefabs")]
    [SerializeField]
    private GameObject _enemyInfoPanelPrefab;

    [SerializeField]
    private GameObject _towerInfoPanelPrefab;

    [SerializeField]
    private GameObject _gameInPanelPrefab;

    [SerializeField]
    private UIWarningText _warningText;

    private UITowerPanel _uITowerPanel;
    private UIEnemyPanel _uIEnemyPanel;
    private UIInGamePanel _uiInGamePanel;
    private TextView _textView;

    public delegate bool RequestProcessedEvent(View view, ViewRequestType viewRequestType);
    public RequestProcessedEvent OnRequestProcessed;

    private Dictionary<View, ViewRequestType> _requestDictionary;

    public void AddRequest(ViewRequestType viewRequestType, View view)
    {
        if (!_requestDictionary.ContainsKey(view))
        {
            _requestDictionary.Add(view, viewRequestType);
        }
    }

    public void AddRequest(ViewRequestType viewRequestType, string msg)
    {
        _textView.message = msg;
        if (!_requestDictionary.ContainsKey(_textView))
        {
            _requestDictionary.Add(_textView, viewRequestType);
        }
    }

    private void Awake()
    {
        _requestDictionary = new Dictionary<View, ViewRequestType>();
        _uIEnemyPanel = _enemyInfoPanelPrefab.GetComponent<UIEnemyPanel>();
        //_uITowerPanel = _towerInfoPanelPrefab.GetComponent<UITowerPanel>(); Убрать при окончании создания
        _uiInGamePanel = _gameInPanelPrefab.GetComponent<UIInGamePanel>();
        _textView = GetComponent<TextView>();
    }

    private void Update()
    {
        if(_requestDictionary.Count > 0)
        {
            foreach(KeyValuePair<View, ViewRequestType> item in _requestDictionary)
            {
                switch (item.Value)
                {
                    case ViewRequestType.EnemySelected:
                    {
                        _uIEnemyPanel.Instantiate((EnemyView)item.Key);
                        break;
                    }
                    case ViewRequestType.TowerSelected:
                    {
                        //_uITowerPanel.Instantiate();
                        break;
                    }
                    case ViewRequestType.DestroyInfoPanel:
                    {
                        //Debug.Log(4);
                        ClearInfoPanel();
                        break;
                    }
                    case ViewRequestType.LevelHealthUpdate:
                    {
                        //Debug.Log("HP was called " + ((LevelHealthView)item.Key).LevelHP);
                        _uiInGamePanel.UpdateUIHealth(((LevelHealthView)item.Key).LevelHP);
                        break;
                    }
                    case ViewRequestType.MoneyUpdate:
                    {
                        //Debug.Log(1);
                        _uiInGamePanel.UpdateUIMoney(((LevelEconomicsView)item.Key).Money);
                        break;
                    }
                    case ViewRequestType.WaveUpdate:
                    {
                        var view = (EnemySpawnerView)item.Key;
                        _uiInGamePanel.UpdateWave(view.currentWave + 1, view.waveCount);
                        break;
                    }
                    case ViewRequestType.WarningUpdate:
                    {
                        _warningText.UpdateText(((TextView)item.Key).message);
                        break;
                    }
                    case ViewRequestType.Destroy:
                    {

                        break;
                    }
                }
            }
            _requestDictionary.Clear();
        }
    }

    private void ClearInfoPanel()
    {
       //Debug.Log(2);
        _uIEnemyPanel.Clear();
        //_uITowerPanel.Clear();
    }

}
