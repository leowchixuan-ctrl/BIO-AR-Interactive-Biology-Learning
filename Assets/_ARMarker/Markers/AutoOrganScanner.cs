using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class AutoOrganScanner : MonoBehaviour
{
    [Header("Scanner Navigation")]
    [SerializeField] private ScannerManager scannerManager;

    private ARTrackedImageManager trackedImageManager;

    // Prevent multiple scene loads from the same detected marker
    private bool hasScanned = false;

    private void Awake()
    {
        trackedImageManager =
            GetComponent<ARTrackedImageManager>();

        // Automatically find ScannerManager if not assigned
        if (scannerManager == null)
        {
            scannerManager =
                FindAnyObjectByType<ScannerManager>();
        }

        if (scannerManager == null)
        {
            Debug.LogError(
                "AutoOrganScanner: ScannerManager was not found.");
        }

        if (trackedImageManager.referenceLibrary == null)
        {
            Debug.LogError(
                "AutoOrganScanner: Reference Image Library is not assigned.");
        }
    }

    private void OnEnable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.AddListener(
                OnTrackablesChanged);
        }
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackablesChanged.RemoveListener(
                OnTrackablesChanged);
        }
    }

    private void OnTrackablesChanged(
        ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        if (hasScanned)
            return;

        // Newly detected images
        foreach (ARTrackedImage trackedImage in args.added)
        {
            CheckTrackedImage(trackedImage);

            if (hasScanned)
                return;
        }

        // Existing images whose tracking state changed
        foreach (ARTrackedImage trackedImage in args.updated)
        {
            CheckTrackedImage(trackedImage);

            if (hasScanned)
                return;
        }
    }

    private void CheckTrackedImage(
        ARTrackedImage trackedImage)
    {
        if (trackedImage == null)
            return;

        if (trackedImage.trackingState != TrackingState.Tracking)
            return;

        string markerName =
            trackedImage.referenceImage.name;

        Debug.Log(
            "Detected biology marker: " + markerName);

        TriggerSceneTransition(markerName);
    }

    private void TriggerSceneTransition(
        string scannedName)
    {
        if (hasScanned)
            return;

        if (scannerManager == null)
        {
            Debug.LogError(
                "Cannot open AR Display: ScannerManager is missing.");

            return;
        }

        hasScanned = true;

        Debug.Log(
            "Opening AR Display for: " + scannedName);

        scannerManager.OpenARDisplayForOrgan(
            scannedName);
    }
}