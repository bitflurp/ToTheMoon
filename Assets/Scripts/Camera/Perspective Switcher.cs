using UnityEngine;

using System.Collections;

using UnityEngine.EventSystems;

[RequireComponent(typeof(MatrixBlender))]

public class PerspectiveSwitcher : MonoBehaviour
{

    private UserInterface uiData;
    
    public GameObject buttonMain;

    public GameObject buttonWeather;

    [SerializeField]

    private GameObject[] _cameras;

    private Matrix4x4 ortho,

                        perspective;

    public float fov = 60f,

                        near = .3f,

                        far = 1000f,

                        orthographicSize = 50f;

    private float aspect;

    private MatrixBlender blender;

    private bool orthoOn;

    Camera m_camera;

    void Start()
    {

       

        aspect = (float)Screen.width / (float)Screen.height;

        ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);

        perspective = Matrix4x4.Perspective(fov, aspect, near, far);

        m_camera = GetComponent<Camera>();

        m_camera.projectionMatrix = ortho;

        orthoOn = true;

        blender = (MatrixBlender)GetComponent(typeof(MatrixBlender));

    }

    public void Switch()

    {

        GameObject curButton = EventSystem.current.currentSelectedGameObject;

        orthoOn = !orthoOn;

        if (orthoOn)

        {

            switch (true)
            {

                case true when curButton == buttonMain:

                    blender.BlendToMatrix(ortho, 1f, 8, true);


                    _cameras[0].SetActive(true);

                    break;

                case true when curButton == buttonWeather:

                    blender.BlendToMatrix(ortho, 1f, 8, true);

                    
                    _cameras[1].SetActive(false);

                    _cameras[2].SetActive(true);


                    break;


            }

        }

        else

        {

            blender.BlendToMatrix(perspective, 1f, 8, false);

            _cameras[0].SetActive(false);

            _cameras[1].SetActive(true);

        }

    }

}
