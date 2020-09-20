using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public Animator anim; 
    SpriteRenderer sr;
    public Rigidbody rb;
    public float speed;
    public Vector3 minVel;
    public Vector3 movement;
    public bool velMaxX = false;
    public bool velMaxZ = false;
    public float moveHorizontal;
    public float moveVertical;
    public float maximumVelocityX = 10f;
    public float maximumVelocityZ = 8f;
    public float Xvelocity;
    public float Yvelocity;
    // Use this for initialization
    public float contadorTiempoParado;

    //variables para detectar gameobjects
    private Vector3 direction;
    private Vector3 origin;
    public LayerMask layerMask;
    public float sphereRadius;
    public float maxDistance;
    private float currentHitDistance;
    public GameObject currentHitObject;
    //variables para detectar gameobjects

    public Sprite maxRangeSprite1;
    public Sprite maxRangeSprite2;
    public Sprite maxRangeSprite3;
    public Sprite maxRangeSprite4;
    SpriteRenderer thisSpriteRenderer;

    public GameObject attack2Gameobject;
    BoxCollider Attack1Collider;
    BoxCollider Attack2Collider; 
    public bool sePuedeAtacar = true; 
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        minVel = new Vector3(0.01f, 0, 0);
        Attack1Collider = GetComponentInChildren<BoxCollider>();
        Attack2Collider = GetComponentInChildren<BoxCollider>();
        Attack2Collider = GameObject.Find("Attack2Collider").GetComponent<BoxCollider>();
        Attack1Collider = GameObject.Find("Attack1Collider").GetComponent<BoxCollider>();
       // attack2Gameobject = attack2Gameobject.GetComponent<GameObject>();
        //attack2Gameobject = GameObject.Find("Attack2Collider").GetComponent<GameObject>();
        thisSpriteRenderer = GetComponent<SpriteRenderer>();

    }
  
    // Update is called once per frame
    void Update()
    {


        if(maxRangeSprite1 == thisSpriteRenderer.sprite || maxRangeSprite2 == thisSpriteRenderer.sprite || maxRangeSprite3 == thisSpriteRenderer.sprite|| maxRangeSprite4 == thisSpriteRenderer.sprite)
        {
            attack2Gameobject.SetActive(true);

        }
        else
        {
            attack2Gameobject.SetActive(false);
        }






        if(moveHorizontal != 0)
        {
            sr.flipX = moveHorizontal > 0;
           
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (!sr.flipX)                                                                                                                                        //
        {                                                                                                                                                     //
                                                                                                                                                              //
            Attack1Collider.center = new Vector3(-0.29f, Attack1Collider.center.y, Attack1Collider.center.z);
            //Debug.Log((Vector3)Attack1Collider.center);
            Attack2Collider.center = new Vector3(-0.29f, Attack2Collider.center.y, Attack2Collider.center.z);

        }                                                                      //condiciones para mover collider de Attack a la izq o der
        else
        {
           
            Attack1Collider.center = new Vector3(0.29f, Attack1Collider.center.y, Attack1Collider.center.z);                                                   //
            //Debug.Log((Vector3)Attack1Collider.center);                                              
            Attack2Collider.center = new Vector3(0.29f, Attack2Collider.center.y, Attack2Collider.center.z);//
        }                                                                                                                                                      //  
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if (Input.GetButtonDown("BotonA") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack1") &&
           !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHit"))
        {
            sePuedeAtacar = true;
            anim.SetTrigger("ATTACK1");
            
            
        }
        if (Input.GetButtonDown("BotonB") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack1") &&
           !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack2") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHit"))// && attack2Gameobject.activeSelf == true)
        {
            sePuedeAtacar = true;
            anim.SetTrigger("ATTACK2");

        }

        if(moveHorizontal == 0 && moveVertical == 0)
        {
            contadorTiempoParado += Time.deltaTime;
        }
        else
        {
            contadorTiempoParado = 0;
            sePuedeAtacar = true;
        }


        if (contadorTiempoParado >= 5f)
        {
            sePuedeAtacar = false; 
        }

        ///SPHERECAST PARA DETECTAR ENEMIGO A DISTANSIA 

        //origin = transform.position;
        //direction = Vector3.right;
        //RaycastHit hit;

        //if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        //{
        //    currentHitObject = hit.transform.gameObject;
        //    currentHitDistance = hit.distance;
        //}
        //else
        //{            
        //    currentHitDistance = maxDistance;
        //    currentHitObject = null;
        //}

    }
    ///SPHERECAST PARA DETECTAR ENEMIGO POR EJEMPLO


    void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.cyan;
        // Gizmos.DrawLine(origin, origin + direction * currentHitDistance); //raycast de la diressio
        // Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius); // bola de la punta del raycast anterior

    }



    private void FixedUpdate()
    {
         moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical); //direccion del movimiento del jugador
        rb.AddForce(movement * speed * Time.deltaTime);//addforce para que se mueva y eso
        if(moveHorizontal == 0)
        {
            rb.velocity = new Vector3(0, 0, rb.velocity.z);  //comprobar si el usr no mueve el joystick, si es asi que el Player no se mueva en el eje z

        }
        if (moveVertical == 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, 0);  //comprobar si el usr no mueve el joystick, si es asi que el Player no se mueva en el eje x
            
        }
        if (rb.velocity.x > maximumVelocityX)  
        {
            rb.velocity = new Vector3(maximumVelocityX, 0, rb.velocity.z); //si la velocidad x es mayor que la maximax, setear la vel.x =  maximax y aplicarla en rb.velocity
        }

        if (rb.velocity.x < -maximumVelocityX)
        {
            rb.velocity = new Vector3(-maximumVelocityX, 0, rb.velocity.z); //si la velocidad x es menor que la -maximax, setear la vel.x =  -maximax y aplicarla en rb.velocity
        }

        if (rb.velocity.z > maximumVelocityZ)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, maximumVelocityZ);//si la velocidad z es mayor que la maximaz, setear la vel.z =  maximaz y aplicarla en rb.velocity
        }
        if (rb.velocity.z < -maximumVelocityZ)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, -maximumVelocityZ);//si la velocidad z es menor que la -maximaz, setear la vel.z =  -maximaz y aplicarla en rb.velocity
        }
        Xvelocity = rb.velocity.x; //usado para ver en el inspector la vel.x
        Yvelocity = rb.velocity.y; // usado para ver en el inspector la vel.z


    }
}