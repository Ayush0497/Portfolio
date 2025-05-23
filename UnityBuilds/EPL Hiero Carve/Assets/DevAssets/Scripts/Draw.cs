using TouchScript;
using UnityEngine;

public class Draw : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector2 _previousPosition;
    private bool _drawing;
    public float lineWidth = 0.1f;

    private void Start()
    {
        TouchManager.Instance.PointersUpdated += OnUpdate;
        TouchManager.Instance.PointersReleased += OnReleaseGesture;
        _drawing = true;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 1;
        _previousPosition = transform.position;

        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }

    private void OnUpdate(object sender, PointerEventArgs e)
    {
        Vector2 currentPosition = e.Pointers[e.Pointers.Count - 1].Position;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(currentPosition);

        if (_drawing)
        {
            if (_previousPosition == new Vector2(transform.position.x, transform.position.y))
            {
                _lineRenderer.SetPosition(0, worldPosition);
            }
            else
            {
                _lineRenderer.positionCount++;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, worldPosition);
            }

            _drawing = CheckIfInZone(currentPosition, e);
            _previousPosition = worldPosition;
        }
    }

    private bool CheckIfInZone(Vector2 currentPosition, PointerEventArgs e)
    {
        bool inZone = false;
        GameObject[] drawZones = GameObject.FindGameObjectsWithTag("DrawZone");

        foreach (GameObject zone in drawZones)
        {
            RectTransform rectTransform = zone.GetComponent<RectTransform>();

            bool isPointerInside = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, currentPosition, Camera.main);

            if (isPointerInside)
            {
                inZone = true;
            }
        }

        return inZone;
    }

    /// <summary>
    /// When the user releases touch the line stops drawing and gets cut off.
    /// </summary>
    private void OnReleaseGesture(object sender, PointerEventArgs e)
    {
        TouchManager.Instance.PointersUpdated -= OnUpdate;
        TouchManager.Instance.PointersReleased -= OnReleaseGesture;
        _drawing = false;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Get rid of the line renderer on the screen.
    /// </summary>
    public void Erase()
    {
        TouchManager.Instance.PointersUpdated -= OnUpdate;
        TouchManager.Instance.PointersReleased -= OnReleaseGesture;
        Destroy(this.gameObject);
    }
}