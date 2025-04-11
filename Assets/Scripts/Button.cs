using UnityEngine;
using UnityEngine.Tilemaps;

public class Button : MonoBehaviour
{

    
    private Tilemap tilemap;
    public FactoryTile factoryTile;

    //Vars to Get Cell data from Tilemap controls
    public Vector3Int currentCell;
    public TileBase currentTile;


    
    void Start()
    {
       //Reference Tilemap class to get setTile  
        tilemap = GetComponent<Tilemap>();
    }

    public void CreateFactory()
    {

        //Debug.Log($"I am" + currentCell + currentTile);

        //On click Sets current tile to Factory tile
        tilemap.SetTile(currentCell,factoryTile);

    }

}
