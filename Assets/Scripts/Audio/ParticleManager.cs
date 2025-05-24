using Unity.VisualScripting;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject particleSystem;
    private ParticleSystem particleData;

    private TilemapControls clickData;
    private void Start()
    {
        clickData = GetComponent<TilemapControls>();
        particleData = particleSystem.GetComponent<ParticleSystem>();
    }

    public void ParticleEffect() {

        particleSystem.gameObject.SetActive(true);
        particleSystem.transform.position = clickData.clickedCell;
        particleData.Play();

        

    }



}
