using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckIfDrawComplete : MonoBehaviour
{
    [SerializeField]
    private Material _invisibleMat;
    [SerializeField]
    private List<Vector2> _pointsTouched = new List<Vector2>();
    [SerializeField]
    private List<Vector2> _pointsToTouch;
    [SerializeField]
    private GameManager _gameManager;
    private bool _complete;
    private void OnEnable()
    {
        GetComponent<LineRenderer>().material = _invisibleMat;
        // = GetComponent<PathDrawer>().Path.Points;
    }

    private void Update()
    {
        if (!_complete)
        {
            GameObject activeBrush = GameObject.FindGameObjectWithTag("Brush");

            if (activeBrush != null)
            {
                LineRenderer newLine = activeBrush.GetComponent<LineRenderer>();

                for (int i = 0; i < newLine.positionCount; i++)
                {
                    Vector2 linePoint = newLine.GetPosition(i);
                    foreach (Vector2 targetPoint in _pointsToTouch)
                    {
                        if (Vector2.Distance(linePoint, targetPoint) < 0.2f && !_pointsTouched.Contains(targetPoint))
                        {
                            _pointsTouched.Add(targetPoint);
                        }
                    }
                }
                CheckIfDone();
            }
        }
    }

    private void CheckIfDone()
    {
        GameObject activeBrush = GameObject.FindGameObjectWithTag("Brush");

        if (_pointsTouched.Count() == _pointsToTouch.Count())
        {
            _complete = true;
            _gameManager.MyGameData.SetLettersComplete(_gameManager.MyGameData.GetLettersComplete() + 1);

            if (activeBrush.GetComponent<Draw>() != null)
            {
                activeBrush.GetComponent<Draw>().Erase();
            }
            else
            {
                Destroy(activeBrush);
            }           
        }
    }
}
