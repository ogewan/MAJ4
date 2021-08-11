using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defensebarrier : MonoBehaviour
{
    public float LastingTime;
    public float CountingTime = 0f;
    public bool isActivated = false;
    private float i =  1f;
    public GameObject BarrierForDefense;

    

    
    void Update()
    {
     if(isActivated == true)
     {  
         if( i > 0 )
         {
            BarrierForDefense.SetActive(true);
            i--;  
         }
        if(CountingTime > LastingTime)
            {
            BarrierForDefense.SetActive(false);
            isActivated = false;
            CountingTime = 0f;
            }
        else
            {
            BarrierForDefense.SetActive(true);
            CountingTime += Time.deltaTime;
            }  
    }
    else
    {
        BarrierForDefense.SetActive(false);
    }
     }
}
