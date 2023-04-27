using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPropertieCurve : MonoBehaviour
{
    Material[] mats;
    public string materialPropertie = "";
    public AnimationCurve curve;
    // Start is called before the first frame update

    float startTime = 0.0f;
    void Start()
    {
        Process();
    }

    public void Process()
    {
        mats = GetComponent<Renderer>().materials;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Material mat in mats)
        {
            mat.SetFloat("_DissolveAmount", curve.Evaluate(Time.time - startTime));
        }
    }
}
