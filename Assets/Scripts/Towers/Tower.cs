using UnityEngine;

abstract public class Tower : MonoBehaviour, IToggleable, IInteractable
{
    public enum TowerType { }

    private TowerType _towerType;
    public TowerType towerType { get => _towerType; }

    [SerializeField]
    private TowerUpgradeConfiguration[] _upgradeList;

    [SerializeField]
    protected GameObject _visualRadius;

    [SerializeField]
    protected GameObject towerSubmenu;

    public TowerUpgradeConfiguration[] upgradeList { get => _upgradeList; }

    private int _currentTowerLevel;
    public int currentTowerLevel { get => _currentTowerLevel; protected set => _currentTowerLevel = value; }
    public bool isUpgradeAvailable { get => (_currentTowerLevel + 1 != _upgradeList.Length); }

    private int _price;
    public int price { get => _price; set => _price = value; }

    private float _towerRadius;
    public float towerRadius
    {
        get
        {
            return _towerRadius;
        }
        set
        {
            if (value <= 0)
            {
                throw new System.Exception(" Set _towerRadius zero or less");
            }
            else
            {
                _towerRadius = value;
            }
        }
    }

    private Tile _busyTile;

    protected virtual void Start()
    {
        towerSubmenu.GetComponent<RectTransform>().rotation = Camera.main.transform.rotation;
        towerSubmenu.SetActive(false);
    }

    public void SetTile(Tile tile)
    {
        _busyTile = tile;
        _busyTile.TakeTile();
    }

    public virtual void ToggleObject(bool newState)
    {

    }

    public void OnSelected()
    {
        //Debug.Log("hey");
    }

    public void OnUnselected()
    {
        
    }

    public void OnClicked()
    {
        towerSubmenu.SetActive(true);
        _visualRadius.SetActive(true);
    }

    public void OnUnclicked()
    {
        if (this == null) return;
        towerSubmenu.SetActive(false);
        _visualRadius.SetActive(false);
    }

    public virtual void Upgrade()
    {

    }

    public virtual void Sell()
    {

    }

    private void OnDestroy()
    {
        _busyTile?.FreeTile();
    }
}
