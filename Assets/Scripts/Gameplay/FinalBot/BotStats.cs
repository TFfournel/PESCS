using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStats: MonoBehaviour
{
    public float health;

    public Weapon currentWeapon;
    public NavMeshAgent agent;

    public enum Team
    { Red, Blue }

    [SerializeField] public Team CurrentTeam = Team.Red;
    [SerializeField] private BlueTeam _BlueTeam;
    [SerializeField] private RedTeam _RedTeam;

    [Header("Sites")]
    [SerializeField] private BoxCollider siteACollider;

    [SerializeField] private BoxCollider siteBCollider;

    public enum Site
    { SiteA, SiteB }

    [SerializeField] public Site CurrentSite = Site.SiteA;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}