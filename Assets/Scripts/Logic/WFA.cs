using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class WFA : MonoBehaviour
{

    private TileData tileData;
    private PlayerData playerData;
    private TilemapControls clickData;
    private Procedures procedureData;
    private Tilemap tilemap;
    private UserInterface uiData;
    private Turns turnData;
   

    public int workForceAllocation = 1;
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        playerData = GetComponent<PlayerData>();
        tileData = GetComponent<TileData>();
        procedureData = GetComponent<Procedures>();
        clickData = GetComponent<TilemapControls>();
        uiData = GetComponent<UserInterface>();
        turnData = GetComponent<Turns>();
    }




    public void WFAProduction()
    {

        int whenStallEnds = turnData.dayCounter + 2;



        switch (true)
        {

            // WFA on Production
            case true when procedureData.curProcedure == uiData.buttonProduce:

                if (playerData.workForce >= workForceAllocation && playerData.rec >= workForceAllocation)
                {

                    tileData.workForceProduction.Add(clickData.clickedCell, workForceAllocation);

                    //costs
                    playerData.workForce -= workForceAllocation;
                    playerData.money -= workForceAllocation;
                    playerData.rec -= workForceAllocation;
                    DebtCheck();
                }
                else
                {

                    uiData.stateText.text = $"{$"You don't have enough recources"}";

                }
                break;


            //WFA on Recruit
            case true when procedureData.curProcedure == uiData.buttonRecruit:

                if (playerData.workForce >= workForceAllocation)
                {



                    // add location of current cell + when the stall will end for that specific cell
                    tileData.stallData.Add(clickData.clickedCell, whenStallEnds);

                    tileData.workForceRecruit.Add(clickData.clickedCell, workForceAllocation);

                    //costs
                    playerData.workForce -= workForceAllocation;
                    playerData.money -= workForceAllocation * 2;
                    DebtCheck();


                    //Set tile to stall tile
                    tilemap.SetTile(clickData.clickedCell, tileData.stallTile);

                    Debug.Log($"there is {tileData.workForceRecruit[clickData.clickedCell]} Recruiting");
                }
                else
                {

                    uiData.stateText.text = $"{$"You don't have enough recources"}";

                }


                break;

            //WFA on Gather
            case true when procedureData.curProcedure == uiData.buttonGather:

                if (playerData.workForce >= workForceAllocation)
                {


                    tileData.workForceGather.Add(clickData.clickedCell, workForceAllocation);

                    //Costs
                    playerData.workForce -= workForceAllocation;
                    playerData.money -= workForceAllocation * 2;
                    DebtCheck();

                    Debug.Log($"there is {tileData.workForceGather[clickData.clickedCell]} Gathering");
                }
                else
                {

                    uiData.stateText.text = $"{$"You don't have enough recources"}";

                }


                break;






        }

        // set WFA to 1 so on next procedure it starts on 1 WF chosen
        workForceAllocation = 1;
        uiData.RemoveUI();
        clickData.hoverData.click = false;
    }

    public void WFAChoice()
    {


        // Gets Current Button Pressed and puts it into curButton Temp Var
        GameObject curButton = EventSystem.current.currentSelectedGameObject;


        switch (true)
        {

            //If button is Increment: Increments WFA + Checks for MAX
            case true when curButton == uiData.buttonIncrement:
                if (workForceAllocation < 5) //&& workForceAllocation <  recData[clickedCell]/2 )
                {
                    workForceAllocation++;
                    uiData.workForceNoText.text = $"{workForceAllocation}";
                }
                else
                {

                    uiData.stateText.text = $"{$"Maximum WorkForce limit Reached"}";

                }
                break;

            //if Button is Reduce: Reduces WFA + Checks for MIN
            case true when curButton == uiData.buttonReduce:
                if (workForceAllocation > 1)
                {
                    workForceAllocation--;
                    uiData.workForceNoText.text = $"{workForceAllocation}";
                }
                else
                {

                    uiData.stateText.text = $"{$"Cannot Have 0 WorkForce Allocated"}";

                }


                break;

        }

        //DO costs here
        uiData.SetUpCost();


    }








//TEMP POSITION
    public void DebtCheck()
    {

        if (playerData.money < -100)
        {

            gameObject.SetActive(false);

        }



    }








}
