using UnityEngine;
using System.Collections;

public class ShellBase : MonoBehaviour
{
    protected float m_shellSpeed;
    protected float m_blastRadius;
    protected int m_damage;

    public virtual void ShotShell(Vector3 targetPos)
    {
 
    }
}