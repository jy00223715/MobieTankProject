using UnityEngine;
using System.Collections;

public class UIJoystick : MonoBehaviour
{
    public UISprite m_node;
    public UISprite m_background;
    public float m_fadeOutTime = 2.0f;

    //float m_fadeOutTimer = 0.0f;
    float m_maxDistance = 0.0f;
    float m_power = 0.0f;
    Vector3 m_direction = Vector3.zero;

    void Start()
    {
        //gameObject.SetActive(false);
        //ResetNode();
        m_maxDistance = (m_background.width - m_node.width) / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (m_background.gameObject.activeSelf)
            {
                var movePos = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
                if (Vector3.Distance(movePos, m_background.transform.position) > m_maxDistance)
                    m_node.transform.position = m_direction * m_maxDistance;
                else
                    m_node.transform.position = movePos;

                m_direction = (movePos - m_background.transform.position).normalized;
                m_power = Vector3.Distance(m_node.transform.position, m_background.transform.position) / m_maxDistance;

                Debug.Log(string.Format("Direction:{0}\tPower:{1}", m_direction, m_power));
            }
            else
            {
                ShowController();
                m_background.transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            ResetNode();
            FadeOut();
        }
    }

    void ResetNode()
    {
        m_node.transform.localPosition = Vector3.zero;
        m_power = 0.0f;
        m_direction = Vector3.zero;
    }

    void ShowController()
    {
        m_background.gameObject.SetActive(true);
        m_background.alpha = 1.0f;
    }

    void FadeOut()
    {
        if (m_background.alpha > 0.0f)
            m_background.alpha -= Time.deltaTime / m_fadeOutTime;
        else
            m_background.gameObject.SetActive(false);
    }
}
