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
public class TileData : MonoBehaviour
{
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

    //Grid data
    public int gridX = 12;
    public int gridY = 12;



    //Dictionaries
    public Dictionary<Vector3Int, TileBase> factoryStateData = new();
    public Dictionary<Vector3Int, TileBase> recStateData = new();
    public Dictionary<Vector3Int, TileBase> weatherData = new();


    public Dictionary<Vector3Int, int> stallData = new();
    public Dictionary<Vector3Int, int> recData = new();


    public Dictionary<Vector3Int, int> workForceProduction = new();
    public Dictionary<Vector3Int, int> workForceRecruit = new();
    public Dictionary<Vector3Int, int> workForceGather = new();


    public Dictionary<Type, List<Vector3Int>> recStatePos = new() {

        {typeof(NewYorkTile) , new List<Vector3Int>{new Vector3Int(5,1,0), new Vector3Int(6,1,0) } } ,


        {typeof(WyomingTile) , new List<Vector3Int>{new Vector3Int(9,1,0), new Vector3Int(10,1,0) , new Vector3Int(11,1,0) }  },

    };



   
}
