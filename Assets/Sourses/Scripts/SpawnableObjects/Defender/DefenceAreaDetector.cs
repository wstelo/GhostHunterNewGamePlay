using System.Collections.Generic;
using UnityEngine;

public class DefenceAreaDetector : MonoBehaviour
{
    private List<Enemy> _currentEnemies = new List<Enemy>();

    public Enemy GetNearbyEnemy(List<ElementTypes> elementTypes)
    {
        Enemy nearbyEnemy = null;

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
        if (other.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Remove(enemy);
        }
    }

    public void Delete(Enemy enemy)
    {
        _currentEnemies.Remove(enemy);
    }

    public void Clear()
    {
        _currentEnemies.Clear();
    }
}
