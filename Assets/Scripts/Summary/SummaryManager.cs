using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SummaryManager : MonoBehaviour
{
    [Header("Summary Text")]
    [SerializeField] private TMP_Text organNameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private TMP_Text feedbackText;

    private void Start()
    {
        DisplayResult();
    }

    private void DisplayResult()
    {
        string organName =
            PlayerPrefs.GetString("LastQuizOrgan", "Heart");

        int score =
            PlayerPrefs.GetInt("LastQuizScore", 0);

        int total =
            PlayerPrefs.GetInt("LastQuizTotal", 3);

        int percentage = 0;

        if (total > 0)
        {
            percentage =
                Mathf.RoundToInt((score / (float)total) * 100f);
        }

        organNameText.text = organName;
        scoreText.text = "Score: " + score + " / " + total;
        percentageText.text = percentage + "%";

        if (score == total)
        {
            feedbackText.text =
                "Excellent! You answered all questions correctly.";
        }
        else if (score >= 2)
        {
            feedbackText.text =
                "Good work! Review the organ information to improve further.";
        }
        else
        {
            feedbackText.text =
                "Keep learning. Review the organ information and try again.";
        }
    }

    public void ReviewQuiz()
    {
        SceneManager.LoadScene("06_Quiz");
    }

    public void ScanAnotherOrgan()
    {
        SceneManager.LoadScene("03_Scanner");
    }

    public void ReturnHome()
{
    PlayerPrefs.SetInt(
        "ARBiology_FirstTimeGuideCompleted",
        1
    );

    PlayerPrefs.Save();

    SceneManager.LoadScene("02_Home");
}
}