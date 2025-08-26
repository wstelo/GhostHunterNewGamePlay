using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class DefenderAreaDetector : MonoBehaviour
{
    private List<Enemy> _currentEnemies = new List<Enemy>();

    public Enemy GetFirstEnemies()
    {
        if(_currentEnemies.Count == 0)
        {
            return null;
        }

        return _currentEnemies.First();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            _currentEnemies.Remove(enemy);
        }
    }
}
