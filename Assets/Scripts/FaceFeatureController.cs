using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FaceFeatureController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer headMesh;
    [SerializeField] private GameObject faceSlider;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Transform canvas;

    Mesh skinnedMesh;
    public GameObject[] faceSliders;
    


    // Start is called before the first frame update
    void Start()
    {
        /*
        faceSlider.onValueChanged.AddListener((v) =>
        {
            headMesh.SetBlendShapeWeight(79, v);
            text.text = v.ToString("0.00");
        });

        faceSlider2.onValueChanged.AddListener((v) =>
        {
            headMesh.SetBlendShapeWeight(80, v);
            text.text = v.ToString("0.00");
        });
        */

        skinnedMesh = headMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh;

        faceSliders = new GameObject[skinnedMesh.blendShapeCount-79];
        int j = 0;

        for (int i = 79; i < skinnedMesh.blendShapeCount-1; i++)
        {
            int copy = i;
            GameObject newSlider = Instantiate(faceSlider, canvas);
            
            faceSliders[j] = newSlider;
            faceSliders[j].GetComponentInChildren<TMP_Text>().text = skinnedMesh.GetBlendShapeName(i).Replace("Face: ",""); 

            faceSliders[j].GetComponent<Slider>().onValueChanged.AddListener((v) =>
            {
                headMesh.SetBlendShapeWeight(copy, v);
                //faceSliders[j].GetComponentInChildren<TMP_Text>().text = v.ToString("0.00");

            });
            j++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
