using UnityEngine;

public class Turns : MonoBehaviour
{


    private Profits profitData;
    private PlayerData playerData;
    private UserInterface uiData;
    private Weather weatherData;
    private Pale paleData;
    private ProfitScreen psData;
    private TilemapControls clickData;
   
    public int dayCounter = 1;
    public int nextQuota = 8;
    public int weatherIndex = -1;

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

       // profitCo = StartCoroutine(psData.ProfitAnim());  



        paleData.PaleFunc();
       

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

        if (playerData.money >= 30)
        {

            uiData.stateText.text = $"{$"QUOTA REACHED"}";

        }
        else
        {

            uiData.stateText.text = $"{$"QUOTA FAILED"}";

        }

    }

    public void StartTurn()
    {



        uiData.dayText.text = $"{$"DAY {dayCounter}"}";

    }








}
