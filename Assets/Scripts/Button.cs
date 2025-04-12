using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;



public class Button : MonoBehaviour
{


    private Tilemap tilemap;
    private TilemapControls tileData;


    public FactoryTile factoryTile;

    //Vars to Get Cell data from Tilemap controls
    public Vector3Int currentCell;
    public TileBase currentTile;

    public int curRec;
    public int curProdCount;

    public GameObject buttonCreate;

    //Lists

    List<Vector3Int> isProducing = new List<Vector3Int>();

    void Start()
    {
        //Reference Tilemap class to get setTile  
        tilemap = GetComponent<Tilemap>();

        tileData = GetComponent<TilemapControls>();


    }

    public void CreateFactory()
    {

        //Debug.Log($"I am" + currentCell + currentTile);

        if (curRec >= 5)
        {         //On click Sets current tile to Factory tile
            tilemap.SetTile(currentCell, factoryTile);

            //closes popup after creating factory
            buttonCreate.SetActive(false);

            curRec = curRec - 5;

            tileData.rec = curRec;

        }
        else
        {

            Debug.Log($"NO DOUGH" + curRec);

            //closes popup after no dough
            buttonCreate.SetActive(false);
        }


    }

    public void ClosePopup()
    {
        //closes popup
        buttonCreate.SetActive(false);
    }

    public void Production()
    {
        if (isProducing.Contains(currentCell) == false)
        {
           
            curProdCount++;
            tileData.productionCounter = curProdCount;
            isProducing.Add(currentCell);

        }
        else
        {
            //comment
            Debug.Log($"already Producing");

        }


    }

    public void OnEndDay()
    {

        isProducing.Clear();

    }





}