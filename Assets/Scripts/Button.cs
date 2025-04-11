using UnityEngine;
using UnityEngine.Tilemaps;

public class Button : MonoBehaviour
{

    
    private Tilemap tilemap;
    private TilemapControls tileData;
    public FactoryTile factoryTile;
    public GameObject button;

    //Vars to Get Cell data from Tilemap controls
    public Vector3Int currentCell;
    public TileBase currentTile;

    public int curRec;



    void Start()
    {
        //Reference Tilemap class to get setTile  
        tilemap = GetComponent<Tilemap>();

        tileData = GetComponent<TilemapControls>();
    }

    public void CreateFactory()
    {

        //Debug.Log($"I am" + currentCell + currentTile);

<<<<<<< HEAD
        //On click Sets current tile to Factory tile
        tilemap.SetTile(currentCell,factoryTile);
        //closes popup after creating factory
         button.SetActive(false);
=======
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


>>>>>>> c4372977ed3e000d84db78d2d24f17a79a837471
    }

    public void ClosePopup()
    {
        //closes popup
         button.SetActive(false);
    }
}
