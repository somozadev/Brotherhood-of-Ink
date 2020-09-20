using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2ColliderScript : MonoBehaviour
{
    public PlayerMovement playermovement;
    public float contadorEnemigoInvulnerable;  
    public SpriteRenderer enemy1;
    public Animator anim;
    public LifesCounterAll lifesCounterAll;
    public Attack1ColliderScript attack1ColliderScript;


    // Use this for initialization
    void Start()
    {
        playermovement = playermovement.GetComponent<PlayerMovement>();
        enemy1 = enemy1.GetComponent<SpriteRenderer>();
        lifesCounterAll = lifesCounterAll.GetComponent<LifesCounterAll>();
        attack1ColliderScript = attack1ColliderScript.GetComponent<Attack1ColliderScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemy1)
        {
            if (attack1ColliderScript.enemigoInvulnerable == false)
            {

                enemy1.color = Color.white;
                contadorEnemigoInvulnerable = 0;

            }
            if (attack1ColliderScript.enemigoInvulnerable == true)
            {

                contadorEnemigoInvulnerable += Time.deltaTime;
                enemy1.color = Color.red; 
                if (contadorEnemigoInvulnerable >= 1.5f)
                {
                    attack1ColliderScript.enemigoInvulnerable = false;
                }
            }
        }
    }




    private void OnTriggerStay(Collider other)
    {

        if (attack1ColliderScript.enemigoInvulnerable == false)
        {

            if (other.name == "Enemy1")
            {
                Debug.Log(other.name + "ha entrado en la zona de " + this.name);

                if (playermovement.sePuedeAtacar == true && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2"))
                {

                    //other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log("ENEMIGO RECIBIO MUCHO DAÑO");
                    attack1ColliderScript.enemigoInvulnerable = true;
                    playermovement.sePuedeAtacar = false;
                    lifesCounterAll.enemyTestLifes-= 3;
                }
                else
                {
                    Debug.Log("NO ESTA ACTIVA LA ANIMACHIONE");
                }


            }
        }


    }


}
