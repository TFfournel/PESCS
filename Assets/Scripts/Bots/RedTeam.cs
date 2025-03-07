using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTeam : MonoBehaviour
{
    public enum Site { SiteA, SiteB }
    [SerializeField] public Site CurrentSite = Site.SiteA;

    private void Start()
    {
        SetSite();
    }

    public Site GetSite() { return CurrentSite; }
    public void SetSite() { CurrentSite = (Site)Random.Range(0, 2); ; }
}
