using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour {

    public string playerName;//在面板中改为敌方的 物体名字 以避免自我伤害
    public string myself;
    void Update()
    {
        Invoke("DestroyBullet", 6.0f);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.name == playerName)
        {
            collision2D.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(this.gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.name == myself)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.name == myself)
        {
            this.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
}
