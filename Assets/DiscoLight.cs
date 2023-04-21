using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    SpriteRenderer sr;

    float hueMin = 0f;
    float hueMax = 1f;
    float saturationMin = 0.5f;
    float saturationMax = 1f;
    float valueMin = 0.5f;
    float valueMax = 1f;

    void ChangeColor()
    {
        sr.color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();        
        InvokeRepeating(nameof(ChangeColor), 0.1f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
