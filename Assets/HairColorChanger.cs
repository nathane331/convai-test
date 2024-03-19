using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HairColorChanger : MonoBehaviour
{
    [SerializeField] MeshRenderer hair;
    [SerializeField] SkinnedMeshRenderer scalp;
    [SerializeField] Slider red;
    [SerializeField] Slider green;
    [SerializeField] Slider blue;

    public Color[] hairColors;
    int hairColorIndex;

    [SerializeField] Image hairPreview;

    [SerializeField] MeshRenderer[] hairModels;
    [SerializeField] Button[] hairModelButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hairModelButtons.Length; i++)
        {
            int buttonIndex = i;
            //colorButtons[i].onClick.AddListener(() => puppetCustoms.ColorChanger(buttonIndex, currentButton));
            hairModelButtons[i].onClick.AddListener(() => SwitchHair(buttonIndex));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEdit()
    {
        Color color = new Vector4(0,0,0,1);
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        
        hair.material.SetColor("_Length_Color", color);

        //scalp.material.color = color;
        //scalp.material.SetColor("_EmissionColor", color);

        hairPreview.color = color;
    }

    public void ChangeHairColorPreset(int hairColorIndex)
    {
        Color color = hairColors[hairColorIndex];

        hair.material.color = color;
        hair.material.SetColor("_Length_Color", color); 
        hair.material.SetColor("_Tip_Color", color);

        //scalp.material.color = color;
        //scalp.material.SetColor("_EmissionColor", color);

        red.value = color.r;
        green.value = color.g;
        blue.value = color.b;

        hairPreview.color = color;

    }

    public void SwitchHair(int i)
    {
        foreach(var newHair in hairModels)
        {
            newHair.gameObject.SetActive(false); //turn all models off
        }

        //turn on the right index
        hairModels[i].gameObject.SetActive(true);

        hair = hairModels[i];

    }


}
