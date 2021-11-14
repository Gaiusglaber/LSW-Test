using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static event Action OnEnterScene = null;
    public static GameManager Get()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    public IEnumerator ChangeScene(string tag)
    {
        
        if (tag == "Hall")
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Hall");
            

        }
        else if (tag == "Shop")
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Shop");
            
        }
        else if (tag == "World")
        {
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Outside");
            
        }
    }
}