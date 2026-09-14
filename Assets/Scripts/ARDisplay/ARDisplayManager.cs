using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARDisplayManager : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private TMP_Text detectedOrganText;

    [Header("3D Models")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private OrganSetup[] allOrgans;

    [Serializable]
    public struct OrganSetup
    {
        [Tooltip("Use: Heart, Brain, Lungs, Liver or Kidneys")]
        public string organName;

        public GameObject organPrefab;
    }

    private void Start()
    {
        // The stored value may be "Heart" or "heart_marker".
        string storedOrgan =
            SelectedOrganManager.GetSelectedOrgan();

        string cleanOrganName =
            GetCleanOrganName(storedOrgan);

        // Example: Heart Detected
        if (detectedOrganText != null)
        {
            detectedOrganText.text =
                cleanOrganName + " Detected";
        }

        // Use the clean name to find the matching prefab.
        SpawnOrganModel(cleanOrganName);
    }

    private string GetCleanOrganName(string organValue)
    {
        if (string.IsNullOrWhiteSpace(organValue))
        {
            Debug.LogWarning("No selected organ was found.");
            return "Unknown";
        }

        switch (organValue.Trim().ToLowerInvariant())
        {
            case "heart":
            case "heart_marker":
            case "heart organ":
                return "Heart";

            case "brain":
            case "brain_marker":
            case "brain organ":
                return "Brain";

            case "lung":
            case "lungs":
            case "lung_marker":
            case "lungs_marker":
            case "lung organ":
            case "lungs organ":
                return "Lungs";

            case "liver":
            case "liver_marker":
            case "liver organ":
                return "Liver";

            case "kidney":
            case "kidneys":
            case "kidney_marker":
            case "kidneys_marker":
            case "kidney organ":
            case "kidneys organ":
                return "Kidneys";

            default:
                Debug.LogWarning(
                    "Unknown selected-organ value: " + organValue);

                return "Unknown";
        }
    }

    private void SpawnOrganModel(string targetOrgan)
    {
        if (spawnPoint == null)
        {
            Debug.LogError(
                "Spawn Point is not assigned in ARDisplayManager.");
            return;
        }

        foreach (OrganSetup setup in allOrgans)
        {
            if (string.Equals(
                setup.organName,
                targetOrgan,
                StringComparison.OrdinalIgnoreCase))
            {
                if (setup.organPrefab == null)
                {
                    Debug.LogError(
                        "Organ prefab is missing for: " +
                        setup.organName);
                    return;
                }

                // Spawn DIRECTLY as a child of SpawnPoint
                GameObject spawnedOrgan =
                    Instantiate(setup.organPrefab, spawnPoint);

                // Force model to stay exactly at SpawnPoint
                spawnedOrgan.transform.localPosition =
                    Vector3.zero;

                spawnedOrgan.transform.localRotation =
                    Quaternion.identity;

                Debug.Log(
                    targetOrgan +
                    " spawned at fixed SpawnPoint.");

                return;
            }
        }

        Debug.LogWarning(
            "Could not find a 3D model for: " +
            targetOrgan);
    }

    public void BackToScanner()
    {
        SceneManager.LoadScene("03_Scanner");
    }

    public void OpenHome()
    {
        SceneManager.LoadScene("02_Home");
    }

    public void OpenOrganInformation()
    {
        SceneManager.LoadScene("05_OrganInfo");
    }
}