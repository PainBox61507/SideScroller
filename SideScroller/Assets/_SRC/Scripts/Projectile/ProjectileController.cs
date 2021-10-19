using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public delegate void OnProjectileDeath_();
    public event OnProjectileDeath_ OnProjectileDeath;


    public void Die()
    {
        this.gameObject.SetActive(false);
        if (OnProjectileDeath != null && OnProjectileDeath.Target != null)
        {
            OnProjectileDeath.Invoke();
        }
    }
}
