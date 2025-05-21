using UnityEngine;

/// <summary>
/// Detects when the player rolls onto the cylinder at the end of a tile,
/// then tells the TileManager to spawn another tile.
/// </summary>
public class EndTileTrigger : MonoBehaviour
{
    private bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure we only react once and only to the player.
        if (triggered || !other.CompareTag("Player")) return;

        triggered = true;

        // Ask the TileManager for a new tile.
        if (TileManager.Instance != null)
        {
            TileManager.Instance.SpawnTile();
        }

        // Optionally clean up this tile after a short delay to save memory.
        Destroy(transform.parent.gameObject, 5f);
    }
}