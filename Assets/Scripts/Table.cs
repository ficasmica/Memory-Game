using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Table : MonoBehaviour
{
    public Sprite[] frontSprites;
    public bool canFlip = true;
    public GameObject firstCard;
    public GameObject secondCard;
    public float showTime;
    private int pairsLeft;
    private float stopwatch;
    public bool startTime = false;

    void Awake(){
        SetCards();
    }

    void Start(){
        stopwatch = 0f;
    }

    void Update(){
        if (startTime == true){
            stopwatch += Time.deltaTime;
        }
    }

    void SetCards(){
        List<Sprite> frontSpriteList = frontSprites.ToList<Sprite>();
        List<Sprite> spriteList = frontSpriteList.Concat<Sprite>(frontSpriteList).ToList<Sprite>();

        foreach (Transform child in transform){
            int spriteIndex = Random.Range(0, spriteList.Count);
            child.gameObject.GetComponent<Card>().frontSprite = spriteList[spriteIndex];
            spriteList.Remove(spriteList[spriteIndex]);
        }

        pairsLeft = transform.childCount / 2;
    }

    public void CheckCards(){
        if (firstCard.GetComponent<Card>().frontSprite == secondCard.GetComponent<Card>().frontSprite){ // ako je hit
            pairsLeft--;
            if (pairsLeft == 0){
                startTime = false;
                Debug.Log(stopwatch);
            }
            canFlip = true;
            firstCard.GetComponent<Card>().isPaired = true;
            secondCard.GetComponent<Card>().isPaired = true;
            firstCard = null;
            secondCard = null;
        } else{
            StartCoroutine(CloseCards());
        }
    }

    private IEnumerator CloseCards(){
        yield return new WaitForSeconds(showTime);
        firstCard = null;
        secondCard = null;
        canFlip = true;
        foreach (Transform child in transform){
            if (child.gameObject.GetComponent<SpriteRenderer>().sprite == child.gameObject.GetComponent<Card>().frontSprite && child.gameObject.GetComponent<Card>().isPaired != true){
                child.gameObject.GetComponent<Animator>().SetBool("flip", false);
            }
        }
    }


}
