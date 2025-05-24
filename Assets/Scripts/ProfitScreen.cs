using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class ProfitScreen : MonoBehaviour
{
    private UserInterface uiData;
    private TileData tileData;
    private Tilemap tilemap;
   
 
    public int moneyProfit;

    private void Start()
    {
        uiData = GetComponent<UserInterface>();
        tileData = GetComponent<TileData>();
        tilemap = GetComponent<Tilemap>();

    }
    public IEnumerator ProfitAnim()
    {
        uiData.profitEndText.text = "";

        string mStr = $"money Profit = {moneyProfit}";
        
        char[] charactersM = mStr.ToCharArray();

        string wfStr = $"\nWF Profit = {moneyProfit}";

        char[] charactersWf = wfStr.ToCharArray();

        string rStr = $"\nRec Profit = {moneyProfit}";

        char[] charactersR = rStr.ToCharArray();


        uiData.psPanel.gameObject.SetActive(true);
        uiData.profitEndText.enabled = true;

        for (int i = 0; i < charactersM.GetLength(0); i++) {


            uiData.profitEndText.text += charactersM[i];
           
            for (int j = 0; j < 20; j++)
            {
                yield return null;
            }
        }
           

        yield return new  WaitForSeconds(1);

        for (int i = 0; i < charactersWf.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersWf[i];

            for (int j = 0; j < 20; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);


        for (int i = 0; i < charactersWf.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersR[i];

            for (int j = 0; j < 20; j++)
            {
                yield return null;
            }
        }


        yield return new WaitForSeconds(2);


   
        uiData.psPanel.gameObject.SetActive(false);
        uiData.profitEndText.enabled = false ;


    }

    public IEnumerator StartAnim() {


        for (int x = 2; x < tileData.gridX; x++)
        {
            for (int y = 1; y < tileData.gridY; y++)
            {
                Vector3Int nowTilePos = new Vector3Int(x, y, 0);
                //TileBase nowTile = tilemap.GetTile(nowTilePos);

                if (tileData.startData.ContainsKey(nowTilePos)) { 
                
                tilemap.SetTile(nowTilePos, tileData.startData[nowTilePos]);

                    for (int i = 0; i < 20; i++)
                    {
                        yield return null;
                    }



                }
                

            }

        }
    }




}


