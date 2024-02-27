
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceFeatureController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer headMesh;
    [SerializeField] private GameObject faceSliderPrefab;
    [SerializeField] private Transform canvas;
    public FaceExpressionManager expressionManager;
    [SerializeField] float weightMutil = 100;

    public Dictionary<int, float> manualSettingDatas = new Dictionary<int, float>();
    public List<string> testString = new List<string>();
    void Start()
    {
        InitializeFaceSliders();
    }

    private void InitializeFaceSliders()
    {
        Mesh skinnedMesh = headMesh.sharedMesh;
        for (int i = 0; i < skinnedMesh.blendShapeCount; i++)
        {
            if (testString.Find(item => item == skinnedMesh.GetBlendShapeName(i)) != null)
            {
                GameObject newSlider = Instantiate(faceSliderPrefab, canvas);
                Slider slider = newSlider.GetComponent<Slider>();
                TMP_Text label = newSlider.GetComponentInChildren<TMP_Text>();

                int blendShapeIndex = i; 
                string name = skinnedMesh.GetBlendShapeName(blendShapeIndex);
                label.text = name;
                UnityEngine.Events.UnityAction<float> call = (value) =>
                {
                    float resultWeight = value * weightMutil;
                    headMesh.SetBlendShapeWeight(blendShapeIndex, resultWeight);
                    if (manualSettingDatas.ContainsKey(blendShapeIndex))
                    {
                        manualSettingDatas[blendShapeIndex] = resultWeight;
                    }
                    else
                    {
                        manualSettingDatas.Add(blendShapeIndex, resultWeight);
                    }
                    //expressionManager.AdjustCustomBlendShapeWeight(headMesh, blendShapeIndex, value); 
                };
                slider.onValueChanged.AddListener(call);
            }

        }
    }

    public void ResetSettingValue()
    {
        Mesh skinnedMesh = headMesh.sharedMesh;

        for (int i = 0; i < skinnedMesh.blendShapeCount; i++)
        {
            if (manualSettingDatas.ContainsKey(i))
                headMesh.SetBlendShapeWeight(i, manualSettingDatas[i]);
        }
    }
}


