using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : TouchManager
{
    [Header("Foreground position")]
    public float foreground = 0;
    [Header("Who is the layer")]
    public int player = 1;

    public int top;
    public int bottom;
    public int right;
    public int left;

    private Vector3 originPosition;

    private new Collider2D collider;
    private bool selected;

    
    private List<GameObject> slotGameObject;

    // Use this for initialization
    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();
        selected = false;
        slotGameObject = new List<GameObject>();
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouchOnObject(collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("slot"))
        {
            slotGameObject.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
     
        if (collision.gameObject.tag.Equals("slot"))
        {
            slotGameObject.Remove(collision.gameObject);
        }
    }

    protected override void SpritePressedBegan()
    {
        selected = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, foreground);
    }
    protected override void SpritePressedEnded()
    {
        selected = false;
        if (PutCardInSlot())
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, originPosition.z);
        }
        else
        {
            transform.position = new Vector3(originPosition.x, originPosition.y, originPosition.z);
        }
}
    protected override void SpritePressedMoved()
    {
        if (selected == true)
        {
            Vector2 p = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            gameObject.transform.position = new Vector2(p.x,p.y);
        }
    }
    protected override void SpritePressedStationary()
    {

    }

    private bool PutCardInSlot()
    {
        if (slotGameObject.Count > 0)
        {
            CardSlot cardSlot = null;
            float nearest = float.MaxValue;
            foreach (GameObject slot in slotGameObject)
            {
                float tmpDist = Vector2.Distance(slot.transform.position, transform.position);
                if (tmpDist < nearest)
                {
                    nearest = tmpDist;
                    cardSlot = slot.GetComponent<CardSlot>();
                }
            }

            if (cardSlot!=null && cardSlot.PutCard(gameObject,this))
            {
                gameObject.GetComponent<Card>().enabled = false;
            }
            return true;
        }
        return false;
    }
}
