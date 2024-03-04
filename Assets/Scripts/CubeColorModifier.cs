using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeColorModifier : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skin;
    [SerializeField] Slider red;
    [SerializeField] Slider green;
    [SerializeField] Slider blue;

    public void OnEdit()
    {
        Color color = skin.material.color;
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        skin.material.color = color;
        skin.material.SetColor("_EmissionColor", color);
    }
}
