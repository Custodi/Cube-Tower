using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UITowerUpgradeMenu : MonoBehaviour
{
    [SerializeField]
    private TowerView _towerView;

    [SerializeField]
    private Button _upgradeButton;

    [SerializeField]
    private TMPro.TextMeshProUGUI _text;

    public void Instantiate(TowerView view)
    {
        _towerView = view;
        var v = Camera.main.WorldToScreenPoint(transform.position);
        GetComponent<RectTransform>().SetPositionAndRotation(v, Quaternion.identity);
        if (_towerView.isUpgradeAvailable == false)
        {
            _upgradeButton.interactable = false;
            _text.text = "MAX";
        }
        else
        {
            _upgradeButton.interactable = true;
            _text.text = _towerView.GetNextPrice() + "";
        }
        
    }

    private void Start()
    {
        LevelController.instance.LevelEconomics.OnMoneyUpdate += UpdateUpgradeButton;
        _text.text = _towerView.GetNextPrice() + "";
    }

    public void OnSellTowerButtonClicked()
    {
        _towerView.SellTower();
    }

    private void UpdateUpgradeButton(int oldValue, int newValue)
    {
        //if(newValue > _towerView)
    }

    public void OnUpgradeTowerButtonClicked()
    {
        _towerView.UpgradeTower();
        if (_towerView.isUpgradeAvailable == false)
        {
            _upgradeButton.interactable = false;
            _text.text = "MAX";
        }
        else
        {
            _upgradeButton.interactable = true;
            _text.text = _towerView.GetNextPrice() + "";
        }
    }

    private void OnDestroy()
    {
        LevelController.instance.LevelEconomics.OnMoneyUpdate -= UpdateUpgradeButton;
    }
}
