using UnityEngine;
using UnityEngine.Tilemaps;

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

            curRec = curRec - 5;

            tileData.rec = curRec;

        }
        else
        {

            Debug.Log($"NO DOUGH" + curRec);

        }


    }


    public void Production()
    {
        curProdCount++;
        tileData.productionCounter = curProdCount;
        

    }


}
