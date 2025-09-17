using System.Collections.Generic;
using UnityEngine;

public class DefenceAreaDetector : MonoBehaviour
{
    private List<IDamageable> _currentEnemies = new List<IDamageable>();


    public IDamageable GetNearbyEnemy(List<ElementTypes> elementTypes)
    {
        IDamageable nearbyEnemy = null;


        foreach (var enemy in _currentEnemies)
        {
            if (elementTypes.ExactMatch(enemy.ElementTypes))
            {
                if (nearbyEnemy == null)
                {
                    nearbyEnemy = enemy;
                }
                else
                {
                    if (transform.position.SqrDistance(enemy.Transform.position) < transform.position.SqrDistance(nearbyEnemy.Transform.position))
                    {
                        nearbyEnemy = enemy;
                    }
                }
            }
        }

        if (nearbyEnemy == null)
        {
            return null;
        }
        else
        {
            return nearbyEnemy;
        }
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.TryGetComponent(out IDamageable enemy))
    {
        _currentEnemies.Add(enemy);
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.TryGetComponent(out IDamageable enemy))
    {
        _currentEnemies.Remove(enemy);
    }
}

public void Delete(IDamageable enemy)
{
    _currentEnemies.Remove(enemy);
}

public void Clear()
{
    _currentEnemies.Clear();
}
}
