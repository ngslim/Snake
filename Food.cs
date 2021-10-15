using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    float height;
    float width;
    void Start()
    {
        width = Mathf.Abs(GameObject.Find("Wall_Right").transform.position.x - GameObject.Find("Wall_Left").transform.position.x) - 2;
        height = Mathf.Abs(GameObject.Find("Wall_Top").transform.position.y - GameObject.Find("Wall_Bottom").transform.position.y) - 2;
        Debug.Log(height);
        Debug.Log(width);
    }

    void RandomizePosition()
    {
        float x = Random.Range(-1 * width / 2, width / 2);
        float y = Random.Range(-1 * height / 2, height / 2);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }



    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
