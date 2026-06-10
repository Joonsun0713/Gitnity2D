using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GoddessStatue : MonoBehaviour
{
    bool IsHealth = false;
    float timer = 0.0f;
    // Start is called before the first frame update
    PlayerController1 pHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsHealth&& PlayerController1.PlayerLife <100)
        {
            timer += Time.deltaTime;
            if(timer >= 1.0f)
            {
                PlayerController1.PlayerLife += 10;
                timer = 0.0f;
            }
           
            Debug.Log("회복 가능" + PlayerController1.PlayerLife);
        }
       
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
           

          IsHealth = true;
          Debug.Log("회복 가능");

          
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            IsHealth = false;
            
            Debug.Log("회복 끝");
        }
    }
}
