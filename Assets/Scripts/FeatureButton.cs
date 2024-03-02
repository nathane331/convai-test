using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeatureButton : MonoBehaviour
{
    public FaceFeaturePresetSO inheretedFaceFeaturePreset;
    SkinnedMeshRenderer headRenderer;
    ClickableFaceFeatureSO inheritedClickableFaceFeature;

    void Awake()
    {
        headRenderer = GameObject.FindWithTag("HeadRenderer").GetComponent<SkinnedMeshRenderer>();

        
    }


    public void InheritBlendshapes(FaceFeaturePresetSO faceFeaturePresetSO, ClickableFaceFeatureSO clickableFaceFeatureSO)
    {
       inheretedFaceFeaturePreset = faceFeaturePresetSO;
       inheritedClickableFaceFeature = clickableFaceFeatureSO;
       GetComponentInChildren<TMP_Text>().text = inheretedFaceFeaturePreset.facialFeatureName;
    }


    public void SetFeatures()
    {
        foreach (var blendshape in inheretedFaceFeaturePreset.blendShapeWeights)
        {
            headRenderer.SetBlendShapeWeight(blendshape.blendShapeIndex, blendshape.weight);
        }

    }

    public void ClearFeatures()
    {
        
        for (int i = 0; i < headRenderer.sharedMesh.blendShapeCount; i++)
        {
            headRenderer.SetBlendShapeWeight(i, 0); // Rest to zero
        }
    }
    
}
