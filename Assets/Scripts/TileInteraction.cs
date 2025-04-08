using UnityEngine;

public class TileInteraction : MonoBehaviour
{

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
        Debug.Log("hello I am" + gameObject.name);
    }
}
