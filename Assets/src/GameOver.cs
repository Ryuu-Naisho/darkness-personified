using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


    public Button GoToMenuButton;    
    // Start is called before the first frame update
    void Start()
    {
        GoToMenuButton.onClick.AddListener(GoToMenu);
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

    }


    ///<summary>Load the menu</summary>
    private void GoToMenu()
    {
        Action loadScene = ()=> SceneManager.LoadScene("Menu");
        StartCoroutine(Wait(2, loadScene));
    }


        private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
