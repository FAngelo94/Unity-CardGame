using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour {

    [Header("row in the table")]
    public int row;
    [Header("column in the table")]
    public int column;

    private bool free;

	// Use this for initialization
	void Start () {
        free = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool PutCard(GameObject card,Card cardClass)
    {
        if (free)
        {
            free = false;
            card.transform.position = new Vector2(transform.position.x, transform.position.y);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            gameObject.GetComponent<Collider2D>().enabled = false;
            card.GetComponent<Card>().enabled = false;
            Table.instance.PutCard(row, column, cardClass);
            return true;
        }
        return false;
    }
}
