using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private TMP_Text organNameText;
    [SerializeField] private TMP_Text validationText;

    [Header("Question Panels")]
    [SerializeField] private QuizQuestionUI[] questionUIs;

    [Header("Quiz Data")]
    [SerializeField] private OrganQuizData[] quizDataList;

    private OrganQuizData selectedQuiz;
    private string selectedOrganName;

    private void Start()
    {
        LoadSelectedOrganQuiz();
    }

    private void LoadSelectedOrganQuiz()
    {
        string detectedMarker =
            SelectedOrganManager.GetSelectedOrgan();

        string selectedOrgan =
            SelectedOrganManager.GetSelectedOrganDisplayName();

        selectedOrganName =
            ConvertMarkerToOrganName(detectedMarker);

        organNameText.text = selectedOrganName;
        selectedQuiz = null;

        foreach (OrganQuizData quizData in quizDataList)
        {
            if (quizData != null &&
                quizData.organName == selectedOrganName)
            {
                selectedQuiz = quizData;
                break;
            }
        }

        if (selectedQuiz == null)
        {
            ShowValidation(
                "Quiz data was not found for " +
                selectedOrganName + ".");

            return;
        }

        if (selectedQuiz.questions.Length != questionUIs.Length)
        {
            ShowValidation(
                "The quiz must contain exactly three questions.");

            return;
        }

        for (int i = 0; i < questionUIs.Length; i++)
        {
            questionUIs[i].DisplayQuestion(
                selectedQuiz.questions[i]);
        }

        ShowValidation("");
    }

    public void SubmitQuiz()
    {

        string selectedOrgan =
            SelectedOrganManager.GetSelectedOrganDisplayName();

        if (selectedQuiz == null)
        {
            ShowValidation("Quiz data is not available.");
            return;
        }

        foreach (QuizQuestionUI questionUI in questionUIs)
        {
            if (!questionUI.IsAnswered())
            {
                ShowValidation(
                    "Please answer all three questions.");

                return;
            }
        }

        int score = 0;

        for (int i = 0; i < questionUIs.Length; i++)
        {
            int selectedIndex =
                questionUIs[i].GetSelectedAnswerIndex();

            int correctIndex =
                selectedQuiz.questions[i].correctAnswerIndex;

            if (selectedIndex == correctIndex)
            {
                score++;
            }
        }

        PlayerPrefs.SetInt("LastQuizScore", score);

        PlayerPrefs.SetInt(
            "LastQuizTotal",
            selectedQuiz.questions.Length);

        PlayerPrefs.SetString(
            "LastQuizOrgan",
            selectedOrganName);

        PlayerPrefs.SetInt(
            "Completed_" + selectedOrganName,
            1);

        PlayerPrefs.Save();

        SceneManager.LoadScene("07_Summary");
    }

    private string ConvertMarkerToOrganName(string markerName)
    {
        switch (markerName)
        {
            case "heart_marker":
            case "Heart":
                return "Heart";

            case "brain_marker":
            case "Brain":
                return "Brain";

            case "lung_marker":
            case "Lungs":
                return "Lungs";

            case "liver_marker":
            case "Liver":
                return "Liver";

            case "kidney_marker":
            case "Kidneys":
                return "Kidneys";

            default:
                Debug.LogWarning(
                    "Unknown organ marker: " + markerName);

                return markerName;
        }
    }

    private void ShowValidation(string message)
    {
        if (validationText != null)
        {
            validationText.text = message;
        }
        else if (!string.IsNullOrEmpty(message))
        {
            Debug.LogWarning(message);
        }
    }

    public void BackToOrganInfo()
    {
        SceneManager.LoadScene("05_OrganInfo");
    }

    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }
}