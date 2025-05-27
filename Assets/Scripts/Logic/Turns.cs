using UnityEngine;
using UnityEngine.Tilemaps;

public class Turns : MonoBehaviour
{


    private Profits profitData;
    private PlayerData playerData;
    private UserInterface uiData;
    private Weather weatherData;
    private Pale paleData;
    private ProfitScreen psData;
    private TilemapControls clickData;
    private Procedures procedureData;
    private Tilemap tilemap;
    private TileData tileData;
   
    public int dayCounter = 1;
    public int nextQuota = 8;
    public int weatherIndex = -1;

    public int quota = 10;
    private Coroutine profitCo;

    private void Start()
    {
        profitData = GetComponent<Profits>();
        playerData = GetComponent<PlayerData>();
        uiData = GetComponent<UserInterface>();
        weatherData = GetComponent<Weather>();
        paleData = GetComponent<Pale>();
        psData = GetComponent<ProfitScreen>();
        clickData = GetComponent<TilemapControls>();
        procedureData = GetComponent<Procedures>();
        tilemap = GetComponent<Tilemap>();
        tileData = GetComponent<TileData>();

        //test Delete after 

        for (int i = 0; i < weatherData.weatherForecast.GetLength(0); i++)
        {

            Debug.Log($"{weatherData.weatherForecast[i, 0]} {weatherData.weatherForecast[i, 1]} {weatherData.weatherForecast[i, 2]}");

        }

    }


    public void EndTurn()
    {

        uiData.RemoveUI();
        clickData.hoverData.click = false;

        //Increment Turn
        dayCounter++;
        weatherIndex++;


        weatherData.WeatherApply();
        weatherData.WeatherRemove();

        profitData.StallCheck();


        //GetProfit
        profitData.ProductionProfit();
        profitData.GatherProfit();

        
        if(dayCounter != nextQuota)
        {
           profitCo = StartCoroutine(psData.ProfitAnim());  
         }

        paleData.PaleFunc();

        if (procedureData.factoryCounter >= 5) {

            tilemap.SetTile(new Vector3Int(11, 10, 0), tileData.paleTile);
        
        
        }

   
       

        if (dayCounter == nextQuota)
        {

            QuotaReach();
            //changes deadline to next week (have to add it into a fail/win state 
            nextQuota += 7;
            //weather index back to -1
            weatherIndex = -1;

            weatherData.WeatherForecast();
            uiData.ForecastTranslate(); 
        }






        StartTurn();
    }


    public void QuotaReach()
    {

        if (playerData.money >= quota)
        {
            quota = quota + 10;
            profitCo = StartCoroutine(psData.ProfitAnim());  
            uiData.stateText.text = $"{$"QUOTA REACHED"}";
            
        }
        else
        {
            profitCo = StartCoroutine(psData.LoseScreen());  
            uiData.stateText.text = $"{$"QUOTA FAILED"}";

        }

    }

    public void StartTurn()
    {



        uiData.dayText.text = $"{$"DAY {dayCounter}"}";

    }








}
