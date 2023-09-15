using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogicController : MonoBehaviour
{

    [Header("References")]
    private GameObject player;
    private GameObject[] Birds;
    private bool gameOver = false;
    [SerializeField] private Image bkgImage;
    [SerializeField] private GameObject goText;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject goBtn;
    [SerializeField] private GameObject retryBtn;




    public void PlayerDead()
    {
        Debug.Log("GAME OVER");
    }

    private void Start()
    {
        
        winText.SetActive(false);
        goText.SetActive(false);
        goBtn.SetActive(false);
        retryBtn.SetActive(false);
        StartCoroutine(FadeToBlack(false));
        Birds = GameObject.FindGameObjectsWithTag("Bird");
        for(int i = 0; i<Birds.Length; i++)
        {
            Birds[i].GetComponent<BirdAIScript>().setActive();
            Debug.Log("FOUND " + i + " BIRD(s)");
        }
    }

    private void Update()
    {
        
    }

    public void PlayerDied()
    {
        if (gameOver) return;
        gameOver = true;
        Debug.Log("Game Over");
        for(int i=0; i<Birds.Length; i++)
        {
            Birds[i].GetComponent<BirdAIScript>().setInactive();
            Debug.Log("Bird Inactive");
            StartCoroutine(FadeToBlack(true,false));
        }
    }

    public void PlayerWin()
    {
        if (gameOver) return;
        gameOver = true;
        Debug.Log("Win");
        for (int i = 0; i < Birds.Length; i++)
        {
            Birds[i].GetComponent<BirdAIScript>().setInactive();
            Debug.Log("Bird Inactive");
            StartCoroutine(FadeToBlack(true, true));
        }
    }

    public void QuitClicked()
    {
        Debug.Log("Quit");
        Loader.Load(Loader.Scene.MainMenu);
    }
    public void RetryClicked()
    {
        Debug.Log("Retry");
        Loader.Load(Loader.Scene.Game);
    }

    public IEnumerator FadeToBlack(bool fadeToBlack = true, bool win = false, int fadeSpeed =5)
    {
        Color objectColor = bkgImage.color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while (bkgImage.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                bkgImage.color = objectColor;
                if(win)
                {
                    winText.SetActive(true);
                }
                else
                {
                    goText.SetActive(true);
                }
                
                goBtn.SetActive(true);
                retryBtn.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                yield return null;
            }
        }
        else
        {
            while (bkgImage.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                bkgImage.color = objectColor;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                yield return null;
            }
        }
    }

    
}
