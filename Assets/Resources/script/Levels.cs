using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    //棋子位置对象
    private GameObject[] check;
    private int[] isCheck;
    //棋盘对象
    private GameObject checkerboard;
    //水滴prefab对象
    public GameObject[] preWater;
    private GameObject waterPar;

    private GameObject leveltext;
    public int levelNum;

    private int[] randnum;
    private int a0, a1, a2, a3, a4;
    private int mode;

    private int checkCount;
    // Use this for initialization
    private void Awake()
    {
        levelNum = 1;
        a0 = a1 = a2 = a3 = a4 = 0;
        checkCount=gameObject.transform.childCount;
}
    void Start()
    {
        waterPar = GameObject.Find("water");
        checkerboard = GameObject.Find("checkerboard");
        leveltext = GameObject.Find("level");
        leveltext.SetActive(false);
        //获取孩子节点
        getAllChild();
        //mode 初始化棋子位置对象
        // mode();
        waitCreateCheck();
    }
    public void setbestScore(int max) {
        if (checkCount == 16)
        {
                PlayerPrefs.SetInt("mod1", max);
        }
        else if (checkCount == 25)
        {
                PlayerPrefs.SetInt("mod2", max);

        }
        else if (checkCount == 36)
        {
                PlayerPrefs.SetInt("mod3", max);
        }
    }
    public int getBestScore()
    {
        int a = 0;
        if (checkCount == 16)
        {
            a = PlayerPrefs.GetInt("mod1", 0);
        }
        else if (checkCount == 25)
        {
            a = PlayerPrefs.GetInt("mod2", 0);
        }
        else if (checkCount == 36)
        {
            a = PlayerPrefs.GetInt("mod3", 0);
        }
        return a;
    }
    //生成水滴
    public void waitCreateCheck()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        var obj = waterPar.GetComponent<Water>();
        mode = gameObject.transform.childCount;
        randnum = new int[mode];
        a0 = a1 = a2 = a3 = a4 = 0;
        setCheckRand();
      
        leveltext.SetActive(true);
        leveltext.GetComponent<Text>().text = "第" + " " + levelNum + " " + "关";
        Invoke("createCheck", 2.0f);
        levelNum++;
    }
    private void createCheck()
    {
        var obj = waterPar.GetComponent<Water>();
        if (obj.step_number <= 0)
        {
            obj.overpan();
        }
        leveltext.SetActive(false);
        System.Random ranNum = new System.Random(System.Guid.NewGuid().GetHashCode());
        for (int i = 0; i < randnum.Length; i++)
        {
            if (randnum[i] != 0)
            {
                GameObject newObj = Instantiate(preWater[(randnum[i] - 1)], check[i].transform.position, check[i].transform.rotation);
                newObj.transform.parent = waterPar.transform;
            }
        }

        obj.max_score = getBestScore();
        obj.maxscore[0].text = "Best:" + obj.max_score.ToString();
        obj.maxscore[1].text = "Best:" + obj.max_score.ToString();
    }
    private void getAllChild()
    {
        check = new GameObject[gameObject.transform.childCount];
        int i = 0;
        foreach (Transform child in transform)
        {
            check[i] = child.gameObject;
            i++;
        }
    }
    // Update is called once per frame
    private bool setCheckRand()
    {
        int k;
        for (int i = 0; i < randnum.Length; i++)
        {
            System.Random ranNum = new System.Random(System.Guid.NewGuid().GetHashCode());
            k = ranNum.Next(5);
            switch (k)
            {
                case 0:
                    a0 += 1;
                    break;
                case 1:
                    a1 += 1;
                    break;
                case 2:
                    a2 += 1;
                    break;
                case 3:
                    a3 += 1;
                    break;
                default:
                    a4 += 1;
                    break;
            }
            randnum[i] = k;

        }
        if (randnum.Length == 16)
        {
            if ((a1 > mode / 3) && (a2 > mode / 4))
            {
                return setCheckRand();
            }
            else
            {
                return true;
            }
        }
        else if (randnum.Length == 25)
        {
            if ((a1 > mode / 2)&& (a4 > mode / 3))
            {
                return setCheckRand();
            }
            else
            {
                return true;
            }

        }
        else 
        {
            if ((a1 > mode / 2) && (a2 > mode / 2))
            {
                return setCheckRand();
            }
            else
            {
                return true;
            }
        }
        
    }
}
