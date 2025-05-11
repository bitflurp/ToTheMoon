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
using UnityEditor;

public class Profits : MonoBehaviour
{


    private Tilemap tilemap;
    private PlayerData playerData;
    private TilemapControls clickData;
    private TileData tileData;

    private Turns turnData;
    //turnData = GetComponent<Turns>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        playerData = GetComponent<PlayerData>();
        clickData = GetComponent<TilemapControls>();
        tileData = GetComponent<TileData>();  
         turnData = GetComponent<Turns>();
    }



    public void ProductionProfit()
    {
        int addMoney = 0;
        int totalWF = 0;

        for (int i = tileData.workForceProduction.Count - 1; i >= 0; i--)
        {
            var item = tileData.workForceProduction.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            totalWF += itemValue;

            //For modifier Uncomment below and comment Addmoney + totalWF...
            //addMoney += itemValue * itemValue;
            tileData.workForceProduction.Remove(itemKey);

        }

        ///For Randomized Profits (Commented cause its hard to playtest with it)
        //for (int i = productionCounter; i > 0; i--)
        //{
        // int randomProfit = Random.Range(1, 5);

        //  addMoney += randomProfit;
        //Debug.Log($"{randomProfit} was added: now is {addMoney}");
        //}



        // Multiply Factories producing by product per factory producing (2)
        addMoney = totalWF * 2;

        //add profit Recourse to player Recourse 
        playerData.money += addMoney;

        //workforce Resets
        playerData.workForce += totalWF;


    }

    public void GatherProfit()
    {
        int wfTBA = 0;
        int totalWF = 0;

        for (int i = tileData.workForceGather.Count - 1; i >= 0; i--)
        {
            var item = tileData.workForceGather.ElementAt(i);
            var itemKey = item.Key;
            var itemValue = item.Value;

            totalWF+= itemValue;
            wfTBA += itemValue;
            //Removes the gathered recourse from recourse tile
            tileData.recData[itemKey] -= itemValue * 2;


            //Check if the recourse tile is depleted
            if (tileData.recData[itemKey] <= 0)
            {
                //changes tile
                tilemap.SetTile(itemKey, tileData.landTile);

                //makes sure that player doesnt profit more recourse then there is availible
                totalWF += tileData.recData[itemKey] / 2;

                //Removes tile from dictionary
                tileData.recData.Remove(itemKey);

            }

            tileData.workForceGather.Remove(itemKey);

        }



        // Multiply Factories producing by product per factory producing (2)
        int addRec = totalWF * 2;

        //add profit Recourse to player Recourse 
        playerData.rec += addRec;

        playerData.workForce += wfTBA;


    }


    public void StallCheck()
    {

        int totalWF = 0;

        for (int x = 1; x < tileData.gridX; x++)
        {
            for (int y = 1; y < tileData.gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (tileData.stallData.ContainsKey(nowTilePos))
                {
                    if (tileData.stallData[nowTilePos] == turnData.dayCounter)
                    {

                        totalWF += tileData.workForceRecruit[nowTilePos];





                        // Multiply Factories producing by product per factory producing (2)
                        int addWorkForce = totalWF * 1;

                        //add profit Recourse to player Recourse 
                        playerData.workForce += addWorkForce;

                        //Resets workforce
                        playerData.workForce += totalWF;

                        // Reset Factory Production 
                        //recruitCounter = 0;






                        
                        //Removesdata from dictionary
                        tileData.stallData.Remove(nowTilePos);

                        //removes workforce data from dictionary
                        tileData.workForceRecruit.Remove(nowTilePos);



                        //If stall is unstalled during a turn where the tile is weathered
                        //it will check if the current tile is the state which is weathered through
                        //weatherData.ContainsValue(factoryStateData[nowTilePos])
                        // and if it is will set the tile to weathered" and add that tile to weatherdata
                        //so as weather stops , all effected state tiles will be set back to their og state
                        if (tileData.weatherData.ContainsValue(tileData.factoryStateData[nowTilePos]))
                        {

                            tilemap.SetTile(nowTilePos, null);
                            tileData.weatherData[nowTilePos] = tileData.factoryTile;

                        }
                        else
                        {

                            //change stall Tile to Facotry
                            tilemap.SetTile(nowTilePos, tileData.factoryTile);

                        }


                        Debug.Log($"Has Unstalled");
                    }
                }
            }
        }




    }

}