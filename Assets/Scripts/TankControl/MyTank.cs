using UnityEngine;
using System.Collections;

public class MyTank : TankCtrl
{
    public Camera m_camera;

    bool m_onClick = false;
    float m_lastClickTime = 0.0f;
    float m_shotTimer = 0.0f;

    protected override void Update()
    {
        CheckInput();
        base.Update();
        UpdateCameraPos();
    }


    void CheckInput()
    {
        /*
        if (Input.GetMouseButtonDown(0))
            m_onClick = true;

        if (Input.GetMouseButtonUp(0))
        {
            if (m_doubleClickTime > 0.0f)
            {
                if (Time.realtimeSinceStartup - m_doubleClickTime < 0.2f)
                {
                    Vector3 pos = Vector3.zero;
                    if (GetRayCast(out pos))
                        SetTargetPos(pos);
                }

                m_doubleClickTime = 0.0f;
            }
            else
                m_doubleClickTime = Time.realtimeSinceStartup;

            m_onClick = false;
        }

        if (m_onClick)
        {
            m_lastClickTime += Time.deltaTime;
            if (m_lastClickTime > 0.2f)
            {
                Vector3 pos = Vector3.zero;
                if (GetRayCast(out pos))
                    SetDest(pos);
            }
        }
         */

        //这里是一段操作，单机或者长安就是移动，短按就是射击，感觉不太好，再实现一种双击发射的模式
        /*
        if (Input.GetMouseButtonDown(0))
            m_onClick = true;

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Vector3.zero;
            if (m_shotTimer > 0.3f)
            {
                if (GetRayCast(out pos))
                    SetTargetPos(pos);
            }
            else
            {
                if (GetRayCast(out pos))
                    SetDest(pos);
            }
            m_onClick = false;
        }

        if (m_onClick)
        {
            m_shotTimer += Time.deltaTime;
        }
        else
        {
            m_shotTimer = 0.0f;
        }
         */
    }

    bool GetRayCast(out Vector3 hitPos)
    {
        RaycastHit hit;
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
        {
            hitPos = hit.point;
            return true;
        }
        else
        {
            hitPos = Vector3.zero;
            return false;
        }
    }

    void UpdateCameraPos()
    {
        m_camera.transform.localPosition = new Vector3(0, 10, -10);
        m_camera.transform.localRotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }
}