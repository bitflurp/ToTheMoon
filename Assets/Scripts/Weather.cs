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
using Random = UnityEngine.Random;

public class Weather : MonoBehaviour
{



    private Turns turnData;
    private Tilemap tilemap;
    private TileData tileData;
    private PlayerData playerData;

    private int dayToRemove;


    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        turnData = GetComponent<Turns>();
        tileData = GetComponent<TileData>();
        playerData = GetComponent<PlayerData>();
    }






    public void WeatherApply()
    {

        int chanceWeather = 3; //Random.Range(1, 4);


        //Checks if weather Hits and if its the day to remove Weather so as to not constantly be in WEATHER STATE
        if (chanceWeather == 3 && tileData.weatherData.Count() == 0)
        {

            //Makes it so day the weather gets removes is the next turn
            dayToRemove = turnData.dayCounter + 1;

            //Randomizes which state to effect
            int chanceStateWeather = Random.Range(1, playerData.statesUnlocked);
            Debug.Log($"{chanceStateWeather}");




            for (int x = 2; x < tileData.gridX; x++)
            {
                for (int y = 1; y < tileData.gridY; y++)
                {


                    Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                    TileBase nowTile = tilemap.GetTile(nowTilePos);




                    //Checks if current tile is factory, and if it is gets the state tile it was placed on
                    tileData.factoryStateData.TryGetValue(nowTilePos, out TileBase factoryState);
                    tileData.recStateData.TryGetValue(nowTilePos, out TileBase recState);


                    if (!(nowTile is PaleTile)) {

                        //Effects only chosen weather
                        switch (chanceStateWeather)
                        {




                            //if New York got selected
                            case 1:


                                if (nowTile is NewYorkLand || factoryState is NewYorkLand || recState is NewYorkTile)
                                {

                                    tileData.weatherData.Add(nowTilePos, nowTile);

                                    tilemap.SetTile(nowTilePos, null);

                                    // Debug.Log($"Bad weather has made {nowTile} unworkable");
                                }





                                break;

                            //If Wyoming Got selected
                            case 2:



                                if (nowTile is WyomingLand || factoryState is WyomingLand || recState is WyomingTile)
                                {

                                    tileData.weatherData.Add(nowTilePos, nowTile);

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
    }


    public void WeatherRemove()
    {

        if (dayToRemove == turnData.dayCounter)
        {


            for (int i = tileData.weatherData.Count - 1; i >= 0; i--)
            {
                var item = tileData.weatherData.ElementAt(i);
                var itemKey = item.Key;
                var itemValue = item.Value;


                tilemap.SetTile(itemKey, itemValue);

                tileData.weatherData.Remove(itemKey);


            }

        }

    }

}
