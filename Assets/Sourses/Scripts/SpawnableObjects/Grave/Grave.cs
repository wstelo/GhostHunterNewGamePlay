using System.Collections;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Splines;

public class Grave : MonoBehaviour
{
    [SerializeField] private List<ElementTypes> elementTypes = new List<ElementTypes>();                                     /////////////////////////////// Init system in EntryPoint with acc to current MVP

    public List<ElementTypes> ElementTypes => elementTypes;
}
