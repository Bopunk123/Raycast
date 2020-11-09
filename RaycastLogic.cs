using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum PlayerStates
{
    Idle,
    Moving,
    AttackMoving
}
public class RaycastLogic : MonoBehaviour
{
   

    NavMeshAgent navMeshAgent;
    [SerializeField]
    GameObject clickObject;

    float clickObjSize;
    float meleeRange = 1.5f;

    SwordLogic swordLogic;

    PlayerStates playerState = PlayerStates.Idle;

    GameObject enemyTarget;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        swordLogic = GetComponentInChildren<SwordLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            RaycastCameraToMouse();
        }

        if (clickObject && clickObjSize > 0) {
            clickObjSize -= Time.deltaTime;
            clickObject.transform.localScale = clickObjSize * Vector3.one;
        }

        if (enemyTarget && playerState == PlayerStates.AttackMoving)
        {
            navMeshAgent.SetDestination(enemyTarget.transform.position);
            navMeshAgent.isStopped = false;
        }
        CheckAttackRange();

       
    }

    void RaycastCameraToMouse()
    {
       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("we hit an object: " + hit.collider.gameObject.name + "at: " + hit.point);

            navMeshAgent.SetDestination(hit.point);
            navMeshAgent.isStopped = false;
            DisplayClickObj(hit.point);

             if (hit.collider.gameObject.tag == "Enemy")
            {
                enemyTarget = hit.collider.gameObject;
                playerState = PlayerStates.AttackMoving;
            }
            else
            {
                enemyTarget = null;
                playerState = PlayerStates.Moving;
            }
        }

       
    }

    void DisplayClickObj(Vector3 pos) {
        if (clickObject)
        {
            clickObjSize = 1.0f;
            clickObject.transform.localScale = Vector3.one;
            clickObject.transform.position = pos;
        }
    }

    void CheckAttackRange() {
        if (!enemyTarget || playerState != PlayerStates.AttackMoving)
        {
            return;
        }
        Debug.DrawLine(transform.position,transform.forward * 1.5f,Color.red);

        RaycastHit hit;
        Ray ray = new Ray(transform.position,transform.forward);
        if (Physics.Raycast(ray,out hit,meleeRange)) {
            if (hit.collider.gameObject.tag == "Enemy") {
                if (swordLogic)
                {
                    swordLogic.SwordAttacking(true);
                }
                navMeshAgent.isStopped = true;
            }
        }
    }
}
