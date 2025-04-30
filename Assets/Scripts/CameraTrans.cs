using UnityEngine;

public class CameraTrans : MonoBehaviour
{
    private bool shouldMove = false;
    private Transform persPos = (6,18,40);
  public void ChangeCamera()
  {
    Camera.main.orthographic = false;
    Transition();
    
  //  Camera.main.transform.position = new Vector3(6,18,40);
    // Camera.main.transform.rotation = Quaternion.Euler(16,0,0);
  }
  
    IEnumerator Transition()
    {
        while(!Camera.main.transform.position = (6,18,40))
        {
             Camera.main.transform.position = Vector3.Lerp((6,5,-34),(6,18,40),Time.deltaTime);
             yield return null;
            

        }
    }
}
