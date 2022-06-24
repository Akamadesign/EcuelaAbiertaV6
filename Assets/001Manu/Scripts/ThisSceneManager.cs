using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisSceneManager : MonoBehaviour
{
    AppManager manager;
    void Start()
    {
        manager = FindObjectOfType<AppManager>();
    }
    public void CargaEstaEscena(string escena)
    {
        manager.LoadThisScene(escena);
    }
    public void SalirDeApp()
    {
        manager.QuitarLaApp();
    }
}
