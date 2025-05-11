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

public class TilemapControls : MonoBehaviour
{
    //TileData Vars
    public Vector3Int clickedCell;
    public TileBase tile;

    //Classes
    private Tilemap tilemap;
    private PlayerData playerData;
    private UserInterface uiData;
    private Turns turnData;
    private TileData tileData;
   

  
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        playerData = GetComponent<PlayerData>();
        uiData = GetComponent<UserInterface>();
        turnData = GetComponent<Turns>();
        tileData = GetComponent<TileData>();



        RecourceAdd();

        uiData.dayText.text = $"{$"DAY {turnData.dayCounter}"}";


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
        uiData.stateText.text = $"{$"TILE INFO" + clickedCell + tile + playerData.rec}";


        switch (tile) {

            case LandTile:

                uiData.RemoveUI();

                //Makes Button visible and sends it to the cell position
                uiData.buttonCreate.SetActive(true);





                break;


            case FactoryTile:

                uiData.RemoveUI();

                uiData.buttonCreate.SetActive(false);

                //Makes Button visible and sends it to the cell position
                uiData.buttonProduce.SetActive(true);

                if (tileData.workForceRecruit.ContainsKey(clickedCell))
                {

                    uiData.stateText.text = $"{$"there is {tileData.workForceRecruit[clickedCell]} Recruiting"}";

                }
                else if (tileData.workForceProduction.ContainsKey(clickedCell))
                {

                    uiData.stateText.text = $"{$"there is {tileData.workForceProduction[clickedCell]} Producing"}";
                }

              

                break;


            case ResTile:

                uiData.RemoveUI();

                //Makes Button visible and sends it to the cell position
                uiData.buttonGather.SetActive(true);


                uiData.stateText.text = $"{tileData.recData[clickedCell]}/30 Recourses left";


                if (tileData.workForceGather.ContainsKey(clickedCell) )
                {
                    uiData.stateText.text = $"{$"there is {tileData.workForceGather[clickedCell]} Gathering"}";

                }


            

                break;


            case RuleTile:

                uiData.RemoveUI();

                uiData.buttonExpodition.SetActive(true);
                //stateText.text = $"{$"locked"}";

           
                break;
        
        
        
        
        
        }


    }





    public void RecourceAdd()
    {




        for (int x = 1; x < tileData.gridX; x++)
        {
            for (int y = 1; y < tileData.gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (nowTile is ResTile)
                {
                    if (!tileData.recData.ContainsKey(nowTilePos))
                    {
                        tileData.recData.Add(nowTilePos, 30);

                    }
                }
            }
        }





    }

}


