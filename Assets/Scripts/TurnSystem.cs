using UnityEditorInternal;
using UnityEngine;


public enum turnState {start, end }
public class TurnSystem : MonoBehaviour
{
    public turnState state;
    public int turnCounter = 1;

    private TilemapControls tileData;
    
    //Procedure Vars
    private int endProduction;
    private int endGather;
    private int endRecruit;

    //RevourseVars
    private int addRec;
    private int curRecEnd;

    private int addMoney;
    private int curMoneyEnd;

    private int addWorkForce;
    private int CurWorkForceEnd;

    void Start()
    {
        state = turnState.start;
        Debug.Log($"DAY" + turnCounter);
        tileData = GetComponent<TilemapControls>();
    }

    public void EndTurn()
    {
        //Increment Turn
        turnCounter++;

        //GetProfit
        ProductionProfit();
        GatherProfit();
        RecruitProfit();

        if (turnCounter == 8)
        {

            QuotaReach();

        }




        StartTurn();
    }




    public void ProductionProfit()
    {

        // Get data from tilemapControls
        endProduction = tileData.productionCounter;
        curMoneyEnd = tileData.money;

        //Check how many factories are in production
        if (endProduction > 0)
        {
            Debug.Log($"PRODUCE" + endProduction);

            // Multiply Factories producing by product per factory producing (2)
            addMoney = endProduction * 2;

            //add profit Recourse to player Recourse 
            curMoneyEnd = curMoneyEnd + addMoney;

            // Reset Factory Production 
            endProduction = 0;


        }
        else
        {
            Debug.Log("no prodice");
        }


    }

    public void GatherProfit() {


        // Get data from tilemapControls
        endGather = tileData.gatherCounter;
        curRecEnd = tileData.rec;

        //Check how many factories are in production
        if (endGather > 0)
        {
            Debug.Log($"GATHER" + endProduction);

            // Multiply Factories producing by product per factory producing (2)
            addRec = endGather * 2;

            //add profit Recourse to player Recourse 
            curRecEnd = curRecEnd + addRec;

            // Reset Factory Production 
            endGather = 0;


        }
        else
        {
            Debug.Log("no prodice");
        }




    }


    public void RecruitProfit()
    {


        // Get data from tilemapControls
        endRecruit = tileData.recruitCounter;
        CurWorkForceEnd = tileData.workForce;

        //Check how many factories are in production
        if (endRecruit > 0)
        {
            Debug.Log($"Recruit" + endRecruit);

            // Multiply Factories producing by product per factory producing (2)
            addWorkForce = endRecruit * 1;

            //add profit Recourse to player Recourse 
            CurWorkForceEnd = CurWorkForceEnd + addWorkForce;

            // Reset Factory Production 
            endRecruit = 0;


        }
        else
        {
            Debug.Log("no prodice");
        }




    }
    public void StartTurn()
    {
        //Send Data to player
        //Production Profit
        tileData.productionCounter = endProduction;
        tileData.money = curMoneyEnd;

        //Gather Profit
        tileData.gatherCounter = endGather;
        tileData.rec = curRecEnd;

        //Recruit Profit
        tileData.recruitCounter = endRecruit;
        tileData.workForce = CurWorkForceEnd;


        Debug.Log($"DAY" + turnCounter);

    }


    public void QuotaReach()
    {

        if (curMoneyEnd >= 30)
        {

            Debug.Log($"QUOTA REACHED");

        }
        else
        {

            Debug.Log($"QUOTA FAILED");

        }

    }



}
