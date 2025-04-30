using System.Collections;
using UnityEngine;

public class CameraTrans : MonoBehaviour
{
    private bool shouldMove = false;
    private Vector3 perPos =new Vector3(6, 18, 40); 
    public void ChangeCamera()
    {
        Camera.main.orthographic = false;
        Transition();

        //  Camera.main.transform.position = new Vector3(6,18,40);
        // Camera.main.transform.rotation = Quaternion.Euler(16,0,0);

    }

    IEnumerator Transition()
    {
        Vector3 curCamPos = Camera.main.transform.position;
        bool isThere = false;
        
        
        
        
        
        while (isThere == false)
        {


            Camera.main.transform.position = Vector3.Lerp(curCamPos, perPos , Time.deltaTime);
           
           
            
            if (curCamPos == perPos)
            {
            isThere = true;

            }

            yield return null;
        }
    }



}
