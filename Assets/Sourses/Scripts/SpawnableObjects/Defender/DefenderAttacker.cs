using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderAttacker
{
    private DefenderAnimatorController _animatorController;
    private DefenderAreaDetector _detector;
    private SpawnerHandler _spawnerHandler;
    private DefenderAttackTypes _currentAttackType;
    private float _attackDelay;
    private Coroutine _currentCoroutine;
    private ProjectileTypes _projectileTypes;
    private ElementTypes _currentElement;
    private Vector3 _spawnPosition;

    public DefenderAttacker(
        DefenderAreaDetector detector, 
        SpawnerHandler spawnerHandler, 
        DefenderAttackTypes attackType, 
        float attackDelay, 
        ProjectileTypes projectileType, 
        ElementTypes elementType, 
        Vector3 spawnPosition, 
        DefenderAnimatorController animatorController)
    {
        _detector = detector;
        _spawnerHandler = spawnerHandler;
        _attackDelay = attackDelay;
        _projectileTypes = projectileType;
        _currentElement = elementType;
        _spawnPosition = spawnPosition;
        _animatorController = animatorController;
    }

    public void UpdateAttack(Defender defender, Enemy currentEnemy)
    {        
        if (_currentAttackType == DefenderAttackTypes.StandartAttack && _currentCoroutine == null && currentEnemy != null && currentEnemy.ElementType == _currentElement)
        {
            _currentCoroutine = defender.StartCoroutine(Attack(currentEnemy));         /////////////////////////////////////////////////////////////////////////         корутина из другого класса?
        }
    }

    public IEnumerator Attack(Enemy target)
    {
        var wait = new WaitForSeconds(_attackDelay);

        _animatorController.StartAttackAnimation();
        Projectile currentProjectile = _spawnerHandler.SpawnProjectile(_projectileTypes, _currentElement, _spawnPosition);
        currentProjectile.SetTarget(target);

        yield return wait;

        _currentCoroutine = null;
    }
}
