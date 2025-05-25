using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    private TilemapControls clickData;
    [SerializeField] private ParticleSystem pSystem;


    private void Start()
    {

        clickData = GetComponent<TilemapControls>();

    }
    public void PFX() {

        pSystem.gameObject.SetActive(true);
        pSystem.transform.position = new Vector3Int(clickData.clickedCell.x - 4, clickData.clickedCell.y, 65);
        pSystem.Play();
    

    }

}
