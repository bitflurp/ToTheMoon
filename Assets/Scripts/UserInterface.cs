using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{


    private PlayerData playerData;
    private WFA wfaData;
    private Weather weatherData;

    //For Buttons
    public GameObject buttonCreate;
    public GameObject buttonProduce;
    public GameObject buttonRecruit;
    public GameObject buttonGather;
    public GameObject buttonExpodition;

    public GameObject buttonIncrement;
    public GameObject buttonReduce;
    public GameObject buttonStartProcedure;

    //for recource ui
    public TMP_Text recText;
    public TMP_Text workForceText;
    public TMP_Text moneyText;
    public TMP_Text stateText;
    public TMP_Text dayText;
    public TMP_Text weatherText;

    public TMP_Text workForceNoText;
    public TMP_Text workForceCostText;

    private string[,] forecastText = {
    {"Day 2","Null","Null"},
    {"Day 3","Null","Null"},
    {"Day 4","Null","Null"},
    {"Day 5","Null","Null"},
    {"Day 6","Null","Null"},
    {"Day 7","Null","Null"},
    {"Day 8","Null","Null"}
    };


    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        wfaData = GetComponent<WFA>();
        weatherData = GetComponent<Weather>();

        //test Delete after

        for (int i = 0; i < weatherData.weatherForecast.GetLength(0); i++)
        {

            Debug.Log($"{forecastText[i, 0]} {forecastText[i, 1]} {forecastText[i, 2]}");

        }
    }

    //EXTRA STUFF IGNORE
    public void RemoveUI()
    {
        buttonCreate.SetActive(false);
        buttonGather.SetActive(false);
        buttonProduce.SetActive(false);
        buttonStartProcedure.SetActive(false);
        buttonExpodition.SetActive(false);


    }


    void Update()
    {
        // shows Current Rec
        recText.text = $"REC: {playerData.rec}";

        workForceText.text = $"WF: {playerData.workForce}";

        moneyText.text = $"PROFIT: {playerData.money}";


        workForceCostText.text = $"Produce: {wfaData.workForceAllocation}$ :{wfaData.workForceAllocation} Rec \nRecruit: {wfaData.workForceAllocation * 2}$ : TURN STALL \nGather: {wfaData.workForceAllocation * 2}$";

        weatherText.text = $"{forecastText[0, 0]} {forecastText[0, 1]} {forecastText[0, 2]}\n " +
           $"{forecastText[1, 0]}{forecastText[1, 1]} {forecastText[1, 2]} \n" +
           $" {forecastText[2, 0]}{forecastText[2, 1]}{forecastText[2, 2]} \n" +
           $"{forecastText[3, 0]} {forecastText[3, 1]}{forecastText[3, 2]} \n" +
           $"{forecastText[4, 0]} {forecastText[4, 1]} {forecastText[4, 2]} \n" +
           $"{forecastText[5, 0]} {forecastText[5, 1]} {forecastText[5, 2]} \n" +
           $"{forecastText[6, 0]} {forecastText[6, 1]} {forecastText[6, 2]} \n";


    }


    public void ForecastTranslate()
    {

        for (int i = 0; i < weatherData.weatherForecast.GetLength(0); i++)
        {


            forecastText[i, 0] = $"Day {weatherData.weatherForecast[i, 0]}";

            switch (weatherData.weatherForecast[i, 1])
            {

                case 3:
                    forecastText[i, 1] = "WEATHER";
                    break;
                default:
                    forecastText[i, 1] = "Null";
                    break;
            }

            if (weatherData.weatherForecast[i, 1] == 3)
            {

                switch (weatherData.weatherForecast[i, 2])
                {

                    case 1:

                        forecastText[i, 2] = "New York";
                        break;

                    case 2:

                        forecastText[i, 2] = "Wyoming";
                        break;


                }


            }
            else
            {

                forecastText[i, 2] = "Null";


            }

        }


        for (int i = 0; i < weatherData.weatherForecast.GetLength(0); i++)
        {

            Debug.Log($"{forecastText[i, 0]} {forecastText[i, 1]} {forecastText[i, 2]}");

        }

       
           

    }


}