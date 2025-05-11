using UnityEngine;

public class Turns : MonoBehaviour
{


    private Profits profitData;
    private PlayerData playerData;
    private UserInterface uiData;
    private Weather weatherData;
    private Pale paleData;
   
    public int dayCounter = 1;
    public int nextQuota = 8;



    private void Start()
    {
        profitData = GetComponent<Profits>();
        playerData = GetComponent<PlayerData>();
        uiData = GetComponent<UserInterface>();
        weatherData = GetComponent<Weather>();
        paleData = GetComponent<Pale>();
    }


    public void EndTurn()
    {

        uiData.RemoveUI();


        //Increment Turn
        dayCounter++;



       // weatherData.WeatherApply();
        //weatherData.WeatherRemove();

        profitData.StallCheck();


        //GetProfit
        profitData.ProductionProfit();
        profitData.GatherProfit();



        paleData.PaleFunc();
       

        if (dayCounter == nextQuota)
        {

            QuotaReach();
            //changes deadline to next week (have to add it into a fail/win state 
            nextQuota += 7;
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
