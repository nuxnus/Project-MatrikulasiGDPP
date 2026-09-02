using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public CameraState CameraState;
    [SerializeField]
    private CinemachineCamera _fpsCamera;
    public void SetFPSClampedCamera(bool isClamped, Vector3 playerRotation)
    {
        CinemachinePanTilt pov = _fpsCamera.GetComponent<CinemachinePanTilt>();
        if (isClamped)
        {
            pov.PanAxis.Wrap = false;
            pov.PanAxis.Range.x = playerRotation.y - 45;
            pov.PanAxis.Range.y = playerRotation.y + 45;
        }
        else
        {
            pov.PanAxis.Range.x = -180;
            pov.PanAxis.Range.y = 180;
            pov.PanAxis.Wrap = true;
        }
    }
}
