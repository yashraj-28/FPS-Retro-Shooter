using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject escapeScreen;
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        //escapeScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
            
        }
        if(Input.GetMouseButton(0))
        {
            LockCursor();
        }
    }
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
         
    }
    // public void EscapeScreen()
    // {
    //     UnlockCursor();
    //     escapeScreen.SetActive(true); 

    // }
   
}
