using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IMenu : MonoBehaviour
{
    public Button quitButton;
    public Button restartButton;
    public TextMeshProUGUI title;
    // options button

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(QuitTaskOnClick);
        restartButton.onClick.AddListener(RestartTaskOnClick);
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    void QuitTaskOnClick()
    {
        //quit
    }

    void RestartTaskOnClick()
    {
        restartButton.image.color = Color.magenta;
    }
}
