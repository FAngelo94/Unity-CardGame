using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Number of cards")]
    public int numberCards = 8;

    private GameObject[] playerCards;

	// Use this for initialization
	void Start () {
        playerCards = new GameObject[numberCards];
        for(int i = 0; i < numberCards; i++)
        {
            string nameCard = "card_" + i;
            playerCards[i] = gameObject.transform.Find(nameCard).gameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void EnableCards()
    {
        for (int i = 0; i < numberCards; i++)
        {
            playerCards[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public void DisableCards()
    {
        for (int i = 0; i < numberCards; i++)
        {
            playerCards[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
