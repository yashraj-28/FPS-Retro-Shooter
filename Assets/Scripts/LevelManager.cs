using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject escapeScreenPanel;
    public static LevelManager instance;
    private void Awake() {
        instance = this;
    }
     public void StartGame()
    {
        
        SceneManager.LoadScene("Map1");
        
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    public void ResumeGame()
    {
        escapeScreenPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
