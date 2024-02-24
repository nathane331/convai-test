using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClickableFaceFeature", menuName = "CharacterExpressions/ClickableFaceFeature", order = 3)]
public class ClickableFaceFeatureSO : ScriptableObject
{
    public string faceFeatureName;
    public List<FaceFeaturePresetSO> faceFeaturePresetSO;

    
}
