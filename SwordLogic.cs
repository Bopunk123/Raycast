using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLogic : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim)
        {
            SwordAttacking(Input.GetButton("Fire2"));
        }
    }

    void WeaponHit() {
        Debug.Log("Hit");
    }

    public void SwordAttacking(bool isAttacking) {
        anim.SetBool("isAttacking", isAttacking);
    }
}
