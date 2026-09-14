using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizQuestionUI : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] private TMP_Text questionText;

    [Header("Answers")]
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Toggle[] answerToggles;
    [SerializeField] private Text[] answerLabels;

    public void DisplayQuestion(QuizQuestionData questionData)
    {
        questionText.text = questionData.question;

        for (int i = 0; i < answerLabels.Length; i++)
        {
            answerLabels[i].text = questionData.answers[i];
            answerToggles[i].isOn = false;
        }

        toggleGroup.SetAllTogglesOff();
    }

    public bool IsAnswered()
    {
        return toggleGroup.AnyTogglesOn();
    }

    public int GetSelectedAnswerIndex()
    {
        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (answerToggles[i].isOn)
            {
                return i;
            }
        }

        return -1;
    }
}