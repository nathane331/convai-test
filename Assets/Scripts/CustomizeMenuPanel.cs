using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeMenuPanel : MonoBehaviour
{
    [SerializeField] Transform faceFeatureContent;
    [SerializeField] GameObject FeatureButton;
    
    // Start is called before the first frame update
    public void Show(ClickableFaceFeatureSO clickableFaceFeatureSO)
    {
        if(this.gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }

        for(int i = 0; i < clickableFaceFeatureSO.faceFeaturePresetSO.Count; i++) //for the number of sliders, instantiate the same number of buttons
        {
            GameObject FeatureButtonPrefab = Instantiate(FeatureButton, faceFeatureContent); //create button for feature presets
            //connect buttons to sliders

            FeatureButtonPrefab.GetComponent<FeatureButton>().InheritBlendshapes(clickableFaceFeatureSO.faceFeaturePresetSO[i], clickableFaceFeatureSO);

        }
    }

    // Update is called once per frame
    public void Hide()
    {
       if(this.gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
        }
        if (transform.childCount > 0)
        {
            foreach (Transform child in faceFeatureContent.transform)
            {
                Destroy(child.gameObject);
            }
        }

    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
