using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{

    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }

    public void OpenScanner()
    {
        SceneManager.LoadScene("03_Scanner");
    }
}