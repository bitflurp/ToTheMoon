using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{


    private PlayerData playerData;
    private WFA wfaData;
    
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

    public TMP_Text workForceNoText;
    public TMP_Text workForceCostText;




    private void Start()
    {
        playerData = GetComponent<PlayerData>();
        wfaData = GetComponent<WFA>();
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

    }



}
