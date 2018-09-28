using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] private ExecuteType m_type;
    [SerializeField] private Camera m_camera;
    [SerializeField] private int m_waveSize = 10;
    [SerializeField] private float m_waveSpeed = 5;

    private BaseController m_controller;

    private void Awake()
    {
        m_camera.orthographicSize = 4 + ((float)m_waveSize - 10) * 0.4f;

        switch (m_type)
        {
            case ExecuteType.Classic:
                m_controller = new ClassicController(transform, m_waveSize, m_waveSpeed);
                break;
            case ExecuteType.HybridECS:
                m_controller = new HybridECSController(transform, m_waveSize, m_waveSpeed);
                break;
            case ExecuteType.ClassicJobSystem:
                m_controller = new ClassicJobSystemController(transform, m_waveSize, m_waveSpeed);
                break;
            case ExecuteType.PureECS:
                m_controller = new PureECSController(transform, m_waveSize, m_waveSpeed);
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
