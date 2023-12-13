using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldMapStrategyKit;
using YemaekHan;

public class UnitStat : MonoBehaviour
{
    public WMSK map;
    private int running_Time;
    public int Running_Time // Detect value change
    {
        get { return running_Time; }
        set
        {
            if (running_Time != value)
            {
                running_Time = value;
                // Script
                Scanning();
            }

            if (clickedUnit != null)
            {
                if (AttackableUnit == clickedUnit)
                {
                    Attack(AttackableUnit);
                }
            }
        }
    }

    public GameObjectAnimator unit;
    public Faction faction;                            //����
    public int factionID;
    public Army army;

    private float maxHealth;                     //�ִ� ü��
    public float curHealth { get; private set; }       //���� ü��
    public float attackRange = 0.3f;

    private int damage;
    private int defense;
    private bool AttackPermission = false;

    private int unitID;
    private Renderer unitRenderer;
    private LineRenderer lineRenderer;

    [SerializeField]
    private LayerMask unitLayerMask;
    private Transform clickedUnit = null;
    private Transform AttackableUnit = null;

    public void Start()
    {
        map = WMSK.instance;
        damage = army.ArmyDamage();
        Debug.Log( army.ID + "군" + " :" + army.ArmyDamage());

        unitRenderer = gameObject.GetComponent<Renderer>();
        UnitColorChange();
    }

    private void Update()
    {
        Running_Time = DisplayTime.instance.realTime;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            // Raycasting을 통해 유닛 선택
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, unitLayerMask))
            {
                if (hitInfo.transform == transform)
                {
                    clickedUnit = hitInfo.transform;
                }
            }
        }
    }

    private void Awake()
    {
        curHealth = maxHealth;                         //����ü�� ����
    }

    void setCountryID()
    {
        switch (faction)
        {
            case Faction.Koguryeo:
                factionID = 0;
                break;
            case Faction.Baekje:
                factionID = 1;
                break;
            case Faction.Sila:
                factionID = 3;
                break;
            case Faction.Gaya:
                factionID = 2;
                break;
        }
    }

    private void Scanning()
    {
        // 적 유닛 검색
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider enemy in enemies)
        {
            // UnitStat 스크립트를 가진 오브젝트만 탐색
            UnitStat enemyUnitStat = enemy.gameObject.GetComponent<UnitStat>();
            if (enemyUnitStat != null)
            {
                // 적 유닛의 faction 값을 가져옴
                Faction enemyFaction = enemyUnitStat.faction;

                // 적 유닛의 faction이 자신과 다를 경우 공격
                if (enemyFaction != faction)
                {
                    //자신국가와 전쟁 상황일 때 공격
                    if (map.GetCountry(factionID).attrib["relationState"][enemyUnitStat.factionID] == 2)
                    {
                        // 적 유닛이 공격 범위 내에 있는지 확인
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy <= attackRange)
                        {
                            if(AttackPermission == true)
                            {
                                AttackableUnit = enemy.transform;

                                //AI유닛이면 자동공격
                                if(enemy.gameObject.GetComponent<AIUnitSelect>()!=null)
                                {
                                    Attack(enemy.transform);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void Attack(Transform target)
    {
        if (target != null)
        {
            Debug.Log( faction + " 의 " + unitID + " 가 " + target.GetComponent<UnitStat>().faction + " 의 " + target.GetComponent<UnitStat>().unitID +"을 공격합니다!");
            target.gameObject.GetComponent<UnitStat>().TakeDamage(damage);
        }
    }
    
    public void TakeDamage(int damage)                 //������ ���
    {
        curHealth -= damage / (defense * 0.5f);
        if (curHealth <= 0)
        {
            Die();
            Debug.Log( faction + " 의 " + unitID + "군이 전멸했습니다.");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void UnitColorChange()
    {
        switch (faction)
        {
            case Faction.Koguryeo:
                unitRenderer.material.color = Color.red;
                break;
            case Faction.Baekje:
                unitRenderer.material.color = Color.yellow;
                break;
            case Faction.Sila:
                unitRenderer.material.color = Color.blue;
                break;
            default:
                unitRenderer.material.color = Color.green;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public int GetArmyDamage()
    {
        return damage;
    }
}

public enum Faction
{
    Koguryeo = 1,
    Baekje = 2,
    Sila = 3,
    Gaya = 4
}

public enum UNIT_TYPE
{
    AIR = 1
}