using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    private GameObject menu1;
    private GameObject menu2;
    private GameObject menu3;
    private GameObject volume;
    private AudioSource bgm;
    private Slider volumeSlider;
    private Toggle isSilence;

    private float std;//辅助调音量
    private GameObject bgmPrefab;
    private GameObject[] gameControls;
    private GameObject gameControl;
    //private GameObject[] list;

    [HideInInspector]
    public int gameModel;//2为双人模式，1为单人简单模式，3为单人地狱模式
    void Awake()
    {
        //保证全场景只有一个音乐
        bgmPrefab = GameObject.Find("Bgm(Clone)");
        if (bgmPrefab == null)
        {
            Instantiate(Resources.Load("Bgm"), transform.position, Quaternion.identity);
        }
        //保证该物体只有最新的一个（每次场景加载就会出现一个），所以要Destroy掉旧的一个，不然游戏模式会一直不变
        gameControls = GameObject.FindGameObjectsWithTag("GameController");
        if(gameControls.Length == 2)
        {
            gameControl = GameObject.Find("GameObject");
            Destroy(gameControl);
        }
        menu1 = GameObject.Find("Menu1");
        menu2 = GameObject.Find("Menu2");
        menu3 = GameObject.Find("Menu3");
        volume = GameObject.Find("Volume");
        bgm = GameObject.Find("Bgm(Clone)").GetComponent<AudioSource>();
        volumeSlider = GameObject.Find("Volume").GetComponent<Slider>();
        isSilence = GameObject.Find("Volume/IsSilence").GetComponent<Toggle>();
        std = bgm.volume;

        //if (isSilence != null)
        //    Debug.Log(1);
    }

    // Use this for initialization
    void Start () {
        menu2.SetActive(false);
        volume.SetActive(false);
        menu3.SetActive(false);

        /*list = (GameObject.FindGameObjectsWithTag("GameController"));
        if (list.Length == 1)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Debug.Log(list.Length);*/

    }

    //模式选择菜单
    public void Menu2()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    //单人难度选择
    public void Menu3()
    {
        menu2.SetActive(false);
        menu3.SetActive(true);
    }
	
    //进入设置菜单
    public void Setting()
    {
        menu1.SetActive(false);
        volume.SetActive(true);
    }

    //返回最开始菜单
    public void ReturnToMenu1()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
        volume.SetActive(false);
    }

    //返回模式选择菜单
    public void ReturnToMenu2()
    {
        menu3.SetActive(false);
        menu2.SetActive(true);
    }

    //退出游戏
    public void Exit()
    {
        Application.Quit();
    }

    //控制音量
    public void ControlVolume()
    {
        bgm.volume = volumeSlider.value;
        if (bgm.volume != 0)
        {
            std = bgm.volume;
        }
        if (bgm.volume == 0)
        {
            isSilence.isOn = true;
        }
        else
        {
            isSilence.isOn = false;
        }
    }

    //控制静音
    public void IsSilence(bool isOn)
    {
        if (isOn)
        {
            bgm.volume = 0f;
            volumeSlider.value = 0f;
        }
        else
        {
            bgm.volume = std;
            volumeSlider.value = std;
        }
    }

    //双人模式
    public void Double()
    {
        gameModel = 2;
        SceneManager.LoadScene(1);
    }

    //单人简单模式
    public void SingleEasy()
    {
        gameModel = 1;
        SceneManager.LoadScene(1);
    }

    //单人困难模式
    public void SingleDifficult()
    {
        gameModel = 3;
        SceneManager.LoadScene(1);
    }
}
