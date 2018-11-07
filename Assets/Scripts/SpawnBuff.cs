using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuff : MonoBehaviour {

    private List<GameObject> buff = new List<GameObject>();
    private static int lastBuff = -1; //用来防止相邻两次随机产生的物体相同

    void Start()
    {
        buff.Add(Resources.Load("Cure") as GameObject);
        buff.Add(Resources.Load("Thunder") as GameObject);
        buff.Add(Resources.Load("Split") as GameObject);
        buff.Add(Resources.Load("Bounce") as GameObject);
        InvokeRepeating("Spawn_Buff", 3.0f, 6.0f);
    }

    void Spawn_Buff()
    {
        GameObject nowbuff = GameObject.FindGameObjectWithTag("Buff");
        if (nowbuff != null)
        {
            Destroy(nowbuff.gameObject);
        }
        int std;
        do
        {
            std = Random.Range(0, buff.Count);
        } while (std == lastBuff);
        lastBuff = std;
        Instantiate(buff[std], transform.position, Quaternion.identity);
    }

}
