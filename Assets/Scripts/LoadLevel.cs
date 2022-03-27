using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    public GameManager gameManager;
    public void _LoadLevel()
    {        
        string levelToLoad = this.gameObject.name;
        Cursor.visible = false;
        SceneManager.LoadScene(levelToLoad);
    }
}
