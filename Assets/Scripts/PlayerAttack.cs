using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject commonBullet;//初始子弹
    public KeyCode key;//攻击键
    public GameObject bounceBullet;//弹弹弹
    public GameObject ThunderBullet;//雷电弹
    public GameObject SplitBullet;//分裂弹

    private AudioClip shootAudio;//发射子弹的声音
    private AudioClip damageAudio;//收到伤害的声音
    private AudioClip impactAudio;//撞墙的声音

    private Rigidbody2D rigidbody2D;//角色刚体组件
    //获取子弹发射方向
    private Transform shootPoint1;
    private Transform shootPoint2;
    private Vector3 offset;
    //控制角色血量的脚本
    private PlayerHealth playerHealth;
    private int model = 1;//射击模式
    public string bulletString;//作“场景中只有一颗子弹”辅助用
    private int thunderNumber = 0;//雷电弹的计数器

    private GameObject game;//上个场景未被销毁的物体

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        shootPoint1 = transform.Find("shootPoint1");
        shootPoint2 = transform.Find("shootPoint2");
        playerHealth = GetComponent<PlayerHealth>();
        shootAudio = Resources.Load("Shoot") as AudioClip;
        damageAudio = Resources.Load("UnderAttack") as AudioClip;
        impactAudio = Resources.Load("Impact") as AudioClip;
        game = GameObject.Find("GameObject");//寻找上一个场景未被销毁的空物体
    }

    void Start()
    {
        if(game.GetComponent<UIControl>().gameModel == 1 && this.transform.name == "Player2")
        {
            InvokeRepeating("ComputerControlEasy", 1.0f, 1.5f);
        }
    }

    void Update()
    {
        /*if(this.transform.name == "Player1")
        {
            PersonControl();
        }*/

        if(game.GetComponent<UIControl>().gameModel == 3 && this.transform.name == "Player2")
        {
            ComputerControlDifficult();
        }
        else
        {
            PersonControl();
        }


        /*switch (game.GetComponent<UIControl>().gameModel)
        {
            case 3:
                ComputerControlDifficult();
                break;
            case 2:
                PersonControl();
                break;
        }*/
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.name.Contains("Bullet"))
            AudioSource.PlayClipAtPoint(damageAudio, transform.position);
        if (collision2D.gameObject.tag == "Edge")
            AudioSource.PlayClipAtPoint(impactAudio, transform.position);

    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        //进入蛛网减速
        if(collider2D.tag == "Slow")
        {
            rigidbody2D.drag *= 50f;
            rigidbody2D.angularDrag *= 50f;
        }
        switch (collider2D.name)
        {
            case "Cure(Clone)":
                //Debug.Log(1);
                playerHealth.Cure();
                Destroy(collider2D.gameObject);
                break;
            case "Bounce(Clone)":
                model = 2;
                Destroy(collider2D.gameObject);
                break;
            case "Split(Clone)":
                model = 3;
                Destroy(collider2D.gameObject);
                break;
            case "Thunder(Clone)":
                model = 4;
                thunderNumber = 0;
                Destroy(collider2D.gameObject);
                break;

        }
    }

    //退出蛛网减速解除
    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Slow")
        {
            rigidbody2D.drag /= 50f;
            rigidbody2D.angularDrag /= 50f;
        }
    }

    //初始射击方式
    void CommonShoot()
    {
        offset = shootPoint2.position - shootPoint1.position;
        //子弹旋转180°
        /*float angle = Vector3.Angle(offset, Vector3.right);
        float dot = Vector3.Dot(offset, Vector3.up);
        if (dot < 0) angle = 360 - angle;
        shootPoint1.rotation = Quaternion.Euler(0, 0, angle - 180);*/
        //限定场景中只有一颗子弹
        GameObject nowBullet = GameObject.FindGameObjectWithTag(bulletString);
        if (nowBullet != null)
        {
            Destroy(nowBullet.gameObject);
        }
        //发射子弹
        GameObject go = GameObject.Instantiate(commonBullet, shootPoint1.position, shootPoint1.rotation);
        go.GetComponent<Transform>().right = -1 * go.GetComponent<Transform>().right;
        //Debug.Log(shootPoint2.rotation);
        go.GetComponent<Rigidbody2D>().velocity = offset * 5f;
    }

    //跳弹射击方式
    void BounceShoot()
    {
        offset = shootPoint2.position - shootPoint1.position;
        //发射子弹
        GameObject go = GameObject.Instantiate(bounceBullet, shootPoint1.position, shootPoint1.rotation);
        go.GetComponent<Rigidbody2D>().velocity = offset * 5f;
        model = 1;
    }

    //雷电射击方式
    void ThunderShoot()
    {
        offset = shootPoint2.position - shootPoint1.position;
        LineRenderer thunder = GetComponentInChildren<LineRenderer>();

        RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint1.position, offset);
        if (hitInfo == false)
        {
            return;
        }
        thunder.enabled = true;
        thunder.SetPosition(0, shootPoint1.position);
        thunder.SetPosition(1, hitInfo.point);
        Invoke("ClearThunder", 0.05f);
        if (hitInfo.transform.tag == "Player")
        {
            hitInfo.transform.GetComponent<PlayerHealth>().TakeDamage();
        }

        /*//限定场景中只有一颗子弹
        GameObject nowBullet = GameObject.FindGameObjectWithTag(bulletString);
        if (nowBullet != null)
        {
            Destroy(nowBullet.gameObject);
        }
        //发射子弹
        GameObject go = GameObject.Instantiate(ThunderBullet, shootPoint2.position, shootPoint2.rotation);//之所以用shootPoint2是因为雷电本身有长度*/
    }

    //清除闪电
    void ClearThunder()
    {
        LineRenderer thunder = GetComponentInChildren<LineRenderer>();
        thunder.enabled = false;
    }

    //分裂球射击
    void SplitShoot()
    {
        offset = shootPoint2.position - shootPoint1.position;
        //发射子弹
        GameObject go = GameObject.Instantiate(SplitBullet, shootPoint1.position, shootPoint1.rotation);
        go.GetComponent<Rigidbody2D>().velocity = offset * 5f;
        model = 1;
    }

    //玩家控制
    void PersonControl()
    {
        if (Input.GetKeyDown(key))
        {
            AudioSource.PlayClipAtPoint(shootAudio, transform.position);
            switch (model)
            {
                case 1://初始射击
                    CommonShoot();
                    break;
                case 2://弹弹弹射击
                    BounceShoot();
                    break;
                case 3://分裂弹射击
                    SplitShoot();
                    break;
                case 4://雷电弹射击
                    ThunderShoot();
                    thunderNumber += 1;
                    break;
            }
            if (thunderNumber == 5)
                model = 1;
            //后退
            rigidbody2D.velocity = -offset * 2f;
            //旋转
            rigidbody2D.angularVelocity = -300f;
        }
    }

    //电脑控制
    void ComputerControlDifficult()
    {
        offset = shootPoint2.position - shootPoint1.position;
        /*if (rigidbody2D.velocity.x <= 0.05f || rigidbody2D.velocity.y <= 0.05f)
        {
            //后退
            rigidbody2D.velocity = -offset * 2f;
            //旋转
            rigidbody2D.angularVelocity = -300f;
        }*/
        RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint1.position, offset);
        if (hitInfo == false)
        {
            return;
        }
        else if(hitInfo.transform.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(shootAudio, transform.position);
            switch (model)
            {
                case 1://初始射击
                    CommonShoot();
                    break;
                case 2://弹弹弹射击
                    BounceShoot();
                    break;
                case 3://分裂弹射击
                    SplitShoot();
                    break;
                case 4://雷电弹射击
                    ThunderShoot();
                    thunderNumber += 1;
                    break;
            }
            if (thunderNumber == 5)
                model = 1;
            //后退
            rigidbody2D.velocity = -offset * 2f;
            //旋转
            rigidbody2D.angularVelocity = -300f;
        }
    }

    void ComputerControlEasy()
    {
        AudioSource.PlayClipAtPoint(shootAudio, transform.position);
        switch (model)
        {
            case 1://初始射击
                CommonShoot();
                break;
            case 2://弹弹弹射击
                BounceShoot();
                break;
            case 3://分裂弹射击
                SplitShoot();
                break;
            case 4://雷电弹射击
                ThunderShoot();
                thunderNumber += 1;
                break;
        }
        if (thunderNumber == 5)
            model = 1;
        //后退
        rigidbody2D.velocity = -offset * 2f;
        //旋转
        rigidbody2D.angularVelocity = -300f;
    }
}