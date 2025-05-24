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
    public int wfProfit;
    public int recProfit;

    private void Start()
    {
        uiData = GetComponent<UserInterface>();
        tileData = GetComponent<TileData>();
        tilemap = GetComponent<Tilemap>();

        //Coroutine startCo = StartCoroutine(StartScreen());
        
    }
    public IEnumerator ProfitAnim()
    {
        uiData.profitEndText.text = "";

        string mStr = $"money Profit = {moneyProfit}";
        
        char[] charactersM = mStr.ToCharArray();

        string wfStr = $"\n\nWF Profit = {wfProfit}";

        char[] charactersWf = wfStr.ToCharArray();

        string rStr = $"\n\nRec Profit = {recProfit}";

        char[] charactersR = rStr.ToCharArray();


        uiData.psPanel.gameObject.SetActive(true);
        uiData.profitEndText.enabled = true;

        //Money Text
        for (int i = 0; i < charactersM.GetLength(0); i++) {


            uiData.profitEndText.text += charactersM[i];
           
            //Frame Wait
            for (int j = 0; j < 5; j++)
            {
                yield return null;
            }
        }
           

        yield return new  WaitForSeconds(1);

        //WF Text
        for (int i = 0; i < charactersWf.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersWf[i];


            //Frame Wait
            for (int j = 0; j < 5; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        //Rec text
        for (int i = 0; i < charactersR.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersR[i];

            //Frame Wait
            for (int j = 0; j < 5; j++)
            {
                yield return null;
            }
        }


        yield return new WaitForSeconds(1);


   
        uiData.psPanel.gameObject.SetActive(false);
        uiData.profitEndText.enabled = false ;


    }

    public IEnumerator StartScreen()
    {

        string oStr = $"PMAP: PCID enabled";

        char[] charactersM = oStr.ToCharArray();

        string tfStr = $"\n\nHacknet Kernel Version 1.0.0: Tue Oct 11 20:56:35 PDT 2011; root:xnu-1699.22.73~1/RELEASE_X86_64";

        char[] charactersWf = tfStr.ToCharArray();

        string thStr = $"\n\nkext submap [0xffffff7f8072e000 - 0xffffff8000000000], kernel text [0xffffff8000200000 - 0xffffff800072e000]";

        char[] charactersR = thStr.ToCharArray();


        uiData.psPanel.gameObject.SetActive(true);
        uiData.profitEndText.enabled = true;
        uiData.profitEndText.fontSize = 13;

        yield return new WaitForSeconds(1);
        //1st Text
        for (int i = 0; i < charactersM.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersM[i];

            //Frame Wait
            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }


        yield return new WaitForSeconds(1);

        //WF Text
        for (int i = 0; i < charactersWf.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersWf[i];


            //Frame Wait
            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        //Rec text
        for (int i = 0; i < charactersR.GetLength(0); i++)
        {


            uiData.profitEndText.text += charactersR[i];

            //Frame Wait
            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);


        int rNum = 0;

        //Frame Wait
        for (int l = 0; l < 5; l++)
        {
            string fStr = $"\nHacknetACPICPU: ProcessorId={rNum} LocalApicId=0 Enabled";

            char[] charactersf = fStr.ToCharArray();

            for (int i = 0; i < charactersf.GetLength(0); i++)
            {


                uiData.profitEndText.text += charactersf[i];

                //Frame Wait
                for (int j = 0; j < 5; j++)
                {
                    yield return null;
                }



            }

            rNum++;
        }


        yield return new WaitForSeconds(1);
      


        uiData.psPanel.gameObject.SetActive(false);
        uiData.profitEndText.enabled = false;

    }

    public IEnumerator StartAnim() {
        Collider2D collideData = GetComponent<Collider2D>();

        collideData.enabled = false;

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


        collideData.enabled =true;
    }




}


