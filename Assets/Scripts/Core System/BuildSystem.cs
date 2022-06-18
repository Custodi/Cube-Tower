using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [SerializeField] private BuildCursorHandler _cursorHandler;
    [SerializeField] private TowerList _towerList;

    private Tower _towerScript;

    public bool isBuildMode { private set; get; }

    private Tile _constructionTile;

    public void SelectTower(int tower_id)
    {
        _cursorHandler.constractionTower = Instantiate(_towerList.TowerListCollection[tower_id]);
        _towerScript = _cursorHandler.constractionTower.GetComponent<Tower>();
        isBuildMode = true;
    }

    private void Update()
    {
        if (isBuildMode)
        {
            if (Input.GetMouseButtonDown(1))
            {
                DeclineBuildTower();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                TryBuildTower();
            }
        }
    }

    private void DeclineBuildTower()
    {
        isBuildMode = false;
        Destroy(_cursorHandler.constractionTower);
        _cursorHandler.ToggleBuildSystem(isBuildMode);
    }

    private bool TryBuildTower()
    {
        _constructionTile = _cursorHandler.selectedTile;
        if (_constructionTile != null)
        {
            if (LevelController.instance.LevelEconomics.CompareMoney(_towerScript.price) && _constructionTile.isBusy == false)
            {
                isBuildMode = false;
                var towerScript = _cursorHandler.constractionTower.GetComponent<Tower>();
                towerScript.ToggleObject(true);
                towerScript.SetTile(_constructionTile);
                LevelController.instance.LevelEconomics.TryTakeMoney(_towerScript.price, out _);
                _cursorHandler.ToggleBuildSystem(isBuildMode);
                return true;
            }
            else
            {
                LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.WarningUpdate, "You don't have enough money!");
                return false;
            }
        }
        else
        {
            LevelController.instance.LevelUIManager.AddRequest(ViewRequestType.WarningUpdate, "You cannot build here");
            return false;
        }
       
    }

}
