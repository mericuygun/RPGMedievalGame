using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public AudioSource audiosource;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void PlayGame()
    {
        audiosource.Play();
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
        audiosource.Play();
        Application.Quit();
    }
}
