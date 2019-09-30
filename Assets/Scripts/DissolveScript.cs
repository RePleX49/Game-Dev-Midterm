﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    Material material;

    public float DissolveTime = 1;
    float SliderVal = 0;
    bool FlipFlop = false;
    bool IsChanging = false;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        if(material)
        {
            Debug.Log(material);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            if(FlipFlop)
            {
                StartCoroutine(Resolve());
                
            }
            else
            {
                StartCoroutine(Dissolve());
                
            }
        }
    }

    IEnumerator Dissolve()
    {    
        if(!IsChanging)
        {
            FlipFlop = true;
            IsChanging = true;
            while (SliderVal < 1)
            {
                SliderVal = Mathf.Lerp(SliderVal, 1.1f, DissolveTime * Time.deltaTime);
                material.SetFloat("Vector1_F2200C18", SliderVal);
                Debug.Log(SliderVal);

                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            IsChanging = false;
        }
        else
        {
            yield return null;
        }      
    }

    IEnumerator Resolve()
    {
        if (!IsChanging)
        {
            FlipFlop = false;
            IsChanging = true;
            while (SliderVal > 0)
            {
                SliderVal = Mathf.Lerp(SliderVal, -0.1f, DissolveTime * Time.deltaTime);
                material.SetFloat("Vector1_F2200C18", SliderVal);
                Debug.Log(SliderVal);

                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
            IsChanging = false;
        }
        else
        {
            yield return null;
        }
    }
}
