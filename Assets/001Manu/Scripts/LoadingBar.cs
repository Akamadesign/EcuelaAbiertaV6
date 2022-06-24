using UnityEngine;
using UnityEngine.UI;
public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    AppManager manager;
    void Update()
    {
        if (manager == null)
            FindObjectOfType<AppManager>();
        if (!slider)
            GetComponent<Slider>();
    }
}
