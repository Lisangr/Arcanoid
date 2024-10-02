using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class StartGame : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject helpPanel;
    public GameObject list2;
    private void Awake()
    {
        settingsPanel.SetActive(false);
        helpPanel.SetActive(false);
    }
    public void OnPanelClosePanel()
    {
        settingsPanel.SetActive(false);      
        list2.SetActive(false);
    }
    public void OnClickStart(string scene)
    {
        //YandexGame.FullscreenShow();
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    public void OnPanelClick()
    {
        settingsPanel.SetActive(true);
    }

    public void OnHelpPanelClick()
    {
        list2.SetActive(false);
        helpPanel.SetActive(true);
    }
    public void OnHelpPanelClosePanel()
    {
        list2.SetActive(false);
        helpPanel.SetActive(false);
    }
}

