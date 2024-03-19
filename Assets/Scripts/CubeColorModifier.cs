using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CubeColorModifier : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer skin, scalp;
    [SerializeField] Slider red;
    [SerializeField] Slider green;
    [SerializeField] Slider blue;

    public Color[] skinColors;
    [SerializeField] int skinColorIndex;

    [SerializeField] int skinTypeIndex = 0;

    [SerializeField] Image skinPreview;

    public Texture[] skinNormals;
    public Texture[] skinAlbedo;

    public TMP_Text labeltext;

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

        skin.material.SetTexture("_BaseColorMap", newAlbedo);
    }

    public void IncreaseSkinTypeIndex()
    {
        if(skinTypeIndex == skinAlbedo.Length-1)
            skinTypeIndex = 0;
        else
            skinTypeIndex++;

        Texture newAlbedo = skinAlbedo[skinTypeIndex];

        skin.material.SetTexture("_BaseColorMap", newAlbedo);

        Texture newNormal = skinNormals[skinTypeIndex];

        skin.material.SetTexture("_NormalMap", newNormal);

        labeltext.text = skinTypeIndex.ToString();
    }

    public void DecreaseSkinTypeIndex()
    {
        if (skinTypeIndex == 0)
            skinTypeIndex = skinAlbedo.Length-1;
        else
            skinTypeIndex--;

        Texture newAlbedo = skinAlbedo[skinTypeIndex];

        skin.material.SetTexture("_BaseColorMap", newAlbedo);

        Texture newNormal = skinNormals[skinTypeIndex];

        skin.material.SetTexture("_NormalMap", newNormal);

        labeltext.text = skinTypeIndex.ToString();
    }

}
