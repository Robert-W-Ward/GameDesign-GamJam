using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public string curlevel;
    public int levelidx;
    public string lastCompletedlevel;
    public string nextLevel;
    public string levelToLoad;
    private static GameManager instance;
    private GameObject player;
    void Awake()
    {
        SceneManager.sceneLoaded += OnLoadScene;
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        if (instance == this) return;
        Destroy(gameObject);
    }
    void OnLoadScene(Scene scene, LoadSceneMode Single)
    {
        
        curlevel = SceneManager.GetActiveScene().name;
        if (Cursor.visible == true&&curlevel!="Main Menu")
            Cursor.visible = false;

    }
   
}
