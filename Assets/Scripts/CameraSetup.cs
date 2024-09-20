using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraSetup : MonoBehaviour
{
    private CinemachineFreeLook  _freeLookCamera;
    private IPlayerController _playerController;

    [Inject]
    public void Construct(IPlayerController playerController)
    {
        _playerController = playerController;
    }

    private void Start()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
        if (_freeLookCamera == null)
        {
            Debug.LogError("CinemachineVirtualCamera component not found on this GameObject.");
            return;
        }

        SetupCamera();
    }

    private void SetupCamera()
    {
        if (_playerController == null)
        {
            Debug.LogError("PlayerController is null. Make sure it's properly injected.");
            return;
        }

        Transform playerTransform = (_playerController as MonoBehaviour)?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not found.");
            return;
        }

        _freeLookCamera.Follow = playerTransform;
        _freeLookCamera.LookAt = playerTransform;
        
        Debug.Log("FreeLook camera setup complete. Following and looking at player.");
    }
}