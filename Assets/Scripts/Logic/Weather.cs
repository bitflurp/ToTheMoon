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

    public int[,] weatherForecast = {
    {2,0,0},
    {3,0,0},
    {4,0,0},
    {5,0,0},
    {6,0,0},
    {7,0,0},
    {8,0,0}
    };
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        turnData = GetComponent<Turns>();
        tileData = GetComponent<TileData>();
        playerData = GetComponent<PlayerData>();
    }




    public void WeatherApply()
    {

        //int chanceWeather = 3; //Random.Range(1, 4);


        //Checks if weather Hits and if its the day to remove Weather so as to not constantly be in WEATHER STATE
        if (weatherForecast[turnData.weatherIndex, 1] == 3 && tileData.weatherData.Count() == 0)
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


                    if (!(nowTile is PaleTile))
                    {

                        //Effects only chosen weather
                        switch (weatherForecast[turnData.weatherIndex, 2])
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


    public void WeatherForecast()
    {

        int quotaWeather = weatherForecast[6, 1];


        for (int i = 0; i < weatherForecast.GetLength(0); i++)
        {

            //Randomize each loop
            int weatherHit = Random.Range(1, 4);
            int stateHit = Random.Range(1, playerData.statesUnlocked);

            //day set
            weatherForecast[i, 0] += 7;

            //weatherForecast[i, 1] = 3;//weatherHit;
            //weather hit set
            //Checking if the previous day had weather so that two weather days does not happen   
            if (i == 0)
            {
                if (quotaWeather != 3 && playerData.statesUnlocked > 1)
                {
                    weatherForecast[i, 1] = 3;//weatherHit;
                }
                else
                {
                    weatherForecast[i, 1] = 0;//weatherHit;
                }

            }
            else
            {

                if (weatherForecast[i - 1, 1] != 3 && playerData.statesUnlocked > 1)
                {
                    weatherForecast[i, 1] = 3;//weatherHit
                }
                else
                {
                    weatherForecast[i, 1] = 0;//weatherHit;
                }


            }

            //state set
            if (weatherForecast[i, 1] == 3 && playerData.statesUnlocked > 1)
            {
                weatherForecast[i, 2] = stateHit;
            }
            else
            {
                weatherForecast[i, 2] = 0;
            }


        }


        for (int i = 0; i < weatherForecast.GetLength(0); i++)
        {

            Debug.Log($"{weatherForecast[i, 0]} {weatherForecast[i, 1]} {weatherForecast[i, 2]}");


        }

    }






}