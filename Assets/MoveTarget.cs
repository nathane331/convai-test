using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    public float factorX;
    public float factorY;
    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(Mathf.Sin(Time.time) * factorX, factorY, 0); // Adjust values for desired effect
    }
}
