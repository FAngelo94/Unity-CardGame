using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (Input.touchCount>0 && collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
        {
            Debug.Log("Sprite pressed, phase="+ Input.GetTouch(0).phase);
        }

        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                //Debug.Log("Began");
                //ChangeColorSprite(Color.black);
            }
            if (touch.phase == TouchPhase.Canceled)
            {
                //Debug.Log("Canceled");
                //ChangeColorSprite(Color.blue);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //Debug.Log("Ended");
                //ChangeColorSprite(Color.red);
            }
            if (touch.phase == TouchPhase.Moved)
            {
                //Debug.Log("Moved");
                //ChangeColorSprite(Color.yellow);
            }
            if (touch.phase == TouchPhase.Stationary)
            {
                //Debug.Log("Stationary");
                //ChangeColorSprite(Color.green);
            }
           
        }
	}

    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        ChangeColorSprite(Color.black);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        ChangeColorSprite(Color.red);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        ChangeColorSprite(Color.yellow);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        ChangeColorSprite(Color.blue);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit");
    }
    

    private void StartMove()
    {

    }

    private void ChangeColorSprite(Color color)
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = color;
    }
}
