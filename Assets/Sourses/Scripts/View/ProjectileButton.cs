using UniRx;
using UnityEngine;

public class ProjectileButton : MonoBehaviour
{
    [SerializeField] private ProjectileButtonDragHandler _dragHandler;
    [SerializeField] private ProjectileButtonView _view;

    private ProjectileCell _cell ;

    private void Awake()
    {
        _dragHandler.DataHolderDetected += DetectDataHolder;
    }

    private void DetectDataHolder(CellDataHolder dataHolder)
    {
        dataHolder.AddCells(_cell);
    }

    public void Init(ProjectileCell projectileCell)
    {
        gameObject.SetActive(true);
        _cell = projectileCell;
        _view.Init(_cell.Count, _cell.Color);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _dragHandler.DataHolderDetected -= DetectDataHolder;
    }
}
