using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TermsCheccker : MonoBehaviour
{
    void Awake()
    {
        CheckForTerms();
    }
    void CheckForTerms()
    {
        if (Directory.Exists(Application.persistentDataPath + "/TerminosCheck"))
            FindObjectOfType<AppManager>().LoadThisScene("MainMenu");
    }
    public void AceptarTerminos()
    {
        Directory.CreateDirectory(Application.persistentDataPath + "/TerminosCheck");
        FindObjectOfType<AppManager>().LoadThisScene("MainMenu");
    }
    public void SalirDeApp()
    {
        FindObjectOfType<AppManager>().QuitarLaApp();
    }
}
