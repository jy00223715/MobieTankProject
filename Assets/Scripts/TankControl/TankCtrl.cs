using UnityEngine;
using System.Collections;

public class TankCtrl : MonoBehaviour
{
    public float m_accSpeed;
    public float m_maxSpeed;
    public float m_sterringSpeed;

    protected Transform m_turret;
    protected Transform m_body;
    
    protected float m_power = 0.0f;

    float m_currentSpeed = 0.0f;
    float m_unTargetTimer = 0.0f;
    Vector3 m_moveDest = Vector3.zero;
    Vector3 m_moveStartPos = Vector3.zero;
    Vector3 m_targetPos = Vector3.zero;
    Vector3 m_tankBodyPos = Vector3.zero;
    Vector3 m_tankTurretPos = Vector3.zero;
    Rigidbody m_rigBody;
    Quaternion m_rotEnd;
    Quaternion m_turrentEnd;

    void Start()
    {
        m_rigBody = GetComponent<Rigidbody>();
        m_turret = transform.Find("TankRenderers/TankTurret");
        m_body = transform.Find("TankRenderers/TankeBody");
        m_tankBodyPos = m_body.transform.localPosition;
        m_tankTurretPos = m_turret.transform.localPosition;
    }

    protected virtual void Update()
    {
        if(AdjustBody())
            MoveToDest();

        AdjustTurret();
        UpdateTankJointPos();
    }


    void MoveToDest()
    {
        if (transform.position != m_moveDest)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_moveDest, m_currentSpeed * Time.deltaTime);
            AdjustSpeed();
        }
    }

    protected void SetDest(Vector3 destPos)
    {
        m_targetPos = Vector3.zero;
        m_moveDest = destPos;
        m_moveStartPos = transform.position;
        m_rotEnd = Quaternion.LookRotation(m_moveDest);
    }

    protected void SetTargetPos(Vector3 pos)
    {
        m_targetPos = pos;
        m_turrentEnd = Quaternion.LookRotation(pos);
    }

    bool AdjustBody()
    {
        if (m_body.rotation != m_rotEnd)
            m_body.rotation = Quaternion.RotateTowards(m_body.rotation, m_rotEnd, m_sterringSpeed * Time.deltaTime);

        return m_body.rotation == m_rotEnd ? true : false;
    }

    void AdjustTurret()
    {
        if (m_targetPos != Vector3.zero)
        {
            if (RotTurrent(m_turrentEnd))
                ShotShell();
        }
        else
            RotTurrent(m_rotEnd);
    }

    void AdjustSpeed()
    {
        var distance = Vector3.Distance(transform.position, m_moveDest);
        var lastDis = Vector3.Distance(m_moveStartPos, m_moveDest);
        if (distance < lastDis / 2.0f)
        {
            m_currentSpeed -= m_accSpeed * Time.deltaTime;
            if (m_currentSpeed < m_accSpeed)
                m_currentSpeed = m_accSpeed;
        }
        else
        {
            m_currentSpeed += m_accSpeed * Time.deltaTime;
            if (m_currentSpeed > m_maxSpeed)
                m_currentSpeed = m_maxSpeed;
        }

        Debug.Log(m_currentSpeed);
    }

    bool RotTurrent(Quaternion to)
    {
        if (m_turret.rotation != to)
            m_turret.rotation = Quaternion.RotateTowards(m_turret.rotation, to, m_sterringSpeed * Time.deltaTime);

        return m_turret.rotation == to ? true : false;
    }

    void UpdateTankJointPos()
    {
        m_body.transform.localPosition = m_tankBodyPos;
        m_turret.transform.localPosition = m_tankTurretPos;
    }

    void ShotShell()
    {
        Debug.Log("Shot");
    }
}