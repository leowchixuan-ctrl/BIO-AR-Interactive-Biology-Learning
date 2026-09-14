using UnityEngine;
using UnityEngine.SceneManagement;

public class UserGuideManager : MonoBehaviour
{
    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }

    public void BackToSplash()
    {
        SceneManager.LoadScene("01_Splash");
    }
}