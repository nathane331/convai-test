using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI

public class UIExpressionTrigger : MonoBehaviour
{
    public FaceExpressionManager expressionManager; // FaceExpressionManager

    // OnClick event for button
    public void OnFaceExpressionButtonClicked(string expressionName)
    {
        if (expressionManager != null)
        {
            expressionManager.SetExpression(expressionName);
        }
    }
}
