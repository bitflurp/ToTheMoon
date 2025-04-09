using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Array for the Type of tiles
    //SWITCH NAMES OF TYLE TYPES ARRAY FOR THE LOVE OF GOD
    public tileTypes[] tileTypes;

    //Array for tile location
    public int[,] tiles;

    //Private Vars (serialized for ease of use) of grid size (X,Y)
    [SerializeField] private int gridX, gridY;

    private void Start()
    {
        //Generate that data baby
        generateGridData();

        //Generate The visuals by spawing those prefabs baby
        generateGridVisuals();
    }

    private void generateGridData()
    {
        //allocate Grid Tiles
        tiles = new int[gridX, gridY];


        // Initialize our Grid Tiles to "Land"
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                // set every tile to be land
                tiles[x, y] = 0;

            }

        }

        // tile on location 1,1 should be locked
        tiles[1, 1] = 1;


    }



    private void generateGridVisuals()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                tileTypes tt = tileTypes[tiles[x, y]];

               GameObject curTile  = (GameObject)Instantiate(tt.TileSpritePrefab, new Vector3(x, y, 0), Quaternion.identity);

                TileInteraction tI = curTile.GetComponent<TileInteraction>();

                tI.posX = x;
                tI.posY = y;

               

            }

        }


    }




}
