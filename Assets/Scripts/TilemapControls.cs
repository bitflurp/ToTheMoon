using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using System.Linq;


public class TilemapControls : MonoBehaviour
{
    //turn Data
    private int dayCounter = 1;
    private int nextQuota = 8;

    //Grid Data
    private int gridX = 12;
    private int gridY = 12;

    //for Tiles
    public FactoryTile factoryTile;
    public LandTile landTile;
    public StallTile stallTile;

    //For Buttons
    public GameObject buttonCreate;
    public GameObject buttonProduce;
    public GameObject buttonGather;
    public GameObject buttonExpodition;

    public GameObject buttonIncrement;
    public GameObject buttonStartProcedure;

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
    public TMP_Text stateText;
    public TMP_Text dayText;

    public TMP_Text workForceNoText;

    //Vars for Procedures
    public int productionCounter = 0;
    public int gatherCounter = 0;
    public int recruitCounter = 0;

    //Vars for WFA
    private int workForceAllocation = 0;


    //Lists

    List<Vector3Int> isProducing = new List<Vector3Int>();
    List<Vector3Int> isGathering = new List<Vector3Int>();
    List<Vector3Int> isRecruiting = new List<Vector3Int>();


    //Dictionaries

    Dictionary<Vector3Int, int> stallData = new();
    Dictionary<Vector3Int, int> workForceProduction = new();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        dayText.text = $"{$"DAY {dayCounter}"}";


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
        stateText.text= $"{$"TILE INFO" + clickedCell + tile + rec}";

        if (tile is LandTile)
        {


            //Makes Button visible and sends it to the cell position
            buttonCreate.SetActive(true);
            


         

            //If Produce Button Exists when clicking on a land time, if turns it off
            if (buttonProduce || buttonExpodition || buttonGather || buttonStartProcedure  != null)
            {
                buttonProduce.SetActive(false);
                buttonExpodition.SetActive(false);
                buttonGather.SetActive(false);
                buttonStartProcedure.SetActive(false);
            }

        }


