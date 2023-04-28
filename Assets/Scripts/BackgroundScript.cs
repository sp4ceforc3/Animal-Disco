using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    SpriteRenderer sr;
    float H;
    float S;
    float V;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Color.RGBToHSV(sr.color, out H,  out S, out V);
            V -= 0.1f;
            sr.color = Color.HSVToRGB(H, S, V);
        } 

        if (Input.GetKeyDown(KeyCode.P))
        {
            Color.RGBToHSV(sr.color, out H, out S, out V);
            V += 0.1f;
            sr.color = Color.HSVToRGB(H, S, V);
        }
    }
}
