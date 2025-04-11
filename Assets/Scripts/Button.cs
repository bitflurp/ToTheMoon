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

      
      
        if (curRec >= 5)
        {         //On click Sets current tile to Factory tile
            tilemap.SetTile(currentCell, factoryTile);
              //closes popup after creating factory
            button.SetActive(false);
            curRec = curRec - 5;

            tileData.rec = curRec;

        }
        else
        {

            Debug.Log($"NO DOUGH" + curRec);
              //closes popup after no dough
             button.SetActive(false);
        }


    }

    public void ClosePopup()
    {
        //closes popup
         button.SetActive(false);
    }
}
