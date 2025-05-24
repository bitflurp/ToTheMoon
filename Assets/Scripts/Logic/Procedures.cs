using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Procedures : MonoBehaviour
{

    private Tilemap tilemap;
    private PlayerData playerData;
    private TilemapControls clickData;
    private TileData tileData;
    private UserInterface uiData;
    private WFA wfaData;

    public GameObject curProcedure;

   
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        playerData = GetComponent<PlayerData>();
        clickData = GetComponent<TilemapControls>();
        tileData= GetComponent<TileData>();
        uiData = GetComponent<UserInterface>();
        wfaData = GetComponent<WFA>();
    }



    public void CreateFactory()
    {

        if (playerData.rec >= 5)
        {

            // Gets the state Tile the factory was placed on 
            tileData.factoryStateData.Add(clickData.clickedCell, clickData.tile);


            //On click Sets current tile to Factory tile
            tilemap.SetTile(clickData.clickedCell, tileData.factoryTile);
            playerData.rec -= 5;



        }
        else
        {
            // UI Script Needs to be added
            uiData.stateText.text = $"{$"NO DOUGH"}";
        }

        //closes popup 
        // Use the thingy to recognize which game object it is
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        clickData.hoverData.click = false;
    }


    public void Production()
    {
        if (tileData.workForceProduction.ContainsKey(clickData.clickedCell) == false && tileData.workForceRecruit.ContainsKey(clickData.clickedCell) == false)
        {

            curProcedure = EventSystem.current.currentSelectedGameObject;


            uiData.buttonStartProcedure.SetActive(true);

            //UI Script
            uiData.workForceNoText.text = $"{wfaData.workForceAllocation}";
            
            //put in istuff
            
            //uiData.wfCostText.enabled = true;
            //uiData.recCostText.enabled = true;
        }
        else if (tileData.workForceRecruit.ContainsKey(clickData.clickedCell))
        {
            //UI SCRIPT
            uiData.stateText.text = $"{$"This Factory is Recruiting "}";

        }
        else
        {
            //UI SCRIPT
            uiData.stateText.text = $"{$"Already Producing"}";

        }

        EventSystem.current.currentSelectedGameObject.SetActive(false);
        uiData.SetUpCost();
    }


    public void Recruit()
    {


        if (tileData.workForceRecruit.ContainsKey(clickData.clickedCell) == false && tileData.workForceProduction.ContainsKey(clickData.clickedCell) == false)
        {

            curProcedure = EventSystem.current.currentSelectedGameObject;


            uiData.buttonStartProcedure.SetActive(true);
            uiData.workForceNoText.text = $"{wfaData.workForceAllocation}";

        }
        else if (tileData.workForceProduction.ContainsKey(clickData.clickedCell))
        {
            uiData.stateText.text = $"{$"This Factory is in Produciton"}";

        }
        else
        {
            uiData.stateText.text = $"{$"Already Recruiting"}";

        }



        uiData.buttonProduce.SetActive(false);
        uiData.SetUpCost();

    }


    public void Gather()
    {
        if (tileData.workForceGather.ContainsKey(clickData.clickedCell) == false)
        {

            curProcedure = EventSystem.current.currentSelectedGameObject;


            uiData.buttonStartProcedure.SetActive(true);
            uiData.workForceNoText.text = $"{wfaData.workForceAllocation}";


        }
        else
        {
            uiData.stateText.text = $"{$"already Gathering"}";

        }


        EventSystem.current.currentSelectedGameObject.SetActive(false);
        uiData.SetUpCost();
    }

    public void Expodition()
    {

        TileBase stateTile = clickData.tile;

        ExpoditionCheck(stateTile);

        EventSystem.current.currentSelectedGameObject.SetActive(false);
        clickData.hoverData.click = false;
    }

    public void ExpoditionCheck(TileBase stateTile)
    {

        //checks if player has enough resources for expodition
        if (playerData.money >= 10)
        {
            playerData.money -= 10;

            playerData.statesUnlocked++;

            // checks on the grid for rule tiles and sets them to land tiles
            for (int x = 2; x < tileData.gridX; x++)
            {
                for (int y = 1; y < tileData.gridY; y++)
                {
                    Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                    TileBase nowTile = tilemap.GetTile(nowTilePos);

                    if (nowTile == stateTile)
                    {
                        switch (nowTile)
                        {



                            case NewYorkTile:

                                if (tileData.recStatePos.ContainsKey(nowTile.GetType()))
                                {

                                    if (tileData.recStatePos[nowTile.GetType()].Contains(nowTilePos) == true)
                                    {
                                        //Adding what state the rec tiles are in
                                        tileData.recStateData.Add(nowTilePos, nowTile);
                                        //Adding the amount of recourses for newly added rec tiles
                                        tileData.recData.Add(nowTilePos, 30);

                                        tilemap.SetTile(nowTilePos, tileData.recTile);


                                    }
                                    else
                                    {

                                        //change state locked tile to land
                                        tilemap.SetTile(nowTilePos, tileData.nyLand);
                                    }
                                }



                                break;


                            case WyomingTile:

                                if (tileData.recStatePos.ContainsKey(nowTile.GetType()))
                                {

                                    if (tileData.recStatePos[nowTile.GetType()].Contains(nowTilePos) == true)
                                    {
                                        //Adding what state the rec tiles are in
                                        tileData.recStateData.Add(nowTilePos, nowTile);
                                        //Adding the amount of recourses for newly added rec tiles
                                        tileData.recData.Add(nowTilePos, 30);

                                        tilemap.SetTile(nowTilePos, tileData.recTile);


                                    }
                                    else
                                    {

                                        //change state locked tile to land
                                        tilemap.SetTile(nowTilePos, tileData.wyoLand);
                                    }
                                }

                                break;

                        }

                        //UI SCRIPT
                        //stateText.text = $"{$"A new state has been discovered"}";

                    }
                }

            }
        }


    }
}
