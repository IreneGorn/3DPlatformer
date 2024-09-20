using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraSetup : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private IPlayerController playerController;

    [Inject]
    public void Construct(IPlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component not found on this GameObject.");
            return;
        }

        SetupCamera();
    }

    private void SetupCamera()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController is null. Make sure it's properly injected.");
            return;
        }

        Transform playerTransform = (playerController as MonoBehaviour)?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not found.");
            return;
        }

        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
        
        Debug.Log("Camera setup complete. Following player.");
    }
}