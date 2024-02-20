using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FaceExpression", menuName = "CharacterExpressions/FaceExpression", order = 1)]
public class FaceExpression : ScriptableObject
{
    public string expressionName;
    public List<BlendShapeWeight> blendShapeWeights;

    [System.Serializable]
    public class BlendShapeWeight
    {
        public string rendererIdentifier;
        public int blendShapeIndex;
        public float weight;
    }
}