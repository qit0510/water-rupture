using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.SceneManagement;
public class Water : MonoBehaviour
{
    // Use this for initialization
    //记录步数分数
    private int now_score;
    public int max_score;
    public int step_number;
    private int double_hit;
    private Text[] nowscore;
    public Text[] maxscore;
    private Text stepnumber;
    //记步数
    private int countStepNum;
    //棋子
    public GameObject[] preWater;
    //子弹
    public GameObject[] minwater;
    private GameObject minwater_par;
    private int isBullets;
    // private Animator animator;
    //
    private Levels level;
    private GameObject stopPan;
    private GameObject overPan;
    private GameObject checkerboard; 
 
   
    private void Awake()
    {
        checkerboard = GameObject.Find("checkerboard");
       
        //获取Levels脚本
        level = checkerboard.GetComponent<Levels>();
        step_number = 10;
        countStepNum = 0;
        now_score = 0;
      
        //PlayerPrefs.GetInt();
        nowscore = new Text[2];
        maxscore = new Text[2];
    }
    void Start()
    {
        minwater_par = GameObject.Find("bullets");
        //文本/按钮 初始化
        nowscore[0] = GameObject.Find("now_score").GetComponent<Text>();
        nowscore[1] = GameObject.Find("nscore").GetComponent<Text>();
        maxscore[0] = GameObject.Find("best_score").GetComponent<Text>();
        maxscore[1] = GameObject.Find("bscore").GetComponent<Text>();
        stepnumber = GameObject.Find("step_number").GetComponent<Text>();
        setStepNumber(10);
  
        //面板 退出|结束
        stopPan = GameObject.Find("exitpan");
        stopPan.SetActive(false);
        overPan = GameObject.Find("over");
        overPan.SetActive(false);
    }

    private void Update()
    {
        isBullets = minwater_par.transform.childCount;
       
        if (step_number > 0 && isBullets==0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // index = 0;
                //重置步数
                countStepNum = 0;
                //射线
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject obj = hit.collider.gameObject;
                    if (obj.transform.parent != null)
                    {
                        if (obj.transform.parent.gameObject.tag == "water")
                        {
                            if (obj.tag == "water1")
                            {
                                double_hit = 0;
                                CreateMinPrefabObj(obj);
                            }
                            else
                            {
                                CreatePrefabObj(obj);
                            }
                            //设置步数
                            setStepNumber((--step_number));
                        }
                    }
                }//if (hit.collider != null)
            }//Input.GetMouseButtonDown(0)
        }

    }
  
    public void CreateMinPrefabObj(GameObject obj)
    {
        Transform tr = obj.transform;
        Destroy(obj);
        ++double_hit;
        //setScore(double_hit);
        for (int i = 0; i < 4; i++)
        {
            GameObject minobj = Instantiate(minwater[i], tr.position, tr.rotation);
            minobj.transform.parent = minwater_par.transform;
        }
        if (gameObject.transform.childCount <= 1 && step_number > 0)
        {
            resetPrefabObj();
        }
        setScore();

    }
    public void CreatePrefabObj(GameObject obj)
    {
        Transform tr = obj.transform;
        int index = int.Parse(obj.tag.Substring(obj.tag.Length - 1, 1));
        Destroy(obj);
        index--;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(tr.position.x, tr.position.x), Vector2.zero);
        GameObject newObj = Instantiate(preWater[index - 1], tr.position, tr.rotation);
        newObj.transform.parent = gameObject.transform;
    }
    private void resetPrefabObj()
    {
            level.waitCreateCheck();
    }
    //分数
    public void setScore()
    {
        switch (double_hit)
        {
            case 3: step_number += 1; setStepNumber(step_number);  break;
            case 6: step_number +=2; setStepNumber(step_number) ; break;
            case 9: step_number += 3; setStepNumber(step_number); break;
            case 12: step_number += 4; setStepNumber(step_number); break;
        }
        if (double_hit < 3)
        {
            now_score += 1;
        }
        else if (double_hit >= 3 && double_hit < 6)
        {
            now_score += 2;
        }
        else if (double_hit >= 6 )
        {
            now_score += 3;
        }
        nowscore[0].text = "score:" + now_score.ToString();
        nowscore[1].text = "score:" + now_score.ToString();
        if (now_score> max_score)
        {
            max_score = now_score;
            level.setbestScore(max_score);
            maxscore[0].text = "Best:" + max_score.ToString();
            maxscore[1].text = "Best:" + max_score.ToString();
        }       
    }
    //步数
    public void setStepNumber(int step)
    {
        stepnumber.text = "step:"+step.ToString();
        
        var iswater = gameObject.transform.childCount;
        if (step_number == 0 && iswater > 0)
        {
            Invoke("overpan", 1.5f);
        }
    }
    //button 设置
    //结束面板
    public void overpan()
    {
        overPan.SetActive(true);
        Time.timeScale = 0;
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
    //重新开始按钮
    public void resetBtn()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.transform.gameObject);
        }
        step_number = 10;
        setStepNumber(step_number);
        overPan.SetActive(false);
        Time.timeScale = 1;
        now_score = 0;
        setScore();
        level.levelNum = 1;
        resetPrefabObj();
    }
    //暂停开始按钮
    public void stopBtn()
    {
        Time.timeScale = 0;
        stopPan.SetActive(true);
        gameObject.SetActive(false);
    }
    //恢复开始按钮
    public void PlayBtn()
    {
        Time.timeScale = 1;
        gameObject.SetActive(true);
        stopPan.SetActive(false);
    }
    //退出开始按钮
    public void exitBtn()
    {
        Application.Quit();
    }


}

