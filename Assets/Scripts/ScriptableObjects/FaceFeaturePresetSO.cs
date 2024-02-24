using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FacialFeaturePreset", menuName = "CharacterExpressions/FacialFeaturePreset", order = 2)]
public class FaceFeaturePresetSO : ScriptableObject

{
    public string facialFeatureName;
    public List<BlendShapeWeight> blendShapeWeights;

    [System.Serializable]
    public class BlendShapeWeight
    {
        public int blendShapeIndex;
        public float weight;
    }
    
}