        if (tile is FactoryTile)
        {
            buttonCreate.SetActive(false);

            //Makes Button visible and sends it to the cell position
            buttonProduce.SetActive(true);

            if (isRecruiting.Contains(clickedCell) == true)
            {

                stateText.text = $"{$"there is {recruitCounter} Recruiting"}";

            }
            else if (isProducing.Contains(clickedCell) == true) {

                stateText.text = $"{$"there is {workForceProduction[clickedCell]} Producing"}";
            }

            if(buttonGather || buttonExpodition != null)
            {
                buttonGather.SetActive(false);
                buttonExpodition.SetActive(false);
            }


        }
        if (tile is ResTile)
        {
            //Makes Button visible and sends it to the cell position
            buttonGather.SetActive(true);

            stateText.text = $"{$"there is" + gatherCounter + " Gathering"}";
            //
            if(buttonCreate || buttonProduce || buttonExpodition != null)
            {
                buttonCreate.SetActive(false);
                buttonProduce.SetActive(false);
                buttonExpodition.SetActive(false);
            }
        }
        if (tile is RuleTile)
        {
            buttonExpodition.SetActive(true);
            stateText.text = $"{$"locked"}";

              if(buttonCreate || buttonGather || buttonProduce != null) 
                    {
                        buttonCreate.SetActive(false);
                        buttonGather.SetActive(false);
                        buttonProduce.SetActive(false);
                    }
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
            stateText.text = $"{$"NO DOUGH"}";

            //closes popup after no dough
            buttonCreate.SetActive(false);
        }


    }


    public void Production()
    {
        if (isProducing.Contains(clickedCell) == false && isRecruiting.Contains(clickedCell) == false)
        {

            //productionCounter++;
            //isProducing.Add(clickedCell);
            //stateText.text = $"{$"there is {productionCounter} Producing"}";

            buttonStartProcedure.SetActive(true);
            workForceNoText.text = $"{workForceAllocation}";

        }
        else if (isRecruiting.Contains(clickedCell) == true)
        {
            stateText.text = $"{$"This Factory is Recruiting "}";
            
        }
        else
        {
            stateText.text = $"{$"Already Producing"}" ;

        }

        buttonProduce.SetActive(false);

    }


    public void Recruit()
    {

        int whenStallEnds = dayCounter + 2;


        if (isRecruiting.Contains(clickedCell) == false && isProducing.Contains(clickedCell) == false)
        {

            isRecruiting.Add(clickedCell);

            // add location of current cell + when the stall will end for that specific cell
            stallData.Add(clickedCell, whenStallEnds);


            //Set tile to stall tile
            tilemap.SetTile(clickedCell, stallTile);

            Debug.Log(isRecruiting.Count);
        }
        else if (isProducing.Contains(clickedCell) == true)
        {
            stateText.text = $"{$"This Factory is in Produciton"}";

        }
        else
        {
            stateText.text = $"{$"Already Recruiting"}";

        }



        buttonProduce.SetActive(false);


    }


    public void Gather()
    {
        if (isGathering.Contains(clickedCell) == false)
        {

            gatherCounter++;
            isGathering.Add(clickedCell);
            stateText.text = $"{$"there is {gatherCounter} Gathering"}";
            
        }
        else
        {
            stateText.text = $"{$"already Gathering"}";

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

                        stateText.text = $"{$"A new state has been discovered"}";
                        
                    }
                }

            }
        }

    }


    //WFA Procedures


    public void WFAProduction()
    {
        if (workForce >= workForceAllocation)
        {
            isProducing.Add(clickedCell);
            workForceProduction.Add(clickedCell, workForceAllocation);
            workForce -= workForceAllocation;
            workForceAllocation = 0;
        }
        else
        {

            stateText.text = $"{$"You don't have enough workers"}";

        }

        buttonStartProcedure.SetActive(false);
    }

    public void WFAChoice()
    {

        if (workForceAllocation < 5)
        {
            workForceAllocation++;
            workForceNoText.text = $"{workForceAllocation}";
        }
        else
        {

            stateText.text = $"{$"Maximum WorkForce limit Reached"}";

        }



    }


    //On End Turn Procedure

    public void EndTurn()
    {




        //Clear Lists
        //Don't Need to clear Recruit list Due to stallchecks as it clears it there
        isProducing.Clear();
        isGathering.Clear();
        


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
        int tempWFA = 0;

        for(int i = workForceProduction.Count - 1; i >=0; i-- )
        {
            var item = workForceProduction.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            tempWFA += itemValue;
            workForceProduction.Remove(itemKey);

        }



        // Multiply Factories producing by product per factory producing (2)
        int addMoney = tempWFA * 2;

        //add profit Recourse to player Recourse 
        money = money + addMoney;

        // Reset Factory Production 
        productionCounter = 0;

        //workforce Resets
        workForce += tempWFA;

    }

    public void GatherProfit()
    {

        

        // Multiply Factories producing by product per factory producing (2)
        int addRec = gatherCounter * 2;

        //add profit Recourse to player Recourse 
        rec = rec + addRec;

        // Reset Factory Production 
        gatherCounter = 0;


    }

    public void RecruitProfit()
    {


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

            stateText.text = $"{$"QUOTA REACHED"}";

        }
        else
        {

            stateText.text = $"{$"QUOTA FAILED"}";

        }

    }

    public void StartTurn()
    {



        dayText.text = $"{$"DAY {dayCounter}"}";

    }

    
    
    //Checks for StallTiles, on stall tile gets its Data, 
    //Checks dictionary for when the stall ends, 
    //check if today is when stall ends and if it is :
    //turns it back into a factory tile, adds to recruit counter, and removes that tiles entry form their dictionary and list
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

                        isRecruiting.Remove(nowTilePos);

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




