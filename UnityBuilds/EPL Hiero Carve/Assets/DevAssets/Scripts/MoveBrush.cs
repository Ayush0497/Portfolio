using System.Collections;
using System.Collections.Generic;
using TouchScript;
using TouchScript.Pointers;
using UnityEngine;

public class MoveBrush : MonoBehaviour
{
    private Pointer _currentPointer; // The current pointer controlling this brush
    private bool _isDragging; // Flag to indicate dragging state

    private void Start()
    {
        TouchManager.Instance.PointersUpdated += OnUpdate;
        TouchManager.Instance.PointersReleased += OnReleaseGesture;
        //_isDragging = false; //removed it, it's now set in CreateBrush
    }

    private void OnUpdate(object sender, PointerEventArgs e)
    {
        // If we are dragging and the current pointer is valid
        if (_currentPointer != null && _isDragging)
        {
            // Update the brush's position based on the current pointer's position
            Vector3 currentPosition = new Vector3(
                _currentPointer.Position.x,
                _currentPointer.Position.y,
                Camera.main.nearClipPlane // Use near clip plane for 2D
            );
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(currentPosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f); // Set z to 0 for 2D
        }
    }

    private void OnReleaseGesture(object sender, PointerEventArgs e)
    {
        // Iterate through released pointers to see if the current pointer was released
        foreach (var pointer in e.Pointers)
        {
            if (_currentPointer != null && pointer.Id == _currentPointer.Id)
            {
                Destroy(gameObject); // Destroy this brush object
                _currentPointer = null; // Reset current pointer
                _isDragging = false; // Reset dragging state
                break; // Exit the loop after processing
            }
        }
    }

    // Method to set the current pointer when the brush is pressed
    public void SetCurrentPointer(Pointer pointer)
    {
        _currentPointer = pointer; // Set the pointer
        _isDragging = true; // Start dragging
    }
}