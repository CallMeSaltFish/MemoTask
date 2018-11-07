using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private PlayerHealth player1;
    private PlayerHealth player2;
    private Text winText;
    private bool isPause = false;//是否暂停
    private PlayerAttack playerAttack1;
    private PlayerAttack playerAttack2;
    private Text timeText;
    //墙
    private Transform left;
    private Transform right;
    private Transform up;
    private Transform down;
    //游戏时长
    private int time = 60;
    private float intervalTime = 1; 


    private GameObject onPauseMenu;
    private GameObject overMenu;
    private GameObject pause;
    //private Transform transform;

    //private Animator animatorPause;
    //private Animator animatorPauseMenu;
    void Awake()
    {
        Time.timeScale = 1;
        player1 = GameObject.Find("Player1").GetComponent<PlayerHealth>();
        player2 = GameObject.Find("Player2").GetComponent<PlayerHealth>();
        winText = GameObject.Find("WinText").GetComponent<Text>();
        playerAttack1 = GameObject.Find("Player1").GetComponent<PlayerAttack>();
        playerAttack2 = GameObject.Find("Player2").GetComponent<PlayerAttack>();
        timeText = GameObject.Find("TimeText").GetComponent<Text>();
        left = GameObject.Find("LVertical").GetComponent<Transform>();
        right = GameObject.Find("RVertical").GetComponent<Transform>();
        up = GameObject.Find("UHorizontal").GetComponent<Transform>();
        down = GameObject.Find("DHorizontal").GetComponent<Transform>();

        onPauseMenu = GameObject.Find("OnPauseMenu");
        overMenu = GameObject.Find("OverMenu");
        pause = GameObject.Find("Pause");
        //transform = onPauseMenu.GetComponent<Transform>();

        //animatorPause = GameObject.Find("Pause").GetComponent<Animator>();
        //animatorPauseMenu = GameObject.Find("OnPauseMenu").GetComponent<Animator>();
    }
    void Start()
    {
        onPauseMenu.SetActive(false);
        overMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        //是否暂停
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause == false)
            {
                Pause();
            }
            else
            {
                Continue();
            }
        }

        //计时器
        if (time > 0)
        {
            //计时
            intervalTime -= Time.deltaTime;
            if (intervalTime <= 0)
            {
                intervalTime = 1;
                time--;
                timeText.text = time + "s";
            }
            //边界移动1
            if (time <= 35)
            {
                left.position = Vector3.Lerp(left.position, new Vector3(-3.8f, left.position.y, 0), 0.8f * Time.deltaTime);
                right.position = Vector3.Lerp(right.position, new Vector3(3.8f, right.position.y, 0), 0.8f * Time.deltaTime);
            }
            //边界移动2
            if (time <= 15)
            {
                up.position = Vector3.Lerp(up.position, new Vector3(up.position.x, 2.0f, 0), 0.8f * Time.deltaTime);
                down.position = Vector3.Lerp(down.position, new Vector3(down.position.x, -2.0f, 0), 0.8f * Time.deltaTime);
            }
        }

        //判定胜负
        if(player1.hp == 0|| player2.hp == 0|| time <= 0)
        {
            Stop();
            if (player1.hp > player2.hp)
            {
                winText.enabled = true;
                winText.text = "Blue Win!";
                winText.color = Color.blue;
            }else if (player1.hp < player2.hp)
            {
                winText.enabled = true;
                winText.text = "Red Win!";
                winText.color = Color.red;
            }
            else
            {
                winText.enabled = true;
                winText.text = "Time is up , Draw!";
                winText.color = Color.green;
            }
        }


        /*if (player1.hp == 0)
        {
            winText.enabled = true;
            winText.text = "Red Win!";
            winText.color = Color.red;
        }
        else if(player2.hp == 0)
        {
            winText.enabled = true;
            winText.text = "Blue Win!";
            winText.color = Color.blue;
        }else if (time <= 0)
        {
            if (player1.hp > player2.hp)
            {

            }
        }*/


    }

    //游戏暂停
    public void Pause()
    {
        onPauseMenu.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 0;
        //StartCoroutine(ToPause());

        playerAttack1.enabled = false;
        playerAttack2.enabled = false;
        isPause = true;
        //transform.localPosition = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1f, 0), 1 * Time.deltaTime);
        //onPauseMenu.SetActive(true);
    }

    //回到游戏
    public void Continue()
    {
        onPauseMenu.SetActive(false);
        pause.SetActive(true);
        Time.timeScale = 1;
        playerAttack1.enabled = true;
        playerAttack2.enabled = true;
        isPause = false;

        //onPauseMenu.SetActive(false);
    }

    //重新开始
    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    /*private IEnumerator ToPause()
    {
        yield return new WaitForSeconds(0);
        animatorPause.SetTrigger("Up");
        animatorPauseMenu.SetTrigger("Down");
        Debug.Log(1);
    }*/


    //返回主菜单
    public void Return()
    {
        SceneManager.LoadScene(0);
    }

    //游戏结束
    private void Stop()
    {
        overMenu.SetActive(true);
        pause.SetActive(false);
        Time.timeScale = 0;
        playerAttack1.enabled = false;
        playerAttack2.enabled = false;
    }
}
