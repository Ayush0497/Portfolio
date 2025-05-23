using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDetection : MonoBehaviour
{
    private Tilemap tilemap;
    private int activeTileCount;  // Counter to track active tiles
    private int totalTileCount;

    public event Action OnShapeCompleted;
    private bool _scoreIncreased;
    private Coroutine alphaPulseCoroutine;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        CountActiveTiles(); // Initialize tile count

        // Define original and peach colors
        Color originalColor = tilemap.color;
        Color peachColor = new Color(168f / 255f, 122f / 255f, 122f / 255f, 1.0f);

        alphaPulseCoroutine = StartCoroutine(PulseTilemapColor(originalColor, peachColor, 0.7f, 1f, 0.7f));
    }

    private void CountActiveTiles()
    {
        activeTileCount = 0;
        totalTileCount = 0; // Initialize total tile count

        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                activeTileCount++;
            }
        }
        totalTileCount = activeTileCount; // Set total count after counting active tiles
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the brush enters the trigger
        if (collision.CompareTag("Brush"))
        {
            // Call method to remove tiles in radius
            RemoveTilesInRadius(collision.transform.position, collision.GetComponent<CircleCollider2D>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Continuously check for tiles in the radius while the brush stays in the trigger
        if (collision.CompareTag("Brush"))
        {
            RemoveTilesInRadius(collision.transform.position, collision.GetComponent<CircleCollider2D>());
        }
    }

    private void RemoveTilesInRadius(Vector2 brushPosition, CircleCollider2D brushCollider)
    {
        if (tilemap == null || brushCollider == null)
        {
            return;
        }

        // Get the radius from the CircleCollider2D component on the brush
        float radius = brushCollider.radius * brushCollider.transform.localScale.x;

        // Calculate the bounds for checking tiles
        Vector3Int minBounds = tilemap.WorldToCell(brushPosition - Vector2.one * radius);
        Vector3Int maxBounds = tilemap.WorldToCell(brushPosition + Vector2.one * radius);

        // Loop through tile positions within the bounding box
        for (int x = minBounds.x; x <= maxBounds.x; x++)
        {
            for (int y = minBounds.y; y <= maxBounds.y; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                if (tilemap.HasTile(tilePosition))
                {
                    // Calculate the distance from the brush position to the tile center
                    Vector3 tileCenter = tilemap.GetCellCenterWorld(tilePosition);
                    if (Vector2.Distance(brushPosition, tileCenter) <= radius)
                    {
                        // Remove the tile
                        tilemap.SetTile(tilePosition, null);
                        activeTileCount--; // Reduce tile count
                    }
                }
            }
        }

        // Check if the shape is completed
        if (activeTileCount <= totalTileCount * 0.1f)
        {
            CompleteShape();
        }
    }

    public void CompleteShape()
    {
        GameManager gameManager = null;

        // Finding where the shape was completed
        if (FindGameShapeIsIn("MainGameLeft"))
        {
            gameManager = GameObject.FindGameObjectWithTag("MainGameLeft").GetComponentInParent<GameManager>();
        }
        else if (FindGameShapeIsIn("MainGameCenter"))
        {
            gameManager = GameObject.FindGameObjectWithTag("MainGameCenter").GetComponentInParent<GameManager>();
        }
        else if (FindGameShapeIsIn("MainGameRight"))
        {
            gameManager = GameObject.FindGameObjectWithTag("MainGameRight").GetComponentInParent<GameManager>();
        }

        if (gameManager != null && !_scoreIncreased)
        {
            _scoreIncreased = true;
            // Adding a score for the letter
            gameManager.MyGameData.SetLettersComplete(gameManager.MyGameData.GetLettersComplete() + 1);
            // Adding the shape to completed shapes
            List<string> shapes = gameManager.MyGameData.GetShapesComplete();
            shapes.Add(transform.parent.name);
            gameManager.MyGameData.SetShapesComplete(shapes);
        }

        if (alphaPulseCoroutine != null)
        {
            StopCoroutine(alphaPulseCoroutine);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, 1f); // Set to fully visible
        }

        OnShapeCompleted?.Invoke();
    }

    private bool FindGameShapeIsIn(string gameTag)
    {
        GameObject go = GameObject.FindGameObjectWithTag(gameTag);
        if (go != null)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                go.GetComponent<RectTransform>(),
                Camera.main.WorldToScreenPoint(transform.position),
                Camera.main);
        }
        return false;
    }

    private IEnumerator PulseTilemapColor(Color fromColor, Color toColor, float minAlpha, float maxAlpha, float duration)
    {
        while (true)
        {
            // Fade from min to max alpha
            float elapsed = 0f;
            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(minAlpha, maxAlpha, elapsed / duration);
                tilemap.color = Color.Lerp(fromColor, toColor, elapsed / duration);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Fade back from max to min alpha
            elapsed = 0f;
            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(maxAlpha, minAlpha, elapsed / duration);
                tilemap.color = Color.Lerp(toColor, fromColor, elapsed / duration);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, alpha);
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
