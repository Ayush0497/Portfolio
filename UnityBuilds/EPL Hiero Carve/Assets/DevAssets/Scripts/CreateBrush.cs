using System.Reflection;
using TouchScript;
using TouchScript.Pointers;
using UnityEngine;

public class CreateBrush : MonoBehaviour
{
    [SerializeField] private GameObject _brushPrefab;

    private void Start()
    {
        TouchManager.Instance.PointersPressed += OnPressGesture;
    }

    public void OnPressGesture(object sender, PointerEventArgs e)
    {
        foreach (TouchScript.Pointers.Pointer pointer in e.Pointers)
        {
            Vector2 pointerScreenPosition = pointer.Position;
            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(pointerScreenPosition.x, pointerScreenPosition.y, Camera.main.nearClipPlane));

            string panelTag = GetPanelUnderPointer(pointerScreenPosition);

            if (!string.IsNullOrEmpty(panelTag) && !LocateBrushInPanel(panelTag))
            {
                GameObject brush = Instantiate(_brushPrefab, new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, 0f), Quaternion.identity);
                brush.tag = "Brush";

                MoveBrush moveBrush = brush.GetComponent<MoveBrush>();
                moveBrush.SetCurrentPointer(pointer);
            }
        }
    }

    private string GetPanelUnderPointer(Vector2 pointerPosition)
    {
        // Check which panel the pointer is inside
        if (IsPointerInsidePanel("MainGameLeft", pointerPosition)) return "MainGameLeft";
        if (IsPointerInsidePanel("MainGameCenter", pointerPosition)) return "MainGameCenter";
        if (IsPointerInsidePanel("MainGameRight", pointerPosition)) return "MainGameRight";
        return "";
    }

    private bool IsPointerInsidePanel(string panelTag, Vector2 pointerPosition)
    {
        GameObject panel = GameObject.FindGameObjectWithTag(panelTag);
        if (panel == null) return false;

        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        // Use the pointer position directly, no need to convert it since we are checking against UI
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, pointerPosition, Camera.main);
    }

    private bool LocateBrushInPanel(string panelTag)
    {
        GameObject panel = GameObject.FindGameObjectWithTag(panelTag);
        if (panel == null) return false;

        RectTransform rectTransform = panel.GetComponent<RectTransform>();
        GameObject[] brushes = GameObject.FindGameObjectsWithTag("Brush");

        foreach (GameObject brush in brushes)
        {
            Vector2 brushScreenPosition = Camera.main.WorldToScreenPoint(brush.transform.position);
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, brushScreenPosition, Camera.main))
            {
                return true; // A brush is already in this panel
            }
        }

        return false; // No brush found in this panel
    }
}