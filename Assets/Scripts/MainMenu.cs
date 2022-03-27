using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectMenu;
    [SerializeField] private GameObject ControlsMenu;
    [SerializeField] private Button StartBtn,ControlsBtn, LevelSelectBtn, QuitBtn;
    private bool LevelSelectMenuIsActive =true;
    private bool ControlsMenuIsActive =true;
    private Button lastClickedButton;
    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("Level 1");
    }
    public void LevelSelect()
    {
        lastClickedButton = LevelSelectBtn;
        LevelSelectMenu.SetActive(LevelSelectMenuIsActive);
        StartBtn.interactable = !LevelSelectMenuIsActive;
        ControlsBtn.interactable = !LevelSelectMenuIsActive;
        LevelSelectBtn.interactable = !LevelSelectMenuIsActive;
        QuitBtn.interactable= !LevelSelectMenuIsActive;
        LevelSelectMenuIsActive = !LevelSelectMenuIsActive;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Controls()
    {
        lastClickedButton = ControlsBtn;
        ControlsMenu.SetActive(ControlsMenuIsActive);
        StartBtn.interactable = !ControlsMenuIsActive;
        ControlsBtn.interactable = !ControlsMenuIsActive;
        LevelSelectBtn.interactable= !ControlsMenuIsActive;
        QuitBtn.interactable= !ControlsMenuIsActive;
        ControlsMenuIsActive = !ControlsMenuIsActive;
    }
    public void OnEscape(InputAction.CallbackContext cntx)
    {

        if (cntx.started)
        {
            if(LevelSelectMenu.activeSelf == true)
                LevelSelect();
            if(ControlsMenu.activeSelf == true)
                Controls();
            lastClickedButton.Select();
        }
       
        
    }
}
