using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class MultiOrganManager : MonoBehaviour
{
    [Serializable]
    public struct OrganSetup
    {
        [Tooltip("Exact image name from the Reference Image Library")]
        public string markerName;

        [Tooltip("Name used by Organ Info, Quiz and Summary")]
        public string organName;

        public GameObject organPrefab;
    }

    [Header("Organ Setup List")]
    [SerializeField] private OrganSetup[] allOrgans;

    private ARTrackedImageManager trackManager;
    private string lastSelectedOrgan;

    private void Awake()
    {
        trackManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        if (trackManager != null)
        {
            trackManager.trackablesChanged.AddListener(OnImagesChanged);
        }
    }

    private void OnDisable()
    {
        if (trackManager != null)
        {
            trackManager.trackablesChanged.RemoveListener(OnImagesChanged);
        }
    }

    private void OnImagesChanged(
        ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            UpdateTrackedImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            UpdateTrackedImage(trackedImage);
        }
    }

    private void UpdateTrackedImage(ARTrackedImage trackedImage)
    {
        string detectedMarker = trackedImage.referenceImage.name;

        if (!TryGetOrganSetup(detectedMarker, out OrganSetup setup))
        {
            Debug.LogWarning(
                "No organ setup found for marker: " + detectedMarker);

            return;
        }

        bool isTracking =
            trackedImage.trackingState == TrackingState.Tracking;

        Transform existingOrgan =
            trackedImage.transform.Find("SpawnedOrgan");

        if (isTracking)
        {
            // Update Organ Info and Quiz selection.
            if (lastSelectedOrgan != setup.organName)
            {
                SelectedOrganManager.SetSelectedOrgan(setup.organName);
                lastSelectedOrgan = setup.organName;

                Debug.Log(
                    "Selected organ updated to: " + setup.organName);
            }

            // Spawn only once.
            if (existingOrgan == null)
            {
                SpawnOrgan(trackedImage, setup);

                existingOrgan =
                    trackedImage.transform.Find("SpawnedOrgan");
            }
        }

        if (existingOrgan != null)
        {
            existingOrgan.gameObject.SetActive(isTracking);
        }
    }

    private void SpawnOrgan(
        ARTrackedImage trackedImage,
        OrganSetup setup)
    {
        if (setup.organPrefab == null)
        {
            Debug.LogError(
                "Organ prefab is missing for: " + setup.organName);

            return;
        }

        GameObject newOrgan = Instantiate(
            setup.organPrefab,
            trackedImage.transform);

        newOrgan.name = "SpawnedOrgan";
        newOrgan.transform.localPosition = Vector3.zero;
        newOrgan.transform.localRotation = Quaternion.identity;

        Debug.Log(
            "Spawned " + setup.organName +
            " for marker " + setup.markerName);
    }

    private bool TryGetOrganSetup(
        string detectedMarker,
        out OrganSetup matchedSetup)
    {
        foreach (OrganSetup setup in allOrgans)
        {
            if (string.Equals(
                setup.markerName,
                detectedMarker,
                StringComparison.OrdinalIgnoreCase))
            {
                matchedSetup = setup;
                return true;
            }
        }

        matchedSetup = default;
        return false;
    }
}