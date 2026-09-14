using UnityEngine;
using UnityEngine.SceneManagement;

public class ScannerManager : MonoBehaviour
{
    public void BackToScanner()
    {
        SceneManager.LoadScene("03_Scanner");
    }

    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }

    public void OpenOrganInformation()
    {
        SceneManager.LoadScene("05_OrganInfo");
    }

    public void OpenARDisplayForOrgan(string organName)
    {
        SelectedOrganManager.SetSelectedOrgan(organName);
        SceneManager.LoadScene("04_ARDisplay");
    }
}
