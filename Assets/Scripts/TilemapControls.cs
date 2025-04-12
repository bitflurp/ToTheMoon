using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class TilemapControls : MonoBehaviour
{
    public FactoryTile factoryTile;
    public GameObject buttonCreate;
    public GameObject buttonProduce;

    public GameObject buttonGather;

    //Classes
    private Tilemap tilemap;
    private Button cB;
    private TurnSystem turnSystem;

    //Recourses Vars
    public int rec = 10;

    public int workForce = 10;
    public int money = 10;

    //for recource ui
     public TMP_Text recText;

     public TMP_Text workForceText;

     public TMP_Text moneyText;

    //Vars for Procedures
    public int productionCounter = 0;
    public int gatherCounter = 0;

   

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        //References Button to Give Cell Data
        cB = GetComponent<Button>();

       

    }
    void Update()
    {
        // shows Current Rec
        recText.text = $"{rec}";

        workForceText.text = $"{workForce}";

        moneyText.text = $"{money}";
    }

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
            buttonCreate.SetActive(true);
            buttonCreate.transform.position = clickedCell;


            //Sends Current Cell Data to Button
            cB.currentCell = clickedCell;
            cB.currentTile = tile;

            //If Produce Button Exists when clicking on a land time, if turns it off
            if(buttonProduce != null)
                {
                     buttonProduce.SetActive(false);
                }
           
        }


        if (tile is FactoryTile)
        {
            buttonCreate.SetActive(false);

            //Makes Button visible and sends it to the cell position
            buttonProduce.SetActive(true);
            buttonProduce.transform.position = clickedCell;

            //Sends Current Cell Data to Button
            cB.currentCell = clickedCell;
            cB.currentTile = tile;


            Debug.Log($"there is" + productionCounter + " Producing");
           

        }
        if (tile is ResTile)
            {
                 //Makes Button visible and sends it to the cell position
                buttonGather.SetActive(true);
                buttonGather.transform.position = clickedCell;

                 //Sends Current Cell Data to Button
                 cB.currentCell = clickedCell;
                 cB.currentTile = tile;

                 Debug.Log($"ResTile is doing"+ gatherCounter);
            }
    }
}