using UnityEngine;

public class TileInteraction : MonoBehaviour
{
    public int posX;
    public int posY;
    
    public GameObject Tile_Factory;
    

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
            gameObject.SetActive(false);
            // creates factory tile
            Instantiate(Tile_Factory, transform.position, Quaternion.identity);
            

        }


        if (CompareTag("Locked"))
        {

            Debug.Log("I'm locked, Cant do shit");

        }
        if (CompareTag("Factory"))
        {

            Debug.Log("I'm a factory" + posX + "," + posY);

        }
        //Debug.Log("hello I am" + gameObject.name);
    }
}
