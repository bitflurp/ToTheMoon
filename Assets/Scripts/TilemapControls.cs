using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
public class TilemapControls : MonoBehaviour
{
    public FactoryTile factoryTile;
    public GameObject button;
    private Tilemap tilemap;
    private Button cB;

    public int rec = 10;

    //for recource ui

     public TMP_Text RecText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        //References Button to Give Cell Data
        cB = GetComponent<Button>();

    }
    void Update()
    {
        //code
        RecText.text = $"{rec}";
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


        cB.curRec = rec;
        // Coordinets for tiles = clickCell
        // tile type is tile
        Debug.Log($"TILE INFO" + clickedCell + tile + rec);

        if (tile is LandTile)
        {
            

            //Makes Button visible and sends it to the cell position
            button.SetActive(true);
            button.transform.position = clickedCell;
        

            //Sends button Current Cell Data to Button
            cB.currentCell = clickedCell;
            cB.currentTile = tile;







            // tilemap.SetTile(clickedCell, factoryTile);
        }
    }

}
