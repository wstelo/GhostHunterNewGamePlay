using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

public class Grave : MonoBehaviour
{
    [SerializeField] private MultiColorAreaGenerator _colorGenerator;
    [SerializeField] private List<ElementTypes> _elementTypes = new List<ElementTypes>();                                     /////////////////////////////// Init system in EntryPoint with acc to current MVP
    [SerializeField] private bool _isSettedElements = false;

    public List<ElementTypes> ElementTypes => _elementTypes;
    public bool IsSettedElements => _isSettedElements;                                         ///////////////////////////////// убираем

    public bool IsOccupy { get; private set; } = false;

    [Inject] ConfigsRepository configRepository;

    private void Awake()
    {
        if (IsSettedElements == true)
        {
            SetColors(_elementTypes);
        }
    }

    public void Init(List<ElementTypes> elementTypes)
    {
        if (_isSettedElements == false)
        {
            _elementTypes = elementTypes;
        }

        SetColors(_elementTypes);
    }

    public void Init(ElementTypes elementType)
    {
        if (_isSettedElements == false)
        {
            _elementTypes.Add(elementType);
        }

        SetColors(_elementTypes);
    }

    public void Occupy()
    {
        IsOccupy = true;
    }

    private void SetColors(List<ElementTypes> elementTypes)
    {
        List<Color> colors = new List<Color>();

        foreach (var item in configRepository.ElementConfigs)                                 //////////////////////////////////// COLOR SYSTEM REPLACE
        {
            foreach (var element in elementTypes)
            {
                if (element == item.Type)
                {
                    colors.Add(item.Color);
                }
            }
        }

        _colorGenerator.Init(colors);
    }
}
