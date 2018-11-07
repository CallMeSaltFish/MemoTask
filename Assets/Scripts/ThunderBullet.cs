using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBullet : MonoBehaviour {



	void Update () {
        Invoke("DestroyBullet", 0.2f);
	}

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "Player")
        {
            collision2D.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
        Destroy(this.gameObject);
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
