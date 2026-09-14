using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrganInfoManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private TMP_Text organTitleText;
    [SerializeField] private TMP_Text overviewText;
    [SerializeField] private TMP_Text structureText;
    [SerializeField] private TMP_Text functionText;
    [SerializeField] private TMP_Text didYouKnowText;

    [Header("Organ Information")]
    [SerializeField] private OrganInfoData[] organDataList;

    private void Start()
    {
        Debug.Log(
            "Organ Info received: " +
            SelectedOrganManager.GetSelectedOrgan()
        );

        DisplaySelectedOrgan();
    }

    private void DisplaySelectedOrgan()
    {
        string selectedOrgan =
            SelectedOrganManager.GetSelectedOrganDisplayName();

        OrganInfoData selectedData = null;

        foreach (OrganInfoData organData in organDataList)
        {
            if (organData != null &&
                organData.organName == selectedOrgan)
            {
                selectedData = organData;
                break;
            }
        }

        if (selectedData == null)
        {
            Debug.LogWarning(
                "No information found for: " + selectedOrgan);
            return;
        }

        organTitleText.text = selectedData.organName;

        overviewText.text =
            "Overview\n\n" + selectedData.overview;

        structureText.text =
            "Structure\n\n" + selectedData.structure;

        functionText.text =
            "Function\n\n" + selectedData.organFunction;

        didYouKnowText.text =
            "Did You Know?\n\n" + selectedData.didYouKnow;
    }

    public void BackToARDisplay()
    {
        SceneManager.LoadScene("04_ARDisplay");
    }

    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }

    public void StartQuiz()
    {
        SceneManager.LoadScene("06_Quiz");
    }

    public void BackToScanner()
    {
        SceneManager.LoadScene("03_Scanner");
    }
}