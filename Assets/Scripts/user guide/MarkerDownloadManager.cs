using UnityEngine;

public class MarkerDownloadManager : MonoBehaviour
{
    [SerializeField]
    private string markerPackUrl =
        "https://drive.google.com/file/d/16nq4pV1F5TENbbs6GWEhIfFZpQ_9rv6A/view?usp=sharing";

    public void DownloadMarkerPack()
    {
        if (string.IsNullOrWhiteSpace(markerPackUrl))
        {
            Debug.LogWarning("Marker pack URL has not been configured.");
            return;
        }

        Application.OpenURL(markerPackUrl);
    }
}