using UnityEngine;

[System.Serializable]
public class QuizQuestionData
{
    [TextArea(2, 4)]
    public string question;

    public string[] answers = new string[4];

    [Range(0, 3)]
    public int correctAnswerIndex;
}

[CreateAssetMenu(
    fileName = "NewOrganQuizData",
    menuName = "BIO AR/Organ Quiz")]
public class OrganQuizData : ScriptableObject
{
    public string organName;
    public QuizQuestionData[] questions = new QuizQuestionData[3];
}