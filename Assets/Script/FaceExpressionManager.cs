using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceExpressionManager : MonoBehaviour
{
    public List<FaceExpression> faceExpressions;
    private Dictionary<string, SkinnedMeshRenderer> renderersByName;

    void Awake()
    {
        InitializeRenderers();
    }

    private void InitializeRenderers()
    {
        renderersByName = new Dictionary<string, SkinnedMeshRenderer>();
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            if (!renderersByName.ContainsKey(renderer.gameObject.name))
            {
                renderersByName.Add(renderer.gameObject.name, renderer);
            }
        }
    }

    public void SetExpression(string expressionName)
    {
        FaceExpression expression = faceExpressions.Find(e => e.expressionName.Equals(expressionName));
        if (expression != null)
        {
            ResetAllBlendShapes(); // Rest BlendShapes
            ApplyExpression(expression);
        }
    }

    private void ResetAllBlendShapes()
    {
        foreach (var rendererEntry in renderersByName)
        {
            SkinnedMeshRenderer renderer = rendererEntry.Value;
            int blendShapeCount = renderer.sharedMesh.blendShapeCount;
            for (int i = 0; i < blendShapeCount; i++)
            {
                renderer.SetBlendShapeWeight(i, 0); // Rest to zero
            }
        }
    }

    private void ApplyExpression(FaceExpression expression)
    {
        foreach (var blendShape in expression.blendShapeWeights)
        {
            if (renderersByName.TryGetValue(blendShape.rendererIdentifier, out SkinnedMeshRenderer renderer))
            {
                renderer.SetBlendShapeWeight(blendShape.blendShapeIndex, blendShape.weight);
            }
        }
    }
}