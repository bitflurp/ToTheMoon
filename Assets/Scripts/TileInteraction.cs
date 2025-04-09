using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    public int posX;
    public int posY;
    

    [SerializeField] private GameObject highlight;

    //Quick way to make hovering highlights: prolly gonna have to rework it 
    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseUp()
    {

        if (CompareTag("Land"))
        {
            Debug.Log("You can build a facotory and my pos is" + posX + "," + posY);

     
            

        }


        if (CompareTag("Locked"))
        {

            Debug.Log("I'm locked, Cant do shit");

        }

        //Debug.Log("hello I am" + gameObject.name);
    }
}
