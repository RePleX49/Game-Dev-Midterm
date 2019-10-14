using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour
{
    Material material;
    Collider BoxComponent;

    [SerializeField] float DissolveTime = 1;
    float SliderVal = 1;
    bool FlipFlop = false;
    bool IsChanging = false;
    bool IsDissolved = true;
    [SerializeField] bool StartDissolved = true;
    [SerializeField] bool PlayerCanAffect = true;

    // Start is called before the first frame update
    void Start()
    {
        BoxComponent = GetComponent<Collider>();
        material = GetComponent<MeshRenderer>().material;
        
        if(!StartDissolved)
        {
            StartCoroutine(Resolve());
        }
    }

    public void FlipFlopEffect()
    {
        if (!IsDissolved && PlayerCanAffect)
        {
            StartCoroutine(Dissolve());
        }
        else
        {
            StartCoroutine(Resolve());
        }
    }

    public IEnumerator Dissolve()
    {    
        if(!IsChanging)
        {
            IsDissolved = true;
            IsChanging = true;
            BoxComponent.enabled = false;
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
            IsDissolved = false;
            IsChanging = true;
            BoxComponent.enabled = true;
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
