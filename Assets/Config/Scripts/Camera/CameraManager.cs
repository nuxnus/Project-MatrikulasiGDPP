using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Action OnChangePerspective;
    [SerializeField]
    public CameraState CameraState;
    [SerializeField]
    private CinemachineCamera _fpsCamera;
    [SerializeField]
    private CinemachineCamera _tpsCamera;
    [SerializeField]
    private InputManager _inputManager;
    
    private void Start()
    {
        _inputManager.OnChangePOV += SwitchCamera;
    }
    private void OnDestroy()
    {
        _inputManager.OnChangePOV -= SwitchCamera;
    }
    public void SetFPSClampedCamera(bool isClamped, Vector3 playerRotation)
    {
        CinemachinePanTilt pov = _fpsCamera.GetComponent<CinemachinePanTilt>();
        if (isClamped)
        {
            pov.PanAxis.Wrap = false;
        }
        else
        {
            pov.PanAxis.Wrap = true;
        }
    }
    
    private void SwitchCamera()
    {
        OnChangePerspective();
        if (CameraState == CameraState.ThirdPerson)
        {
            CameraState = CameraState.FirstPerson;
            _tpsCamera.gameObject.SetActive(false);
            _fpsCamera.gameObject.SetActive(true);
        }
        else
        {
            CameraState = CameraState.ThirdPerson;
            _tpsCamera.gameObject.SetActive(true);
            _fpsCamera.gameObject.SetActive(false);
        }
    }
    public void SetTPSFieldOfView(float fieldOfView)
    {
        _tpsCamera.Lens.FieldOfView = fieldOfView;
    }
}
