using UnityEditorInternal;
using UnityEngine;


public enum turnState {start, end }
public class TurnSystem : MonoBehaviour
{
    public turnState state;
    public int turnCounter = 1;

    private TilemapControls tileData;
    private int endProduction;
    private int addRec;
    private int curRecEnd;
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

        // Get data from tilemapControls
        endProduction = tileData.productionCounter;
        curRecEnd = tileData.rec;

        //Check how many factories are in production
        if (endProduction > 0)
        {
            Debug.Log($"PRODUCE" + endProduction);

            // Multiply Factories producing by product per factory producing (2)
            addRec = endProduction * 2;

            //add profit Recourse to player Recourse 
            curRecEnd = curRecEnd + addRec;

            // Reset Factory Production 
            endProduction = 0;


        }
        else
        {
            Debug.Log("no prodice");
        }






        StartTurn();
    }

    public void StartTurn()
    {
        //Send Data to player
        tileData.productionCounter = endProduction;
        tileData.rec = curRecEnd;


        Debug.Log($"DAY" + turnCounter );

    }


}
