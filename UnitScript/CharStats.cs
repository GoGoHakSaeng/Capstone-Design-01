using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Goguryeo,
    Baekje,
    Shilla,
    Gaya
}

public class CharStats : MonoBehaviour
{
    public Faction faction;                            //진영

    public float maxHealth = 100f;                     //최대 체력
    public float curHealth { get; private set; }       //현재 체력

    public Stat damage;
    public Stat defense;

    private void Awake()
    {
        curHealth = maxHealth;                         //시작체력 설정
    }

    public void TakeDamage(int damage)                 //데미지 계산
    {
        curHealth -= damage - (defense.GetStat() * 0.5f);
        if (curHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}