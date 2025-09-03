using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenderAreaDetector : MonoBehaviour
{
    private List<Enemy> _currentEnemies = new List<Enemy>();

    public Enemy GetNearbyEnemyByType(ElementTypes type)            /////////////////////////////////////////////////
    {
        List<Enemy> currentEnemies = new List<Enemy>();

        foreach (var item in _currentEnemies)
        {
            if (item.IsMultiType == false)
            {
                if(item.ElementTypes.First() == type)
                {
                    currentEnemies.Add(item);
                }               
            }
        }

        if (currentEnemies.Count == 0)
        {
            return null;
        }

        return currentEnemies.First();
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
}
