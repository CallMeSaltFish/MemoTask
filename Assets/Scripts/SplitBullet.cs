using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet : MonoBehaviour {

    private Transform[] splitPoint;
    private Vector3 offset;

    public GameObject littleBullet;
    void Awake()
    {
        splitPoint = GetComponentsInChildren<Transform>();
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Edge")
        {
            Destroy(gameObject);
            //Debug.Log(splitPoint.Length);
            foreach (var split in splitPoint)
            {
                offset = split.position - transform.position;
                if(offset != new Vector3(0.0f, 0.0f, 0.0f))
                {
                    //Debug.Log("0.0");
                    GameObject go = GameObject.Instantiate(littleBullet, split.position, split.rotation);
                    go.GetComponent<Rigidbody2D>().velocity = offset * 40f;
                    //Debug.Log(offset);
                } 
            }
        }
    }
}
