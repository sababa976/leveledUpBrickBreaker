using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public SpriteRenderer spriteRednerer {get;private set;}
    public int health {get; private set;}
    public Sprite[] states;

    private void Awake()
    {
        this.spriteRednerer=GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
       
            this.health = this.states.Length;
            this.spriteRednerer.sprite = this.states[health-1];
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Ball")
        {
           
            
            health--;
            if(this.health==0)
            {
                this.gameObject.SetActive(false);
                FindObjectOfType<GameManage>().AddScore(10);
            }
            else
            {
                FindObjectOfType<GameManage>().AddScore(5);
                this.spriteRednerer.sprite = this.states[health-1];
            }

            
            
        }
    }

}
