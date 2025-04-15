using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using TreeEditor;

public class TilemapControls : MonoBehaviour
{
    private int dayCounter = 1;
    private int nextQuota = 8;
   
    //Grid Data
    private int gridX = 12;
    private int gridY = 12;

    //for Tiles
    public FactoryTile factoryTile;
    public LandTile landTile;
    public StallTile stallTile;

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


    //Dictionaries

    Dictionary<Vector3Int, int> stallData = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"DAY {dayCounter}");


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

        int whenStallEnds = dayCounter + 2;



        if (tile is StallTile)
        {
            Debug.Log($"STALLING");

        }
        else
        {
            //curRecruitCount++;
           
            isRecruiting.Add(clickedCell);

            // add location of current cell + when the stall will end for that specific cell
            stallData.Add(clickedCell, whenStallEnds);


            //Set tile to stall tile
            tilemap.SetTile(clickedCell, stallTile);

            Debug.Log($"there is Recruiting");



        }

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



   
    
    
    
    
    //On End Turn Procedure

    public void EndTurn()
    {
       



        //Clear Lists
        isProducing.Clear();
        isGathering.Clear();
        isRecruiting.Clear();


        //Increment Turn
        dayCounter++;



        StallCheck();





        //GetProfit
        ProductionProfit();
        GatherProfit();
        RecruitProfit();

        if (dayCounter == nextQuota)
        {
            QuotaReach();
            //changes deadline to next week (have to add it into a fail/win state 
            nextQuota += 7;


        }




        StartTurn();
    }

    public void ProductionProfit()
    {
        //Debug.Log($"PRODUCE" + endProduction);

        // Multiply Factories producing by product per factory producing (2)
        int addMoney = productionCounter * 2;

        //add profit Recourse to player Recourse 
        money = money + addMoney;

        // Reset Factory Production 
        productionCounter = 0;

    }

    public void GatherProfit()
    {

            //Debug.Log($"GATHER" + endProduction);

            // Multiply Factories producing by product per factory producing (2)
            int addRec = gatherCounter * 2;

            //add profit Recourse to player Recourse 
            rec = rec + addRec;

            // Reset Factory Production 
            gatherCounter = 0;


    }

    public void RecruitProfit()
    {

        
            //Debug.Log($"Recruit" + endRecruit);

            // Multiply Factories producing by product per factory producing (2)
            int addWorkForce = recruitCounter * 1;

            //add profit Recourse to player Recourse 
            workForce = workForce + addWorkForce;

            // Reset Factory Production 
            recruitCounter = 0;


  
    }



    public void QuotaReach()
    {

        if (money >= 30)
        {

            Debug.Log($"QUOTA REACHED");

        }
        else
        {

            Debug.Log($"QUOTA FAILED");

        }

    }

    public void StartTurn()
    {
      


        Debug.Log($"DAY {dayCounter}");

    }

    public void StallCheck()
    {

        for (int x = 1; x < gridX; x++)
        {
            for (int y = 1; y < gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (nowTile is StallTile)
                {
                    if (stallData[nowTilePos] == dayCounter)
                    {

                        recruitCounter++;


                        Debug.Log($"RECRUIT: {recruitCounter}");

                        //Removesdata from dictionary
                        stallData.Remove(nowTilePos);

                        //change stall Tile to Facotry
                        tilemap.SetTile(nowTilePos, factoryTile);

                        Debug.Log($"Has Unstalled");
                    }
                }
            }
        }




    }

}