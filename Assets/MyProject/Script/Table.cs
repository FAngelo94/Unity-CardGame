using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour {

    public static Table instance = null;

    [Header("Rows in the table")]
    public int rows = 4;
    [Header("Columns in the table")]
    public int columns = 4;

    [Header("Color for player 1")]
    public Color colorPlayer_1 = Color.green;
    [Header("Color for player 2")]
    public Color colorPlayer_2 = Color.red;
    [Header("Text for the final message")]
    public Text textUI;

    [Header("Script Player 1")]
    public Player player_1;
    [Header("Script Player 2")]
    public Player player_2;

    private GameObject [,]cardSlots;
    private SingleCard [,]cards;

    private int playerTurn;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        cards = new SingleCard[4, 4];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                cards[i, j] = new SingleCard
                {
                    top = 0,
                    bottom = 0,
                    right = 0,
                    left = 0,
                    who = 0
                };
            }
        }

        cardSlots = new GameObject[4, 4];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                string nameChild = "CardSlot_" + (i * 4 + j);
                
                cardSlots[i, j] = gameObject.transform.Find(nameChild).gameObject;
                cardSlots[i, j].gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }

        textUI.enabled = false;

        playerTurn = 2;
        ChangeTurn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PutCard(int row,int column,Card newCard)
    {
        Debug.Log("Coordinates=" + row + "-" + column);
        cards[row, column].who = newCard.player;
        cards[row, column].top = newCard.top;
        cards[row, column].bottom = newCard.bottom;
        cards[row, column].left = newCard.left;
        cards[row, column].right = newCard.right;
        cardSlots[row, column].gameObject.GetComponent<SpriteRenderer>().color = (newCard.player == 1 ? colorPlayer_1 : colorPlayer_2);
 
        Debug.Log(cards[row, column].who + " - " + cards[row, column].top + " - " + cards[row, column].bottom + " - " + cards[row, column].right + " - " + cards[row, column].left);


        if (row > 0 && cards[row - 1, column].who!=0 && cards[row-1,column].bottom<newCard.top && newCard.player!= cards[row - 1, column].who)
        {
            cardSlots[row - 1, column].gameObject.GetComponent<SpriteRenderer>().color = (newCard.player == 1 ? colorPlayer_1 : colorPlayer_2);
            cards[row - 1, column].who = newCard.player;
        }
        if (row < 3 && cards[row + 1, column].who != 0 && cards[row + 1, column].top < newCard.bottom && newCard.player != cards[row + 1, column].who)
        {
            cardSlots[row + 1, column].gameObject.GetComponent<SpriteRenderer>().color = (newCard.player == 1 ? colorPlayer_1 : colorPlayer_2);
            cards[row + 1, column].who = newCard.player;
        }
        if (column > 0 && cards[row, column - 1].who != 0 && cards[row, column - 1].right < newCard.left && newCard.player != cards[row, column - 1].who)
        {
            cardSlots[row, column - 1].gameObject.GetComponent<SpriteRenderer>().color = (newCard.player == 1 ? colorPlayer_1 : colorPlayer_2);
            cards[row, column - 1].who = newCard.player;
        }
        if (column < 3 && cards[row, column + 1].who != 0 && cards[row, column + 1].left < newCard.right && newCard.player != cards[row, column + 1].who)
        {
            cardSlots[row, column + 1].gameObject.GetComponent<SpriteRenderer>().color = (newCard.player == 1 ? colorPlayer_1 : colorPlayer_2);
            cards[row, column + 1].who = newCard.player;
        }
        CheckEnd();
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        Debug.Log("player turn=" + playerTurn);
        if(playerTurn==1)
        {
            playerTurn = 2;
            player_1.DisableCards();
            player_2.EnableCards();
        }
        else
        {
            playerTurn = 1;
            player_1.EnableCards();
            player_2.DisableCards();
        }
    }

    private void CheckEnd()
    {
        int pointPlayer1 = 0;
        int pointPlayer2 = 0;
        foreach (SingleCard singleCard in cards)
        {
            if (singleCard.who == 1)
                pointPlayer1++;
            if(singleCard.who == 2)
                pointPlayer2++;
            
        }
        Debug.Log(pointPlayer1 + " - " + pointPlayer2);
        if (pointPlayer2+pointPlayer1 == 16)
        {
            textUI.enabled = true;
            if (pointPlayer1 != pointPlayer2)
            {
                if(pointPlayer1 > pointPlayer2)
                {
                    textUI.text = "Ha vinto il giocatore 1 \n ( "+pointPlayer1+" - "+pointPlayer2+" )";
                }
                else
                {
                    textUI.text = "Ha vinto il giocatore 2 \n ( " + pointPlayer1 + " - " + pointPlayer2 + " )";
                }
            }
            else
            {
                textUI.text = "Pareggio";
            }
        }
    }
}

struct SingleCard
{
    public int top;
    public int bottom;
    public int right;
    public int left;
    public int who;
}
