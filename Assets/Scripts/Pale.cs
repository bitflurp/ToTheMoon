using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using System.Data.SqlTypes;
using System;

public class Pale : MonoBehaviour
{


    private Turns turnData;
    private Tilemap tilemap;
    private TileData tileData;


    public List<Vector3Int> Juiced = new List<Vector3Int>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        turnData = GetComponent<Turns>();
        tileData = GetComponent<TileData>();
    }



    //PALE CODE
    public void PaleFunc()
    {

        List<Vector3Int> paleHit = new List<Vector3Int>();
        List<Vector3Int> paleEffect = new List<Vector3Int>();

       
        for (int x = 1; x < tileData.gridX; x++)
        {
            for (int y = 1; y < tileData.gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);
                //Debug.Log(nowTilePos);

                if (nowTile is PaleTile && Juiced.Contains(nowTilePos) == false)
                {




                    if (paleEffect.Contains(nowTilePos) == false)
                    {


                        Vector3Int N = new Vector3Int(x, y + 1, 0);
                        Vector3Int S = new Vector3Int(x, y - 1, 0);
                        Vector3Int W = new Vector3Int(x - 1, y, 0);
                        Vector3Int E = new Vector3Int(x + 1, y, 0);

                        paleHit.Add(N);

                        paleHit.Add(S);
                        paleHit.Add(W);

                        paleHit.Add(E);



                        Juiced.Add(nowTilePos);


                    }



                    for (int i = paleHit.Count - 1; i >= 0; i--)
                    {


                        var item = paleHit.ElementAt(i);
                        if (tilemap.GetTile(item) != null)
                        {


                            tilemap.SetTile(item, tileData.paleTile);
                            paleEffect.Add(item);
                        }






                    }

                }


            }


        }













    }












}
