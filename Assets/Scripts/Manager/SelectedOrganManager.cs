using UnityEngine;

public static class SelectedOrganManager
{
    private const string SelectedOrganKey = "SelectedOrgan";

    public static void SetSelectedOrgan(string organName)
    {
        string markerName = ConvertToMarkerName(organName);

        if (string.IsNullOrEmpty(markerName))
        {
            Debug.LogWarning("Invalid organ name: " + organName);
            return;
        }

        PlayerPrefs.SetString(SelectedOrganKey, markerName);
        PlayerPrefs.Save();

        Debug.Log("Selected organ: " + markerName);
    }

    // Used by the Scanner and 3D model scene.
    public static string GetSelectedOrgan()
    {
        return PlayerPrefs.GetString(
            SelectedOrganKey,
            "heart_marker"
        );
    }

    // Used by Organ Information, Quiz, Summary and Progress.
    public static string GetSelectedOrganDisplayName()
    {
        switch (GetSelectedOrgan())
        {
            case "heart_marker":
                return "Heart";

            case "brain_marker":
                return "Brain";

            case "lung_marker":
                return "Lungs";

            case "liver_marker":
                return "Liver";

            case "kidney_marker":
                return "Kidneys";

            default:
                Debug.LogWarning(
                    "Unknown selected organ: " +
                    GetSelectedOrgan()
                );

                return "Heart";
        }
    }

    private static string ConvertToMarkerName(string organName)
    {
        switch (organName)
        {
            case "heart_marker":
            case "Heart":
                return "heart_marker";

            case "brain_marker":
            case "Brain":
                return "brain_marker";

            case "lung_marker":
            case "Lung":
            case "Lungs":
                return "lung_marker";

            case "liver_marker":
            case "Liver":
                return "liver_marker";

            case "kidney_marker":
            case "Kidney":
            case "Kidneys":
                return "kidney_marker";

            default:
                return null;
        }
    }
}