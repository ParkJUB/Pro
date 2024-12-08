using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // 전환할 카메라 배열
    private int currentCameraIndex = 0; // 현재 활성화된 카메라 인덱스

    void Start()
    {
        // 모든 카메라를 비활성화하고 첫 번째 카메라만 활성화
        InitializeCameras();
    }

    void Update()
    {
        // C 키를 눌렀을 때 카메라 전환
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToNextCamera();
        }
    }

    void InitializeCameras()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    void SwitchToNextCamera()
    {
        // 현재 카메라 비활성화
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // 다음 카메라로 인덱스 업데이트 (순환)
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // 새 카메라 활성화
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}
