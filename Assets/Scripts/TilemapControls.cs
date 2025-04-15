using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using TreeEditor;

public class TilemapControls : MonoBehaviour
{
    //Grid Data
    private int gridX = 12;
    private int gridY = 12;

    //for Tiles
    public FactoryTile factoryTile;
    public LandTile landTile;

    //For Game Obj
    public GameObject buttonCreate;
    public GameObject buttonProduce;
    public GameObject buttonGather;
    public GameObject buttonExpodition;

    //TileData Vars
    public Vector3Int clickedCell;
    public TileBase tile;

    //Classes
    private Tilemap tilemap;
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
    public int recruitCounter = 0;


    //Lists

    List<Vector3Int> isProducing = new List<Vector3Int>();
    List<Vector3Int> isGathering = new List<Vector3Int>();
    List<Vector3Int> isRecruiting = new List<Vector3Int>();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

      


    }
    void Update()
    {
        // shows Current Rec
        recText.text = $"REC: {rec}";

        workForceText.text = $"WF: {workForce}";

        moneyText.text = $"PROFIT: {money}";
    }

    void OnMouseDown()
    {
        // get the mouse position
        Vector2 pos = Input.mousePosition;

        // changes screen to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        clickedCell = tilemap.WorldToCell(worldPos);
        tile = tilemap.GetTile(clickedCell);

     


        // Coordinets for tiles = clickCell
        // tile type is tile
        Debug.Log($"TILE INFO" + clickedCell + tile + rec);

        if (tile is LandTile)
        {


            //Makes Button visible and sends it to the cell position
            buttonCreate.SetActive(true);
            buttonCreate.transform.position = clickedCell;


         

            //If Produce Button Exists when clicking on a land time, if turns it off
            if (buttonProduce != null)
            {
                buttonProduce.SetActive(false);
            }

        }


        if (tile is FactoryTile)
        {
            buttonCreate.SetActive(false);

            //Makes Button visible and sends it to the cell position
            buttonProduce.SetActive(true);

            if (isRecruiting.Contains(clickedCell) == true)
            {

                Debug.Log($"there is {recruitCounter} Recruiting");

            }
            else if (isProducing.Contains(clickedCell) == true) {

                Debug.Log($"there is {productionCounter} Producing");

            }

            


        }
        if (tile is ResTile)
        {
            //Makes Button visible and sends it to the cell position
            buttonGather.SetActive(true);
            buttonGather.transform.position = clickedCell;

          
            Debug.Log($"there is" + gatherCounter + " Gathering");
        }
        if (tile is RuleTile)
        {
            buttonExpodition.SetActive(true);
            Debug.Log($"locked");
        }




    }






    //Procedures





    public void CreateFactory()
    {

        //Debug.Log($"I am" + currentCell + currentTile);

        if (rec >= 5)


        {   //On click Sets current tile to Factory tile

            tilemap.SetTile(clickedCell, factoryTile);

            //closes popup after creating factory
            buttonCreate.SetActive(false);

            rec -=  5;

        }
        else
        {

            Debug.Log($"NO DOUGH");

            //closes popup after no dough
            buttonCreate.SetActive(false);
        }


    }


    public void Production()
    {
        if (isProducing.Contains(clickedCell) == false && isRecruiting.Contains(clickedCell) == false)
        {

            productionCounter++;
            isProducing.Add(clickedCell);
            Debug.Log($"there is {productionCounter} Producing");
        }
        else if (isRecruiting.Contains(clickedCell) == true)
        {

            Debug.Log($"This Factory is Recruiting ");
        }
        else
        {

            Debug.Log($"Already Producing");

        }

        buttonProduce.SetActive(false);

    }


    public void Recruit()
    {
        if (isRecruiting.Contains(clickedCell) == false && isProducing.Contains(clickedCell) == false)
        {


            recruitCounter++;
            isRecruiting.Add(clickedCell);

            Debug.Log($"there is {recruitCounter} Recruiting");
        }
        else if (isProducing.Contains(clickedCell) == true)
        {

            Debug.Log($"This Factory is in Produciton");

        }
        else
        {

            Debug.Log($"Already Recruiting");

        }



        buttonProduce.SetActive(false);


    }


    public void Gather()
    {
        if (isGathering.Contains(clickedCell) == false)
        {

            gatherCounter++;
            isGathering.Add(clickedCell);

            Debug.Log($"there is {gatherCounter} Gathering");
        }
        else
        {
            
            Debug.Log($"already Gathering");

        }




    }

    public void Expodition()
    {
        //checks if player has enough resources for expodition
        if (money >= 10)
        {
            money -= 10;
           
            // checks on the grid for rule tiles and sets them to land tiles
            for (int x = 2; x < gridX; x++)
            {
                for (int y = 1; y < gridY; y++)
                {
                    Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                    TileBase nowTile = tilemap.GetTile(nowTilePos);

                    if (nowTile is RuleTile)
                    {
                        //change state locked tile to land
                        tilemap.SetTile(nowTilePos, landTile);

                        Debug.Log($"A new state has been discovered");
                    }
                }

            }
        }

    }



    public void OnEndDay()
    {

        isProducing.Clear();
        isGathering.Clear();
        isRecruiting.Clear();

    }










}