using System;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 position;

    private void Awake()
    {
        offset = transform.localPosition;
    }
    
    // TODO: For logic in this class, check what needs to be changed for 3D compatibility.

    public void StartNewGame()
    {
        Track(Vector3.zero);
    }

    public void Track(Vector3 focusPoint)
    {
        position = focusPoint + offset;
        transform.localPosition = position;
    }
}
