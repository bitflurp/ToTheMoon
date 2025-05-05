using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Data.SqlTypes;
using System;
using Random=UnityEngine.Random;

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
    public ResTile recTile;
    public PaleTile paleTile;
    public PaleJuicedTile paleJuice;

    //For LandTiles
    public NewYorkLand nyLand;
    public WyomingLand wyoLand;

    //For Buttons
    public GameObject buttonCreate;
    public GameObject buttonProduce;
    public GameObject buttonRecruit;
    public GameObject buttonGather;
    public GameObject buttonExpodition;




    public GameObject buttonIncrement;
    public GameObject buttonReduce;
    public GameObject buttonStartProcedure;

    //TileData Vars
    public Vector3Int clickedCell;
    public TileBase tile;

    //Classes
    private Tilemap tilemap;

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
    public TMP_Text quotaText;

    public TMP_Text workForceNoText;
    public TMP_Text workForceCostText;

    //Vars for Procedures
    public int productionCounter = 0;
    public int gatherCounter = 0;
    public int recruitCounter = 0;

    //Vars for WFA
    private int workForceAllocation = 1;


    //Lists

    List<Vector3Int> isProducing = new List<Vector3Int>();
    List<Vector3Int> isGathering = new List<Vector3Int>();
    List<Vector3Int> isRecruiting = new List<Vector3Int>();


    //Dictionaries
    Dictionary<Vector3Int, TileBase> factoryStateData = new();
    Dictionary<Vector3Int, TileBase> RecStateData = new();
    Dictionary<Vector3Int, TileBase> weatherData = new();


    Dictionary<Vector3Int, int> stallData = new();
    Dictionary<Vector3Int, int> recData = new();


    Dictionary<Vector3Int, int> workForceProduction = new();
    Dictionary<Vector3Int, int> workForceRecruit = new();
    Dictionary<Vector3Int, int> workForceGather = new();


    Dictionary<Type, List<Vector3Int>> recStatePos = new() {

        {typeof(NewYorkTile) , new List<Vector3Int>{new Vector3Int(5,1,0), new Vector3Int(6,1,0) } } ,


        {typeof(WyomingTile) , new List<Vector3Int>{new Vector3Int(9,1,0), new Vector3Int(10,1,0) , new Vector3Int(11,1,0) }  },



    };

    //test vars
    private GameObject curProcedure;
    private int dayToRemove;
    private int statesUnlocked = 1;

    //Check Vars
    private int industryCounter = 0;
    private int quota = 30;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
       

        RecourceAdd();

        dayText.text = $"{$"DAY {dayCounter}"}";
        quotaText.text = ($"Quota: {quota}$  (Day {nextQuota - 1})");

    }
    void Update()
    {
        // shows Current Rec
        recText.text = $"REC: {rec}";

        workForceText.text = $"WF: {workForce}";

        moneyText.text = $"PROFIT: {money}";



        workForceCostText.text = $"Produce: {workForceAllocation}$ :{workForceAllocation} Rec | +{workForceAllocation*2}$ \nRecruit: {workForceAllocation * 2}$ : TURN STALL  | +{workForceAllocation} WF \nGather: {workForceAllocation * 2}$  | +{workForceAllocation * 2} Rec";

    }

    void OnMouseDown()
    {
        // get the mouse position
        Vector2 pos = Input.mousePosition;

        // changes screen to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);

        clickedCell = tilemap.WorldToCell(worldPos);
        tile = tilemap.GetTile(clickedCell);

        //Debug.Log($"{tile.GetType()}");



        // Coordinets for tiles = clickCell
        // tile type is tile
        stateText.text = $"{$"TILE INFO" + clickedCell + tile + rec}";

        if (tile is LandTile)
        {


            //Makes Button visible and sends it to the cell position
            buttonCreate.SetActive(true);





            //If Produce Button Exists when clicking on a land time, if turns it off
            if (buttonProduce || buttonExpodition || buttonGather || buttonStartProcedure != null)
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
            else if (isProducing.Contains(clickedCell) == true)
            {

                stateText.text = $"{$"there is {workForceProduction[clickedCell]} Producing"}";
            }

            if (buttonGather || buttonExpodition || buttonStartProcedure != null)
            {
                buttonGather.SetActive(false);
                buttonExpodition.SetActive(false);
                buttonStartProcedure.SetActive(false);
            }


        }

        if (tile is ResTile)
        {
            //Makes Button visible and sends it to the cell position
            buttonGather.SetActive(true);


            stateText.text = $"{recData[clickedCell]}/30 Recourses left";


            if (isGathering.Contains(clickedCell) == true)
            {
                stateText.text = $"{$"there is {workForceGather[clickedCell]} Gathering"}";

            }


            if (buttonCreate || buttonProduce || buttonExpodition || buttonStartProcedure != null)
            {
                buttonCreate.SetActive(false);
                buttonProduce.SetActive(false);
                buttonExpodition.SetActive(false);
                buttonStartProcedure.SetActive(false);
            }
        }

        if (tile is RuleTile)
        {
            buttonExpodition.SetActive(true);
            //stateText.text = $"{$"locked"}";

            if (buttonCreate || buttonGather || buttonProduce || buttonStartProcedure != null)
            {
                buttonCreate.SetActive(false);
                buttonGather.SetActive(false);
                buttonProduce.SetActive(false);
                buttonStartProcedure.SetActive(false);
            }
        }




    }






    //Procedures





    public void CreateFactory()
    {

        if (rec >= 5)
        {
           
            // Gets the state Tile the factory was placed on 
            factoryStateData.Add(clickedCell, tile);


            //On click Sets current tile to Factory tile
            tilemap.SetTile(clickedCell, factoryTile);
            rec -= 5;

            industryCounter++;

        }
        else
        {
            stateText.text = $"{$"NO DOUGH"}";
        }

        //closes popup 
        buttonCreate.SetActive(false);
    }


    public void Production()
    {
        if (isProducing.Contains(clickedCell) == false && isRecruiting.Contains(clickedCell) == false)
        {

            curProcedure = EventSystem.current.currentSelectedGameObject;


            buttonStartProcedure.SetActive(true);
            workForceNoText.text = $"{workForceAllocation}";

        }
        else if (isRecruiting.Contains(clickedCell) == true)
        {
            stateText.text = $"{$"This Factory is Recruiting "}";

        }
        else
        {
            stateText.text = $"{$"Already Producing"}";

        }

        buttonProduce.SetActive(false);

    }


    public void Recruit()
    {


        if (isRecruiting.Contains(clickedCell) == false && isProducing.Contains(clickedCell) == false)
        {

            curProcedure = EventSystem.current.currentSelectedGameObject;


            buttonStartProcedure.SetActive(true);
            workForceNoText.text = $"{workForceAllocation}";

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

            curProcedure = EventSystem.current.currentSelectedGameObject;


            buttonStartProcedure.SetActive(true);
            workForceNoText.text = $"{workForceAllocation}";


        }
        else
        {
            stateText.text = $"{$"already Gathering"}";

        }


        buttonGather.SetActive(false);

    }

    public void Expodition()
    {

        TileBase stateTile = tile;

        ExpoditionCheck(stateTile);

        buttonExpodition.SetActive(false);

    }

    public void ExpoditionCheck(TileBase stateTile)
    {

        //checks if player has enough resources for expodition
        if (money >= 10)
        {
            money -= 10;

            statesUnlocked++;

            // checks on the grid for rule tiles and sets them to land tiles
            for (int x = 2; x < gridX; x++)
            {
                for (int y = 1; y < gridY; y++)
                {
                    Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                    TileBase nowTile = tilemap.GetTile(nowTilePos);

                    if (nowTile == stateTile)
                    {
                        switch (nowTile)
                        {



                            case NewYorkTile:

                                if (recStatePos.ContainsKey(nowTile.GetType()))
                                {

                                    if (recStatePos[nowTile.GetType()].Contains(nowTilePos) == true)
                                    {   
                                        //Adding what state the rec tiles are in
                                        RecStateData.Add(nowTilePos, nowTile);
                                        //Adding the amount of recourses for newly added rec tiles
                                        recData.Add(nowTilePos, 30);

                                        tilemap.SetTile(nowTilePos, recTile);
                                       

                                    }
                                    else
                                    {

                                        //change state locked tile to land
                                        tilemap.SetTile(nowTilePos, nyLand);
                                    }
                                }
                               
                                  

                                break;


                            case WyomingTile:

                                if (recStatePos.ContainsKey(nowTile.GetType()))
                                {

                                    if (recStatePos[nowTile.GetType()].Contains(nowTilePos) == true)
                                    {
                                        //Adding what state the rec tiles are in
                                        RecStateData.Add(nowTilePos, nowTile);
                                        //Adding the amount of recourses for newly added rec tiles
                                        recData.Add(nowTilePos, 30);

                                        tilemap.SetTile(nowTilePos, recTile);


                                    }
                                    else
                                    {

                                        //change state locked tile to land
                                        tilemap.SetTile(nowTilePos, wyoLand);
                                    }
                                }

                                break;

                        }

                        stateText.text = $"{$"A new state has been discovered"}";

                    }
                }

            }
        }


    }




    //WFA Procedures


    public void WFAProduction()
    {

        int whenStallEnds = dayCounter + 2;



        switch (true)
        {

            // WFA on Production
            case true when curProcedure == buttonProduce:

                if (workForce >= workForceAllocation && rec >= workForceAllocation)
                {
                    isProducing.Add(clickedCell);
                    workForceProduction.Add(clickedCell, workForceAllocation);

                    //costs
                    workForce -= workForceAllocation;
                    money -= workForceAllocation;
                    rec -= workForceAllocation;
                    DebtCheck();
                }
                else
                {

                    stateText.text = $"{$"You don't have enough recources"}";

                }
                break;


            //WFA on Recruit
            case true when curProcedure == buttonRecruit:

                if (workForce >= workForceAllocation)
                {

                    isRecruiting.Add(clickedCell);

                    // add location of current cell + when the stall will end for that specific cell
                    stallData.Add(clickedCell, whenStallEnds);

                    workForceRecruit.Add(clickedCell, workForceAllocation);

                    //costs
                    workForce -= workForceAllocation;
                    money -= workForceAllocation * 2;
                    DebtCheck();


                    //Set tile to stall tile
                    tilemap.SetTile(clickedCell, stallTile);

                    Debug.Log($"there is {workForceRecruit[clickedCell]} Recruiting");
                }
                else
                {

                    stateText.text = $"{$"You don't have enough recources"}";

                }


                break;

            //WFA on Gather
            case true when curProcedure == buttonGather:

                if (workForce >= workForceAllocation)
                {


                    isGathering.Add(clickedCell);
                    workForceGather.Add(clickedCell, workForceAllocation);

                    //Costs
                    workForce -= workForceAllocation;
                    money -= workForceAllocation * 2;
                    DebtCheck();

                    Debug.Log($"there is {workForceGather[clickedCell]} Gathering");
                }
                else
                {

                    stateText.text = $"{$"You don't have enough recources"}";

                }


                break;




        }

        // set WFA to 1 so on next procedure it starts on 1 WF chosen
        workForceAllocation = 1;
        buttonStartProcedure.SetActive(false);
    }

    public void WFAChoice()
    {


        // Gets Current Button Pressed and puts it into curButton Temp Var
        GameObject curButton = EventSystem.current.currentSelectedGameObject;


        switch (true)
        {

            //If button is Increment: Increments WFA + Checks for MAX
            case true when curButton == buttonIncrement:
                if (workForceAllocation < 5)
                {
                    workForceAllocation++;
                    workForceNoText.text = $"{workForceAllocation}";
                }
                else
                {

                    stateText.text = $"{$"Maximum WorkForce limit Reached"}";

                }
                break;

            //if Button is Reduce: Reduces WFA + Checks for MIN
            case true when curButton == buttonReduce:
                if (workForceAllocation > 1)
                {
                    workForceAllocation--;
                    workForceNoText.text = $"{workForceAllocation}";
                }
                else
                {

                    stateText.text = $"{$"Cannot Have 0 WorkForce Allocated"}";

                }


                break;

        }



    }





    //On End Turn Procedure

    public void EndTurn()
    {

        RemoveUI();


        //Clear Lists
        //Don't Need to clear Recruit list Due to stallchecks as it clears it there
        isProducing.Clear();
        isGathering.Clear();



        //Increment Turn
        dayCounter++;



      
        if (industryCounter >= 5)
        {

            WeatherApply();
            WeatherRemove();

        }


        StallCheck();


        //GetProfit
        ProductionProfit();
        GatherProfit();


        if (industryCounter >= 10 ) {

            tilemap.SetTile(new Vector3Int(11,10,0), paleTile);
            Pale();


        }
      


        if (dayCounter == nextQuota)
        {

            QuotaReach();
            
        }



      


        StartTurn();
    }

    public void ProductionProfit()
    {
        int addMoney = 0;

        for (int i = workForceProduction.Count - 1; i >= 0; i--)
        {
            var item = workForceProduction.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            productionCounter += itemValue;
            workForceProduction.Remove(itemKey);

        }

        ///For Randomized Profits (Commented cause its hard to playtest with it)
        //for (int i = productionCounter; i > 0; i--)
        //{
           // int randomProfit = Random.Range(1, 5);

          //  addMoney += randomProfit;
            //Debug.Log($"{randomProfit} was added: now is {addMoney}");
        //}



        // Multiply Factories producing by product per factory producing (2)
        addMoney = productionCounter * 2;

        //add profit Recourse to player Recourse 
        money = money + addMoney;

        //workforce Resets
        workForce += productionCounter;

        // Reset Factory Production 
        productionCounter = 0;
    }

    public void GatherProfit()
    {
        for (int i = workForceGather.Count - 1; i >= 0; i--)
        {
            var item = workForceGather.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            gatherCounter += itemValue;
            //Removes the gathered recourse from recourse tile
            recData[itemKey] -= itemValue * 2;


            //Check if the recourse tile is depleted
            if (recData[itemKey] <= 0)
            {
                //changes tile
                tilemap.SetTile(itemKey, landTile);

                //makes sure that player doesnt profit more recourse then there is availible
                gatherCounter += recData[itemKey] / 2;

                //Removes tile from dictionary
                recData.Remove(itemKey);

            }

            workForceGather.Remove(itemKey);

        }



        // Multiply Factories producing by product per factory producing (2)
        int addRec = gatherCounter * 2;

        //add profit Recourse to player Recourse 
        rec = rec + addRec;

        workForce += gatherCounter;

        // Reset Factory Production 
        gatherCounter = 0;


    }



    public void QuotaReach()
    {

        if (money >= quota)
        {
            //changes deadline to next week (have to add it into a fail/win state 
            nextQuota += 7;
            quota = quota * 2;
            stateText.text = $"{$"QUOTA REACHED"}";
            quotaText.text = ($"Quota: {quota}$  (Day {nextQuota - 1})");

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

                if (stallData.ContainsKey(nowTilePos))
                {
                    if (stallData[nowTilePos] == dayCounter)
                    {

                        recruitCounter += workForceRecruit[nowTilePos];





                        // Multiply Factories producing by product per factory producing (2)
                        int addWorkForce = recruitCounter * 1;

                        //add profit Recourse to player Recourse 
                        workForce += addWorkForce;

                        //Resets workforce
                        workForce += recruitCounter;

                        // Reset Factory Production 
                        recruitCounter = 0;






                        isRecruiting.Remove(nowTilePos);

                        //Removesdata from dictionary
                        stallData.Remove(nowTilePos);

                        //removes workforce data from dictionary
                        workForceRecruit.Remove(nowTilePos);



                        //If stall is unstalled during a turn where the tile is weathered
                        //it will check if the current tile is the state which is weathered through
                        //weatherData.ContainsValue(factoryStateData[nowTilePos])
                        // and if it is will set the tile to weathered" and add that tile to weatherdata
                        //so as weather stops , all effected state tiles will be set back to their og state
                        if (weatherData.ContainsValue(factoryStateData[nowTilePos]))
                        {

                            tilemap.SetTile(nowTilePos, null);
                            weatherData[nowTilePos] = factoryTile;

                        }
                        else
                        {

                            //change stall Tile to Facotry
                            tilemap.SetTile(nowTilePos, factoryTile);

                        }


                        Debug.Log($"Has Unstalled");
                    }
                }
            }
        }




    }


    public void RecourceAdd()
    {




        for (int x = 1; x < gridX; x++)
        {
            for (int y = 1; y < gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (nowTile is ResTile)
                {
                    if (!recData.ContainsKey(nowTilePos))
                    {
                        recData.Add(nowTilePos, 30);

                    }
                }
            }
        }





    }




    public void DebtCheck()
    {

        if (money < -100)
        {

            gameObject.SetActive(false);

        }



    }




    //WEATHER CODE
    public void WeatherApply()
    {

        int chanceWeather = Random.Range(1, 4);
        Debug.Log($"{chanceWeather}");

        //Checks if weather Hits and if its the day to remove Weather so as to not constantly be in WEATHER STATE
        if (chanceWeather == 3 &&  weatherData.Count()  == 0 ) {

            //Makes it so day the weather gets removes is the next turn
            dayToRemove = dayCounter + 1;

            //Randomizes which state to effect
            int chanceStateWeather = Random.Range(1, statesUnlocked);
            Debug.Log($"{chanceStateWeather}");




            for (int x = 2; x < gridX; x++)
        {
            for (int y = 1; y < gridY; y++)
            {
               
                    
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);
               

              

               //Checks if current tile is factory, and if it is gets the state tile it was placed on
               factoryStateData.TryGetValue(nowTilePos, out TileBase factoryState);
               RecStateData.TryGetValue(nowTilePos, out TileBase recState);



                    //Effects only chosen weather
                    switch (chanceStateWeather) {




                        //if New York got selected
                        case 1:


                            if (nowTile is NewYorkLand || factoryState is NewYorkLand || recState is NewYorkTile)
                            {

                                weatherData.Add(nowTilePos, nowTile);

                                tilemap.SetTile(nowTilePos, stallTile);

                               //Debug.Log($"Bad weather has made New York unworkable");
                            }





                            break;

                        //If Wyoming Got selected
                        case 2:



                            if (nowTile is WyomingLand || factoryState is WyomingLand)
                            {

                                weatherData.Add(nowTilePos, nowTile);

                                tilemap.SetTile(nowTilePos, null);

                                //Debug.Log($"Bad weather has made {nowTile} unworkable");
                            }


                            break;


                        default:

                            Debug.Log($"the range is fucked");

                            break;
                    }
              



            }
        }

        }
    }


    public void WeatherRemove()
    {

        if (dayToRemove == dayCounter)
        {


            for (int i = weatherData.Count - 1; i >= 0; i--)
            {
                var item = weatherData.ElementAt(i);
                var itemKey = item.Key;
                var itemValue = item.Value;


                tilemap.SetTile(itemKey, itemValue);

                weatherData.Remove(itemKey);


            }





        }

    }









    //PALE CODE
    public void Pale() {

        List<Vector3Int> paleHit = new List<Vector3Int>();
        List<Vector3Int> paleEffect = new List<Vector3Int>();

        //List<Vector3Int> Juiced = new List<Vector3Int>();
        for (int x = 1; x < gridX; x++)
        {
            for (int y = 1; y < gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);
                //Debug.Log(nowTilePos);

                if (nowTile is PaleTile) 
                {




                    if (paleEffect.Contains(nowTilePos) == false)  //&& Juiced.Contains(nowTilePos) == false)
                    {


                        Vector3Int N = new Vector3Int(x, y + 1, 0);
                        Vector3Int S = new Vector3Int(x, y - 1, 0);
                        Vector3Int W = new Vector3Int(x - 1, y, 0);
                        Vector3Int E = new Vector3Int(x + 1, y, 0);

                        paleHit.Add(N);

                        paleHit.Add(S);

                        paleHit.Add(W);

                        paleHit.Add(E);


                        //Juiced.Add(nowTilePos);
                        tilemap.SetTile(nowTilePos, paleJuice);


                    }



                    for (int i = paleHit.Count - 1; i >= 0; i--)
                    {

                       
                        var item = paleHit.ElementAt(i);


                        if (tilemap.GetTile(item) != null) {


                            tilemap.SetTile(item, paleTile);
                            paleEffect.Add(item);
                        }

                        

                      


                    }

                }


            }


        }




        //Debug.Log(paleHit.Count);
        //Debug.Log(paleEffect.Count);

        paleHit.Clear();
        paleEffect.Clear(); 









    }
































    //EXTRA STUFF IGNORE
    public void RemoveUI() {



        buttonCreate.SetActive(false);
        buttonGather.SetActive(false);
        buttonProduce.SetActive(false);
        buttonStartProcedure.SetActive(false);
        buttonExpodition.SetActive(false);







    }








}


