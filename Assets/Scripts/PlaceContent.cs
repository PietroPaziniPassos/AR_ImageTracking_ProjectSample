using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

[RequireComponent(typeof(ARTrackedImageManager))]
public class PlaceContent : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ArObjects = new List<GameObject>();
    private readonly Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager arTrackedImageManager;
    private void Awake() {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
    private void OnEnable() {
        arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    private void OnDisable() {
        arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (var trackedImage in eventArgs.added) {
            foreach (GameObject currentObject in ArObjects) {
                if(string.Compare(trackedImage.referenceImage.name, currentObject.name, StringComparison.OrdinalIgnoreCase) == 0 && !instantiatedObjects.ContainsKey(currentObject.name)) {
                    GameObject instantiatedObject = Instantiate(currentObject, trackedImage.transform);
                    instantiatedObjects[currentObject.name] = instantiatedObject;
                }
            }
        }
    }
}
