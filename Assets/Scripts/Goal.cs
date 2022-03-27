using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            gameManager.lastCompletedlevel = SceneManager.GetActiveScene().name;
            string curLevel = SceneManager.GetActiveScene().name;
            string[] tmp = curLevel.Split(' ');
            int nextLevel = int.Parse(tmp[1])+1;
            string SceneToLoad = "Level "+ (nextLevel);           
            SceneManager.LoadScene(SceneToLoad);
            

        }
    } 
}
 