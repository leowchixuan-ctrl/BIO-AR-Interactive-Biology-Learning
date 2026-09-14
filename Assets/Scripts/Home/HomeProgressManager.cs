using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeProgressManager : MonoBehaviour
{
    [Header("Progress Interface")]
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private Slider progressSlider;

    private readonly string[] completionKeys =
    {
        "Completed_Heart",
        "Completed_Brain",
        "Completed_Lungs",
        "Completed_Liver",
        "Completed_Kidneys"
    };

    private void Start()
    {
        UpdateProgressDisplay();
    }

    private void UpdateProgressDisplay()
    {
        int completedCount = GetCompletedOrganCount();
        int percentage = Mathf.RoundToInt(
            completedCount / 5f * 100f);

        if (progressText != null)
        {
            progressText.text = completedCount + " / 5";
        }

        if (percentageText != null)
        {
            percentageText.text = percentage + "%";
        }

        if (progressSlider != null)
        {
            progressSlider.maxValue = 5;
            progressSlider.value = completedCount;
        }
    }

    private int GetCompletedOrganCount()
    {
        int completedCount = 0;

        foreach (string key in completionKeys)
        {
            if (PlayerPrefs.GetInt(key, 0) == 1)
            {
                completedCount++;
            }
        }

        return completedCount;
    }
}
