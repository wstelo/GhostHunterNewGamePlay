using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;

    private ProjectileViewHandler _projectileViewHandler;
    private SpawnerHandler<Projectile> _spawnHandler;
    private UnitLineHandler _unitLineHandler;
    private UnityEngine.Coroutine _currentCoroutine = null;
    private List<ChargingProjectileCell> _chargingProjectiles;
    private ChargingProjectileCell _firstCell;

    public void Init(ProjectileViewHandler projectileViewHandler, SpawnerHandler<Projectile> spawnHandler, UnitLineHandler unitLineHandler)
    {
        _projectileViewHandler = projectileViewHandler;
        _spawnHandler = spawnHandler;
        _unitLineHandler = unitLineHandler;
    }

    private void OnEnable()
    {
        _chargingProjectiles = _projectileViewHandler.GetProjectilesCells();

        _firstCell = _chargingProjectiles.First();
    }

    private void FixedUpdate()
    {
        if (_firstCell.IsActive && _currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(TryShot());
        }
        else
        {
            _projectileViewHandler.SwapProjectileCells();
        }
    }

    private IEnumerator TryShot()
    {
        Ghost currentTarget = _unitLineHandler.GetFirstTarget();

        currentTarget.Destroyed += ResetButtons;

        if (_firstCell.Type == currentTarget.Type)
        {
            var wait = new WaitForSeconds(1f);

            Projectile currentProjectile = _spawnHandler.Spawn(_firstCell.Type, spawnPosition.position);
            currentProjectile.Init(_firstCell.Color, _firstCell.Type);
            currentProjectile.SetTarget(currentTarget);
            _firstCell.DecreaseCount();

            yield return wait;

            _currentCoroutine = null;
        }
    }

    private void ResetButtons(Ghost spawnableObject)
    {

        _projectileViewHandler.InitializeButtons();
        spawnableObject.Destroyed -= ResetButtons;
    }
}
