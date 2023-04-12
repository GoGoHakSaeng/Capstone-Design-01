using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharStats))]
public class CharCombat : MonoBehaviour
{
    public float atkSpd = 1f;
    private float atkCdw = 0f;

    CharStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharStats>();
    }

    private void Update()
    {
        atkCdw -= Time.deltaTime;
    }

    public void Attack(CharStats targetStats)
    {
        if (atkCdw <= 0)
        {
            targetStats.TakeDamage(myStats.damage.GetStat());
            atkCdw = 1f / atkSpd;
        }
    }
}
