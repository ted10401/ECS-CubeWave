using UnityEngine;

public abstract class BaseController
{
    protected Transform m_parent;
    protected int m_waveSize;
    protected float m_waveSpeed;
    protected Vector3 m_centerPosition;

    public BaseController(Transform parent, int waveSize, float waveSpeed)
    {
        m_parent = parent;
        m_waveSize = waveSize;
        m_waveSpeed = waveSpeed;
        m_centerPosition = new Vector3((float)m_waveSize / 2, 0, (float)m_waveSize / 2);
        if (m_waveSize % 2 == 0)
        {
            m_centerPosition += new Vector3(0.5f, 0, 0.5f);
        }
    }

    protected void CreateCubes()
    {
        for (int x = 0; x < m_waveSize; x++)
        {
            for (int y = 0; y < m_waveSize; y++)
            {
                CreateCube(x, y);
            }
        }
    }

    public abstract void CreateCube(int x, int y);

    public abstract void Tick();

    protected Vector3 GetPosition(int x, int y)
    {
        return new Vector3(x + 1, 0, y + 1);
    }

    protected float GetDistance(int x, int y)
    {
        return Vector3.Distance(m_centerPosition, GetPosition(x, y));
    }
}
