using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager of UbhBullet
/// </summary>
[DisallowMultipleComponent]
public sealed class UbhBulletManager : UbhSingletonMonoBehavior<UbhBulletManager>
{
    private List<UbhBullet> m_bulletList = new List<UbhBullet>(2048);
    private HashSet<UbhBullet> m_bulletHashSet = new HashSet<UbhBullet>();

    public int activeBulletCount { get { return m_bulletList.Count; } }

    protected override void Awake()
    {
        base.Awake();

        // Create UbhTimer
        if (UbhTimer.instance == null) { }
    }

    /// <summary>
    /// Update Bullets Move
    /// </summary>
    public void UpdateBullets(float deltaTime)
    {
        for (int i = m_bulletList.Count - 1; i >= 0; i--)
        {
            UbhBullet bullet = m_bulletList[i];
            if (bullet == null)
            {
                m_bulletList.Remove(bullet);
                continue;
            }
            bullet.UpdateMove(deltaTime);
        }
    }

    /// <summary>
    /// Add bullet
    /// </summary>
    public void AddBullet(UbhBullet bullet)
    {
        if (m_bulletHashSet.Contains(bullet))
        {
            Debug.LogWarning("This bullet is already added in m_bulletList.");
            return;
        }
        m_bulletList.Add(bullet);
        m_bulletHashSet.Add(bullet);
    }

    /// <summary>
    /// Remove bullet
    /// </summary>
    public void RemoveBullet(UbhBullet bullet)
    {
        if (m_bulletHashSet.Contains(bullet) == false)
        {
            Debug.LogWarning("This bullet is not found in m_bulletList.");
            return;
        }
        m_bulletList.Remove(bullet);
        m_bulletHashSet.Remove(bullet);
    }
}
