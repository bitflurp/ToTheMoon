using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapControls : MonoBehaviour
{
    public FactoryTile factoryTile;
    public GameObject button;
    public GameObject buttonProduce;

    //Classes
    private Tilemap tilemap;
    private Button cB;
    private TurnSystem turnSystem;


    public int rec = 10;


    //Vars
    public int productionCounter = 0;

   

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        //References Button to Give Cell Data
        cB = GetComponent<Button>();

       

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

        //Give rec data to button
        cB.curRec = rec;
        cB.curProdCount = productionCounter;

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


        if (tile is FactoryTile)
        {
            button.SetActive(false);

            //Makes Button visible and sends it to the cell position
            buttonProduce.SetActive(true);
            buttonProduce.transform.position = clickedCell;

            Debug.Log($"there is" + productionCounter + " Producing");
           

        }
    }
}
