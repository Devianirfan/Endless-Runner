using UnityEngine;

/// <summary>
/// Singleton that spawns new “Tile” prefabs in an endless line
/// whenever it is asked to do so by EndTileTrigger.
/// </summary>
public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    [Tooltip("Drag the Tile prefab (parent object that contains Cube + Cylinder) here")]
    public GameObject tilePrefab;

    // Reference to the last spawned tile so we know where to place the next one.
    private Transform lastTile;

    private void Awake()
    {
        // Simple singleton guard.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Optional: bootstrap with one tile if the scene does not already contain one.
        if (lastTile == null)
        {
            GameObject initial = GameObject.FindWithTag("Tile");
            lastTile = initial != null ? initial.transform : null;
        }

        // Ensure there is at least one tile.
        if (lastTile == null)
        {
            SpawnTile();
        }
    }

    /// <summary>
    /// Instantiates a new Tile directly in front of the previous one.
    /// </summary>
    public void SpawnTile()
    {
        Vector3 spawnPos = Vector3.zero;

        if (lastTile != null)
        {
            // Get the Z length of the last tile via renderer bounds
            float zLength = lastTile.GetComponentInChildren<Renderer>().bounds.size.z;
            spawnPos = lastTile.position + new Vector3(0f, 0f, zLength);
        }

        GameObject newTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
        lastTile = newTile.transform;
    }
}