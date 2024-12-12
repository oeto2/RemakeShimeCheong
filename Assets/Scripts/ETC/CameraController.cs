using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D _cinemachineConfiner2D;


    private void Awake()
    {
        _cinemachineConfiner2D = GetComponent<CinemachineConfiner2D>();
    }

}
