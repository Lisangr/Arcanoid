using UnityEngine;
public class Panels : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject list2;

    void Start()
    {
        menuPanel.SetActive(false);
        list2.SetActive(false);
    }

    public void OnClickShowMenuPanel()
    {
        list2.SetActive(false);
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void OnClickHideMenuPanel()
    {        
        menuPanel.SetActive(false);
        list2.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void OnClickNextPanel()
    {
        list2.SetActive(true);
    }

}
