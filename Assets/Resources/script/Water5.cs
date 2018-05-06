using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water5 : MonoBehaviour
{

    // Use this for initialization
    private float speed;
    private Rigidbody2D rb;
    private Water waterObj;
    private GameObject minWater;
    private void Awake()
    {
        speed = Random();
    }
    void Start()
    {
      
        rb = gameObject.GetComponent<Rigidbody2D>();
        waterObj = GameObject.Find("water").GetComponent<Water>();
        minWater = GameObject.Find("minWater");
        Invoke("removegame",1.5f);
    }
    private void  removegame()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
    private float Random()
    {
        //生成水滴
        System.Random ranNum = new System.Random(System.Guid.NewGuid().GetHashCode());
        int x = ranNum.Next(10);

        int[] j = new int[10];
        for (int i = 0; i < 10; i++)
        {
            j[i] = ranNum.Next(333333, 3333333);
        }
        return ((float)j[x] / 10000000);
    }
    void Update()
    {
        switch (gameObject.tag)
        {
            case "left": rb.AddForce(new Vector3(-speed * Time.deltaTime, 0, 0)); break;
            case "right": rb.AddForce(new Vector3(speed * Time.deltaTime, 0, 0)); break;
            case "up": rb.AddForce(new Vector3(0, speed * Time.deltaTime, 0)); break;
            case "down": rb.AddForce(new Vector3(0, -speed * Time.deltaTime, 0)); break;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log()
        GameObject obj = collision.gameObject;
        if (obj.tag == "wall")
        {
            Destroy(gameObject);
        }
        if (obj.transform.parent != null)
        {
            if (obj.transform.parent.gameObject.tag == "water")
            {
                if (obj.tag == "water1")
                {
                    waterObj.CreateMinPrefabObj(obj);
                }
                else
                {
                    waterObj.CreatePrefabObj(obj);
                }
                Destroy(gameObject);
                //setStepNumber((--step_number));
            }
        }
    }



}
