using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    #region Singleton

    public static CameraShake Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }
    }

    #endregion Singleton

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
    private float shakeTimer;

    private void Start()
    {
        cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer < 0) { return; }

        shakeTimer -= Time.deltaTime;
        if (shakeTimer <= 0f) // time is over
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

        }
    }
}