# Unity AR Image Tracking Project Sample

## Overview

Esse repositório é um projeto Unity de realidade aumentada baseado no rastreamento de imagem.
O objetivo é facilitar a construção de aplicações desse modelo. 

## How to get started

1. Clone this repository on Unity;
2. Open the Main scene;
3. Import the images that you want to track inside Textures folder;
4. Add them to Reference Image Library in Textures folder;
5. Import the 3D objects inside 3D folder;
6. Add them to Ar Objects in Place Content Component attached in XR Origin;
7. Generate a build for your android;
8. Be happy!

## About the code

### Librarys
* [ARFoundation](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@5.0/manual/index.html)
* [UniGTF](https://github.com/vrm-c/UniVRM)
* [Universal RP](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/manual/index.html)
* [XR Plugin Management](https://docs.unity3d.com/Packages/com.unity.xr.management@4.4/manual/index.html)
* [Google ARCore](https://docs.unity3d.com/Packages/com.unity.xr.arcore@5.0/manual/index.html)

### Main functionality

```cs
using UnityEngine.XR.ARSubsystems;

public class PlaceContent : MonoBehaviour
{
    [SerializeField]
    public bool followImage;
    [SerializeField]
    private List<GameObject> ArObjects = new List<GameObject>();
    private readonly Dictionary<string, GameObject> instantiatedObjects = new Dictionary<string, GameObject>();
    private ARTrackedImageManager arTrackedImageManager;

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
        foreach (var trackedImage in eventArgs.added) {
            foreach (GameObject currentObject in ArObjects) {
                if(string.Compare(trackedImage.referenceImage.name, currentObject.name, StringComparison.OrdinalIgnoreCase) == 0 && !instantiatedObjects.ContainsKey(currentObject.name)) {
                    GameObject instantiatedObject = Instantiate(currentObject, trackedImage.transform);
                    instantiatedObjects[currentObject.name] = instantiatedObject;
                }
            }
        }
        if(followImage) {
            foreach (var trackedImage in eventArgs.updated) {
                instantiatedObjects[trackedImage.referenceImage.name].SetActive(trackedImage.trackingState == TrackingState.Tracking);
            }
            foreach (var trackedImage in eventArgs.removed) {
                Destroy(instantiatedObjects[trackedImage.referenceImage.name]);
                instantiatedObjects.Remove(trackedImage.referenceImage.name);
            }
        }
    }
}
```
This code shows how the 3D object is generated and tracks the movement of the image.
It is also possible to observe that the 3D object corresponding to the image is identified by its name.
If the name of the object is the same as the name of the image, it will be generated, otherwise it will not be.
_______________________
Made by [Pietro Pazini](https://www.linkedin.com/in/pietro-pazini)