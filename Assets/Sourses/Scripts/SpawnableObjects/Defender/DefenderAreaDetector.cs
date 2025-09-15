using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenderAreaDetector : MonoBehaviour
{
    private List<IDamageable> _currentEnemies = new List<IDamageable>();

    public IDamageable GetNearbyEnemy(List<ElementTypes> elementTypes)
    {
        foreach (var enemy in _currentEnemies)
        {
            if (elementTypes.ExactMatch(enemy.ElementTypes))
            {
                return enemy;
            }

        }

        return null;
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
}
