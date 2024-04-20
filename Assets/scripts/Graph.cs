using UnityEngine;


public class Graph : MonoBehaviour
{
    [SerializeField] private Transform m_PointPrefab;

    [SerializeField, Range(10, 500)] private int m_Resolution = 10;
    private int m_PreviousResolution;

    private Transform[] m_Points;

    private void Awake()
    {
        m_PreviousResolution = m_Resolution;
        float step = 2.0f / m_PreviousResolution;
        Vector3 position = Vector3.zero;
        Vector3 scale = Vector3.one * step;
        m_Points = new Transform[m_PreviousResolution];

        for (int i = 0; i < m_Points.Length; i++)
        {
            m_Points[i] = Instantiate(m_PointPrefab);
            position.x = (i + 0.5f) * step - 1;
            m_Points[i].localPosition = position;
            m_Points[i].localScale = scale;
            m_Points[i].SetParent(transform, false);
        }
    }

    void Update()
    {
        if (m_PreviousResolution != m_Resolution)
        {
            for (int i = 0; i < m_Points.Length; i++)
            {
                Destroy(m_Points[i].gameObject);
            }

            Awake();
        }

        float timeNow = Time.time;

        for (int i = 0; i < m_Points.Length; i++)
        {
            Vector3 position = m_Points[i].localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + timeNow));
            m_Points[i].localPosition = position;
        }
    }
}
