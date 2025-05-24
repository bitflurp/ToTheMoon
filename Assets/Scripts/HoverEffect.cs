using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverEffect : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile hoverTile;
    public bool click = false;
    [SerializeField] private Tile landTile;
    private Vector3Int preMouse = new Vector3Int();
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }
    void Update()
    {
        Vector3Int clickedCell;
        TileBase tile;

        Vector2 pos = Input.mousePosition;

        // changes screen to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        clickedCell = tilemap.WorldToCell(worldPos);
        tile = tilemap.GetTile(clickedCell);
        if (click == false)
        {
            if (tile != null)
            {
                if (!clickedCell.Equals(preMouse))
                {
                    tilemap.SetTile(preMouse, landTile);
                    tilemap.SetTile(clickedCell, hoverTile);
                    preMouse = clickedCell;

                }
            }
            else { tilemap.SetTile(preMouse, landTile); }
        }
        else
        {
            
         }
        
        
    }
}
