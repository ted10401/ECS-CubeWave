using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private WaveType m_type;
    [SerializeField] private Camera m_camera;
    [SerializeField] private int m_waveSize = 10;
    [SerializeField] private float m_waveSpeed = 5;

    private BaseWave m_controller;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 9999;

        m_camera.orthographicSize = 4 + ((float)m_waveSize - 10) * 0.4f;

        switch (m_type)
        {
            case WaveType.Classic:
                m_controller = new ClassicWave(transform, m_waveSize, m_waveSpeed);
                break;
            case WaveType.HybridECS:
                m_controller = new HybridECSWave(transform, m_waveSize, m_waveSpeed);
                break;
            case WaveType.ClassicJobSystem:
                m_controller = new ClassicJobSystemWave(transform, m_waveSize, m_waveSpeed);
                break;
            case WaveType.PureECS:
                m_controller = new PureECSWave(transform, m_waveSize, m_waveSpeed);
                break;
        }
    }

    private void Update()
    {
        m_controller.Tick();
    }

    private void OnDisable()
    {
        m_controller.Destroy();
    }
}
