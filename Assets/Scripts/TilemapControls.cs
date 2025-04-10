using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapControls : MonoBehaviour
{
    public FactoryTile factoryTile;

    private Tilemap tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        // get the mouse position
        Vector2 pos = Input.mousePosition;

        // changes screen to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        Vector3Int clickedCell = tilemap.WorldToCell(worldPos);
        TileBase tile = tilemap.GetTile(clickedCell);

        if (tile is LandTile)
        {
            tilemap.SetTile(clickedCell, factoryTile);
        }
    }
}
