using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {


    public void mode1()
    {
        SceneManager.LoadScene(1);
    }
    public void mode2()
    {
        SceneManager.LoadScene(2);
    }
    public void mode3()
    {
        SceneManager.LoadScene(3);
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
    public void about()
    {
        SceneManager.LoadScene(4);
    }
    public void help()
    {
        SceneManager.LoadScene(5);
    }
    public void aboutbenren()
    {
        SceneManager.LoadScene(6);
    }
    public void fabout()
    {
        SceneManager.LoadScene(4);
    }
    public void zz()
    {
        SceneManager.LoadScene(7);
    }
    public void exit()
    {
        Application.Quit();
    }
}
