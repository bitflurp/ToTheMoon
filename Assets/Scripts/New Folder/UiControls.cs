using UnityEngine;
using UnityEngine.Tilemaps;

public class UiControls : MonoBehaviour
{

    private Tilemap tilemap;
    private Vector3Int previousTileCoordinate;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = tilemap.WorldToCell(mouseWorldPos);

        if (tileCoordinate != previousTileCoordinate)
        {
            tilemap.SetTileFlags(previousTileCoordinate, TileFlags.None);
            tilemap.SetColor(previousTileCoordinate, new Color(255, 255, 255, 255));


            tilemap.SetTileFlags(tileCoordinate, TileFlags.None);
            tilemap.SetColor(tileCoordinate, new Color(191, 191, 191, 255));
            previousTileCoordinate = tileCoordinate;
        }
    }
}
