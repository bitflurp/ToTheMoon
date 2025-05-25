using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class ProfitScreen : MonoBehaviour
{
    private UserInterface uiData;
    private TileData tileData;
    private Tilemap tilemap;
    private AudioManager audioData;
 
    public int moneyProfit;
    public int wfProfit;
    public int recProfit;

    private void Start()
    {
        uiData = GetComponent<UserInterface>();
        tileData = GetComponent<TileData>();
        tilemap = GetComponent<Tilemap>();
        audioData = GetComponent<AudioManager>();

        Coroutine startCo = StartCoroutine(StartScreen());
    }
    public IEnumerator ProfitAnim()
    {

        uiData.psPanel.gameObject.SetActive(true);
        uiData.profitEndText.enabled = true;

        uiData.profitEndText.text = "";

        string mStr = $"Money Profit =";
        string mStrNum = $" {moneyProfit}";


        string wfStr = $"\n\nWF Profit =";
        string wfStrNum = $" {wfProfit}";


        string rStr = $"\n\nRec Profit =";
        string rStrNum = $" {recProfit}";





        uiData.profitEndText.text += mStr;
        audioData.PlaySFX();
        yield return new WaitForSeconds(1);

        uiData.profitEndText.text += mStrNum;
        audioData.PlaySFX();


        yield return new WaitForSeconds(1);


        uiData.profitEndText.text += wfStr;
        audioData.PlaySFX();
        yield return new WaitForSeconds(1);


        uiData.profitEndText.text += wfStrNum;
        audioData.PlaySFX();


        yield return new WaitForSeconds(1);


        uiData.profitEndText.text += rStr;
        audioData.PlaySFX();
        yield return new WaitForSeconds(1);



        uiData.profitEndText.text += rStrNum;
        audioData.PlaySFX();


        yield return new WaitForSeconds(1);



        uiData.psPanel.gameObject.SetActive(false);
        uiData.profitEndText.enabled = false;

        moneyProfit = 0;
        wfProfit = 0;
        recProfit = 0;

    }




    public IEnumerator StartScreen() {


        uiData.profitEndText.text = "";
        uiData.profitEndText.fontSize = 13;

        string Str1 = $"PMAP: PCID enabled";

        char[] characters1 = Str1.ToCharArray();

        string Str2 = $"\nHacknet Kernel Version 1.0.0: Tue Oct 11 20:56:35 PDT 2011; root:xnu-1699.22.73~1/RELEASE_X86_64";

        char[] characters2 = Str2.ToCharArray();

        string Str3 = $"\nkext submap [0xffffff7f8072e000 - 0xffffff8000000000], kernel text [0xffffff8000200000 - 0xffffff800072e000]";

        char[] characters3 = Str3.ToCharArray();


        uiData.psPanel.gameObject.SetActive(true);
        uiData.profitEndText.enabled = true;

        yield return new WaitForSeconds(2);


        for (int i = 0; i < characters1.GetLength(0); i++)
        {


            uiData.profitEndText.text += characters1[i];

            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }


        yield return new WaitForSeconds(1);

        for (int i = 0; i < characters2.GetLength(0); i++)
        {


            uiData.profitEndText.text += characters2[i];

            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);


        for (int i = 0; i < characters3.GetLength(0); i++)
        {


            uiData.profitEndText.text += characters3[i];

            for (int j = 0; j < 2; j++)
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);

        int rNum = 0;
        

        for (int i = 0; i < 5; i++)
        {
           string Str4 = $"\nHacknetACPICPU: ProcessorId: {rNum} LocalApicId=0 Enabled";

          char[] characters4 = Str4.ToCharArray();
            for (int l = 0; l < characters4.GetLength(0); l++)
            {


                uiData.profitEndText.text += characters4[l];

                for (int j = 0; j < 2; j++)
                {
                    yield return null;
                }
            }

            rNum++;
        }

        yield return new WaitForSeconds(2);


        uiData.profitEndText.fontSize = 30;
        uiData.psPanel.gameObject.SetActive(false);
        uiData.profitEndText.enabled = false;







    }
    public IEnumerator LoseScreen() {



     uiData.profitEndText.text = "";

     uiData.profitEndText.fontSize = 13;

     string Str1 = $"MESSAGE INCOMING FROM | ADMIN";

     char[] characters1 = Str1.ToCharArray();

     string Str2 = $"\n\nDear 'EMPLOYEE',";

     char[] characters2 = Str2.ToCharArray();

     string Str3 = $"\n\nWe regret to inform you that due to your subpar perfomance, your position at Space Co has been ";

     char[] characters3 = Str3.ToCharArray();

     string Str4 = $"\n\nT E R M I N A T E D ";

     char[] characters4 = Str4.ToCharArray();

     uiData.psPanel.gameObject.SetActive(true);
     uiData.profitEndText.enabled = true;



     for (int i = 0; i < characters1.GetLength(0); i++)
     {


         uiData.profitEndText.text += characters1[i];

         for (int j = 0; j < 5; j++)
         {
             yield return null;
         }
     }

     yield return new WaitForSeconds(1);

     for (int i = 0; i < characters2.GetLength(0); i++)
     {


         uiData.profitEndText.text += characters2[i];

         for (int j = 0; j < 5; j++)
         {
             yield return null;
         }
     }


     yield return new WaitForSeconds(1);

     for (int i = 0; i < characters3.GetLength(0); i++)
     {


         uiData.profitEndText.text += characters3[i];

         for (int j = 0; j < 5; j++)
         {
             yield return null;
         }
     }


     yield return new WaitForSeconds(1);

     for (int i = 0; i < characters4.GetLength(0); i++)
     {


         uiData.profitEndText.text += characters4[i];

         for (int j = 0; j < 10; j++)
         {
             yield return null;
         }
     }

     yield return new WaitForSeconds(1);
        
        audioData.PlaySFX();

        uiData.buttonReset.gameObject.SetActive(true);
    




 }




}
