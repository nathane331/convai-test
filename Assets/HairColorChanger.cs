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
    [SerializeField] int hairColorIndex;

    [SerializeField] Image hairPreview;

    // Start is called before the first frame update
    void Start()
    {
        
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
        
        hair.material.SetColor("_Grad1_Color", color);

        //scalp.material.color = color;
        //scalp.material.SetColor("_EmissionColor", color);

        hairPreview.color = color;
    }

    public void ChangeHairColorPreset(int hairColorIndex)
    {
        Color color = hairColors[hairColorIndex];

        hair.material.color = color;
        hair.material.SetColor("_Grad1_Color", color); 
        hair.material.SetColor("_Grad2_Color", color);

        //scalp.material.color = color;
        //scalp.material.SetColor("_EmissionColor", color);

        red.value = color.r;
        green.value = color.g;
        blue.value = color.b;

        hairPreview.color = color;

    }


}
