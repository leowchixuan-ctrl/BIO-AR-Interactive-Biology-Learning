using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirstTimeGuide : MonoBehaviour
{
    public Button[] allButtons;
    public Button[] guideOrder;

    public GameObject guidePanel;
    public TMP_Text guideText;
    public string[] guideMessages;

    private int currentStep = 0;

    private const string TutorialCompletedKey =
        "ARBiology_FirstTimeGuideCompleted";

    void Start()
    {
        bool completed =
            PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1;

        if (completed)
        {
            EnableAllButtons();

            if (guidePanel != null)
                guidePanel.SetActive(false);
        }
        else
        {
            StartGuide();
        }
    }

    void StartGuide()
    {
        currentStep = 0;

        // Some scenes, such as Scanner, do not have a guided button step.
        // In that case, leave their navigation available so the user can
        // recover if an external dependency such as camera permission or
        // marker recognition prevents the normal automatic transition.
        if (guideOrder == null || guideOrder.Length == 0)
        {
            CompleteGuide();
            return;
        }

        if (guidePanel != null)
            guidePanel.SetActive(true);

        ShowCurrentStep();
    }

    void ShowCurrentStep()
    {
        // Disable all tutorial-controlled buttons
        foreach (Button button in allButtons)
        {
            if (button != null)
                button.interactable = false;
        }

        // Enable only the required button
        if (currentStep < guideOrder.Length)
        {
            if (guideOrder[currentStep] != null)
                guideOrder[currentStep].interactable = true;
        }

        // Display tutorial message
        if (guideText != null &&
            currentStep < guideMessages.Length)
        {
            guideText.text = guideMessages[currentStep];
        }
    }

    public void NextStep()
    {
        currentStep++;

        if (currentStep >= guideOrder.Length)
        {
            CompleteGuide();
        }
        else
        {
            ShowCurrentStep();
        }
    }

    void CompleteGuide()
{
    // Only finish the guide for the current scene.
    // Do NOT mark the whole app tutorial as completed here.

    EnableAllButtons();

    if (guidePanel != null)
        guidePanel.SetActive(false);
}

    void EnableAllButtons()
    {
        if (allButtons == null)
            return;

        foreach (Button button in allButtons)
        {
            if (button != null)
                button.interactable = true;
        }
    }

    [ContextMenu("Reset First-Time Guide")]
    public void ResetGuide()
    {
        PlayerPrefs.DeleteKey(TutorialCompletedKey);
        PlayerPrefs.Save();

        Debug.Log("First-Time Guide reset.");
    }
}
