using UnityEngine;
using UnityEngine.Tilemaps;

public class WeatherSystem : MonoBehaviour
{
    private int gridX, gridY = 12;
    public LandTile landtile;

    private Tilemap tilemap;

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void WeatherCheck() {

        for (int x = 2; x < gridX; x++)
        {
            for (int y = 1; y < gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (nowTile is NewYorkLand)
                {


                    tilemap.SetTile(nowTilePos, landtile);





                }

                   

                }
            }

        }
    


}


