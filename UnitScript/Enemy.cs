using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    CharCombat combat;
    public Transform target;
    NavMeshAgent nav;

    // 초기 데이터 설정
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharCombat>();
    }

    // 계속해서 탐색 및 공격
    void Update()
    {
        EnemyAI();
    }


    void EnemyAI()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);
        if (distance > 1.3f)
        {
            nav.SetDestination(target.transform.position);
        }
        else if (distance <= 1.3f)
        {
            CharStats targetStats = target.GetComponent<CharStats>();
            if (targetStats != null)
            {
                 // 타겟 오브젝트의 진영 속성이 다른 경우에만 공격
                if (targetStats.faction != GetComponent<CharStats>().faction)
                {
                    combat.Attack(targetStats);
                }
            }
        }
        else
        {
            nav.SetDestination(transform.position);
        }
    }
}
