using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigsRepository : MonoBehaviour
{
    [SerializeField] private List<ElementConfig> _configList = new List<ElementConfig>();

    public List<ElementConfig> ConfigList => _configList;
}
