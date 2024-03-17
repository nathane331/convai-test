using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeColorModifier : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skin, scalp;
    [SerializeField] Slider red;
    [SerializeField] Slider green;
    [SerializeField] Slider blue;

    public Color[] skinColors;
    [SerializeField] int skinColorIndex;

    [SerializeField] Image skinPreview;

    public Texture[] skinNormals;
    public Texture[] skinAlbedo;

    public void OnEdit()
    {
        Color color = skin.material.color;
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        skin.material.color = color;
        //skin.material.SetColor("_EmissionColor", color);

        scalp.material.color = color;
       // scalp.material.SetColor("_EmissionColor", color);

        skinPreview.color = color;
    }

    public void ChangeSkinColorPreset(int skinColorIndex)
    {
        Color color = skinColors[skinColorIndex];

        skin.material.color = color;
       // skin.material.SetColor("_EmissionColor", color);

        scalp.material.color = color;
        //scalp.material.SetColor("_EmissionColor", color);

        red.value = color.r;
        green.value = color.g;
        blue.value = color.b;

        skinPreview.color = color;

    }

    public void ChangeSkinNormalPreset(int skinNormalsIndex)
    {
        Texture newNormal = skinNormals[skinNormalsIndex];

        skin.material.SetTexture("_BumpMap", newNormal);
    }

    public void ChangeSkinAlbedoPreset(int skinAlbedoIndex)
    {
        Texture newAlbedo = skinAlbedo[skinAlbedoIndex];

        skin.material.SetTexture("_MainTex", newAlbedo);
    }

}
