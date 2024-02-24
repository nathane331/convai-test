using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeatureButton : MonoBehaviour
{
    public FaceFeaturePresetSO inheretedFaceFeaturePreset;
    SkinnedMeshRenderer headRenderer;

    void Awake()
    {
        headRenderer = GameObject.FindWithTag("HeadRenderer").GetComponent<SkinnedMeshRenderer>();

        
    }


    public void InheritBlendshapes(FaceFeaturePresetSO faceFeaturePresetSO)
    {
       inheretedFaceFeaturePreset = faceFeaturePresetSO;
       GetComponentInChildren<TMP_Text>().text = inheretedFaceFeaturePreset.facialFeatureName;
    }


    public void SetFeatures()
    {
        foreach (var blendshape in inheretedFaceFeaturePreset.blendShapeWeights)
        {
            headRenderer.SetBlendShapeWeight(blendshape.blendShapeIndex, blendshape.weight);
        }

    }
    
}
