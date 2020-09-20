
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1ColliderScript : MonoBehaviour {
    public PlayerMovement playermovement;
    public float contadorEnemigoInvulnerable;
    public bool enemigoInvulnerable = false; 
    public SpriteRenderer enemy1;
    public Animator anim;
    public LifesCounterAll lifesCounterAll;
    // Use this for initialization
    void Start () {
        playermovement = playermovement.GetComponent<PlayerMovement>() ;
        enemy1 = enemy1.GetComponent<SpriteRenderer>();
        lifesCounterAll = lifesCounterAll.GetComponent<LifesCounterAll>();

	}
	
	// Update is called once per frame
	void Update () {

        if(enemy1){ 
            if (enemigoInvulnerable == false)
            {

                enemy1.color = Color.white;
                contadorEnemigoInvulnerable = 0;
           
            }
            if(enemigoInvulnerable == true)
            {
                enemy1.color = Color.red; 
                contadorEnemigoInvulnerable += Time.deltaTime;
           
                if (contadorEnemigoInvulnerable >= 1.5f)
                {
                    enemigoInvulnerable = false;
                }
            }
        }

    }

   


    private void OnTriggerStay( Collider other)
    {

        if(enemigoInvulnerable == false)
        {

            if (other.name == "Enemy1")
            {
                Debug.Log(other.name + "ha entrado en la zona de " + this.name);

                if (playermovement.sePuedeAtacar == true && anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack1"))
                {

                    //other.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    Debug.Log("ENEMIGO RECIBIO DAÑO");
                    enemigoInvulnerable = true;
                    playermovement.sePuedeAtacar = false;
                    lifesCounterAll.enemyTestLifes--;
                }
                else
                {
                    Debug.Log("NO ESTA ACTIVA LA ANIMACHIONE");
                }


            }
        }


    }


}
