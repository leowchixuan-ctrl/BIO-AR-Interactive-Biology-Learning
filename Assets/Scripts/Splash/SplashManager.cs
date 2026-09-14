using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    private const string TutorialCompletedKey =
        "ARBiology_FirstTimeGuideCompleted";

    public void OpenHome()
    {
        bool tutorialCompleted =
            PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1;

        if (tutorialCompleted)
        {
            // User has already completed the guide before
            SceneManager.LoadScene("02_Home");
        }
        else
        {
            // First-time user
            SceneManager.LoadScene("UserGuide");
        }
    }
}