using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverEffect : MonoBehaviour
{
    public Tilemap HoverTilemap;
    public Tile hoverTile;
    public bool click = false;
    public Tile landTile;
    public Vector3Int preMouse = new Vector3Int();
    void Start()
    {
        HoverTilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        Vector3Int clickedCell;
        TileBase tile;

        Vector2 pos = Input.mousePosition;

        // changes screen to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        clickedCell = HoverTilemap.WorldToCell(worldPos);
        tile = HoverTilemap.GetTile(clickedCell);
        if (click == false)
        {
            if (tile != null)
            {
                if (!clickedCell.Equals(preMouse))
                {
                    HoverTilemap.SetTile(preMouse, landTile);
                    HoverTilemap.SetTile(clickedCell, hoverTile);
                    preMouse = clickedCell;

                }
            }
            else { HoverTilemap.SetTile(preMouse, landTile); }
        }
        else
        {
            
         }
        
        
    }
}
