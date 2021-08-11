using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Image[] hearts; 
    public int health;
    public int numOfHearts;

    public Sprite heartFull;
    public Sprite heartLess;
    
    
    void Update()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        for(int i = 0 ; i < hearts.Length ; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = heartFull;
            }
            else
            {
                hearts[i].sprite = heartLess;
            }

            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
    public void TakeDamage()
    {
       health--;
    }
}
