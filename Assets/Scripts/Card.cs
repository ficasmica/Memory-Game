using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Sprite frontSprite;
    public Sprite backSprite;
    private Animator anim;
    private SpriteRenderer rend;
    private Table table;
    public bool isPaired = false;

    void Awake(){
        table = transform.parent.GetComponent<Table>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
    }

    void OnMouseDown(){
        if (table.startTime == false){
            table.startTime = true;
        }
        FlipCard();
    }

    void FlipCard(){
        if (!table.canFlip){
            return;
        } else{
            anim.SetBool("flip", true); // flip animacija
            GetComponent<BoxCollider2D>().enabled = false; // da ne moze dupli klik
            // storniraj slicice za check i start check
            if (table.firstCard == null && table.secondCard == null){
                table.firstCard = gameObject;
            } else if (table.firstCard != null && table.secondCard == null){
                table.secondCard = gameObject;
                table.canFlip = false;
                table.CheckCards();
            }
        }
    }

    public void TurnOnCollider(){
        if (GetComponent<BoxCollider2D>().enabled == false){
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void FlipSprite(){
        rend.sprite = frontSprite;
    }

    public void FlipBack(){
        rend.sprite = backSprite;
    }
}
