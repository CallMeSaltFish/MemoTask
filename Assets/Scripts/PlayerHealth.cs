using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [HideInInspector]
    public int hp = 3;

    private SpriteRenderer transparent;//调整透明度

    void Awake()
    {
        transparent = GetComponent<SpriteRenderer>();
    }

    //收到伤害
    public void TakeDamage()
    {
        if (hp <= 0) return;
        hp -= 1;
        transparent.color = new Color(1.0f, 1.0f, 1.0f, hp/3f);
    }
    //回血
    public void Cure()
    {
        if (hp >= 3) return;
        hp += 1;
        transparent.color = new Color(1.0f, 1.0f, 1.0f, hp / 3f);
    }

}
