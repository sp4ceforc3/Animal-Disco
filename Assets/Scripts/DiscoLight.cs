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

    public bool stopColorChange = false;

    private void ChangeColor() {
        ChangeColor(Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax));
    }

    public void ChangeColor(Color? color = null) {
        if (!stopColorChange && color != null)
            sr.color = color.GetValueOrDefault();             
    }
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();  
        sr.color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);      
        InvokeRepeating(nameof(ChangeColor), Random.Range(0.5f, 1.5f), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
