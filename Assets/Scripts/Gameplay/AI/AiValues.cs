using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiValues: MonoBehaviour
{
    public float health;
    public static List<AiValues> allies = new List<AiValues>();

    public Weapon weapon;
    public Bot bot;

    // Start is called before the first frame update
    private void Start()
    {
        allies.Add(this);
        bot = GetComponentInChildren<Bot>();
        // weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnDestroy()
    {
        allies.Remove(this);
    }
}