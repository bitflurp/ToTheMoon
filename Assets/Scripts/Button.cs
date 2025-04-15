using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;



public class Button : MonoBehaviour
{


    private Tilemap tilemap;
    private TilemapControls tileData;
    private int gridX = 12;
    private int gridY = 12;

    public FactoryTile factoryTile;
    public LandTile landTile;



    //Vars to Get Cell data from Tilemap controls
    public Vector3Int currentCell;
    public TileBase currentTile;

    //Vars For Recourses
    public int curRec;
    public int curWorkForce;
    public int curMoney;

    //Vars for Procedures 
    public int curProdCount;
    public int curGatherCount;
    public int curRecruitCount;

    public GameObject buttonCreate;
    public GameObject buttonProduce;
    public GameObject buttonExpodition;



    //Lists

    List<Vector3Int> isProducing = new List<Vector3Int>();
    List<Vector3Int> isGathering = new List<Vector3Int>();
    List<Vector3Int> isRecruiting = new List<Vector3Int>();

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



    public void Production()
    {
        if (isProducing.Contains(currentCell) == false && isRecruiting.Contains(currentCell) == false)
        {

            curProdCount++;
            tileData.productionCounter = curProdCount;
            isProducing.Add(currentCell);
            Debug.Log($"there is" + curProdCount + " Producing");
        }
        else if (isRecruiting.Contains(currentCell) == true)
        {

            Debug.Log($"This Factory is Recruiting ");

        }
        else
        {

            Debug.Log($"Already Producing");

        }

        buttonProduce.SetActive(false);

    }

    public void Gather()
    {
        if (isGathering.Contains(currentCell) == false)
        {

            curGatherCount++;
            tileData.gatherCounter = curGatherCount;
            isGathering.Add(currentCell);

            Debug.Log($"there is" + curGatherCount + " Gathering");
        }
        else
        {
            //comment
            Debug.Log($"already Gathering");

        }




    }


    public void Recruit()
    {
        if (isRecruiting.Contains(currentCell) == false && isProducing.Contains(currentCell) == false)
        {


            curRecruitCount++;
            tileData.recruitCounter = curRecruitCount;
            isRecruiting.Add(currentCell);

            Debug.Log($"there is" + curRecruitCount + " Recruiting");
        }
        else if (isProducing.Contains(currentCell) == true)
        {

            Debug.Log($"This Factory is in Produciton");

        }
        else
        {

            Debug.Log($"Already Recruiting");

        }



        buttonProduce.SetActive(false);


    }
    public void Expodition()
    {
        //checks if player has enough resources for expodition
        if(curMoney >= 10){
            curMoney -= 10;
              tileData.money = curMoney;
            // checks on the grid for rule tiles and sets them to land tiles
            for (int x = 0; x < gridX; x++)
                    {
                        for (int y = 0; y < gridY; y++)
                        {
                            Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                            TileBase nowTile = tilemap.GetTile(nowTilePos);
                        
                            if (nowTile is RuleTile)
                            {
                                //change state locked tile to land
                                tilemap.SetTile(nowTilePos, landTile);

                                Debug.Log($"Expodion is doing");
                            }
                        }

                    }
        }
       

    }
    public void ClosePopup()
    {
        //closes popup
        buttonCreate.SetActive(false);
    }
    public void OnEndDay()
    {

        isProducing.Clear();
        isGathering.Clear();
        isRecruiting.Clear();

    }





}