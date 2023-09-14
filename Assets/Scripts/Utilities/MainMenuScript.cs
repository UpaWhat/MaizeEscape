using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    

    public void PlayClicked()
    {
        Debug.Log("Play");
        Loader.Load(Loader.Scene.Game);
    }

    public void CreditsClicked()
    {
        Debug.Log("Credits");
        Loader.Load(Loader.Scene.Credits);
    }

    public void QuitClicked()
    {
        Debug.Log("Quit");
    }

}
