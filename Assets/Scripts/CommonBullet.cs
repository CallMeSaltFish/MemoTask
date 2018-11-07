using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonBullet : MonoBehaviour {

    public string myself;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Debug.Log(collision2D.gameObject.name);
        if(collision2D.gameObject.tag == "Player" && collision2D.gameObject.name != myself)
        {
            //这三句效果一样？
            //collision2D.transform.GetComponent<PlayerHealth>().TakeDamage();
            //collision2D.collider.GetComponent<PlayerHealth>().TakeDamage();
            collision2D.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(this.gameObject);
        }
        if(collision2D.gameObject.tag == "Edge")
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.name == myself)
        {
            this.GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.name == myself)
        {
            this.GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }
}
