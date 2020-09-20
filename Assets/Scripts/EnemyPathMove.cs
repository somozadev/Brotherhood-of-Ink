using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[RequireComponent(typeof(Rigidbody))]
public class EnemyPathMove : MonoBehaviour {

	Rigidbody rb;
	SpriteRenderer sr;
	Animator anim;
	public enum States { patrol, pursuit }
	public States state = States.patrol;
	public float searchRange =1.5f;
	public float stoppingDistance = 0.5f;
	public Transform player;
	Vector3 target;
	public float moveHorizontalIA;
	public float moveVerticalIA;
	LayerMask layerMask;
	RaycastHit hit;
	GameObject objectInCollisionWithRaycast;
    public LifesCounterAll lifesCounterAll;
    public Attack1ColliderScript attack1colliderscript;
    public Attack2ColliderScript attack2ColliderScript;



	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
        attack1colliderscript = attack1colliderscript.GetComponent<Attack1ColliderScript>();
        lifesCounterAll = lifesCounterAll.GetComponent<LifesCounterAll>();
        attack2ColliderScript = attack2ColliderScript.GetComponent<Attack2ColliderScript>();

    }
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		InvokeRepeating("SetTarget", 0, 3);

	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, searchRange);
		Gizmos.DrawWireSphere(target, 1f);
	   
   
	}

	void SetTarget()
	{
		if(state == States.pursuit)
		{
			return;
		}
		//esto de abajo solo ocurre si el estado esta en patrol 
		target = new Vector3(transform.position.x + Random.Range(-searchRange, searchRange), transform.position.y, Random.Range(-searchRange, searchRange));// (-6.7f, 7.5f));
		//Debug.Log("Target: " + target + "Vs PlayerPos: " + (Vector3)player.transform.position);
	}




	Vector3 vel;
	void Update() {

        if(lifesCounterAll.enemyTestLifes <= 0)
        {
            Destroy(this.gameObject);
        }

        if(attack1colliderscript.enemigoInvulnerable == true)
        {
            state = States.patrol;
        }


		if (state == States.pursuit)
		{
			target = player.position;
			if (Vector3.Distance(target, transform.position) > searchRange )//* 1.2f)
			{
				target = transform.position;
				state = States.patrol;
				return;
			}
		}
		else if (state == States.patrol)
		{

			//if PATROL 
			//target = player.position;
			if(Vector3.Distance(player.position, transform.position) <= searchRange && attack1colliderscript.enemigoInvulnerable == false)
			{
				state = States.pursuit;
				return;
			}







			//var ob = Physics.SphereCast(transform.position, searchRange, new Vector3(1,1,1), out hit,  Mathf.Infinity, layerMask , QueryTriggerInteraction.UseGlobal);//Physics.SphereCast(new Ray(transform.position, Vector3.forward), searchRange, 100f);

			//objectInCollisionWithRaycast = hit.transform.gameObject;
			//if(hit.collider.CompareTag("Player"))
			//{
			//    Debug.Log("Plauer loca");
			//    state = States.pursuit;
			//    return;
			//}
			//if(objectInCollisionWithRaycast.name == "Player")
			//{
			//    Debug.Log("Player Localized");
			//    state = States.pursuit;
			//    return;
			//}




   //         if (ob.collider != null)
			//{
			//	if (ob.collider.CompareTag("Player"))               //esta parte es la que no funciona

			//	{
   //                 Debug.Log("Player Localized");
			//		state = States.pursuit;
			//		return;
			//	}
			//}
		}

		vel = target - transform.position;
		sr.flipX = vel.x > 0;

		if (vel.magnitude < stoppingDistance)
		{
			vel = Vector3.zero; 
		}
		vel.Normalize();

		rb.velocity = new Vector3(vel.x * moveHorizontalIA, vel.y, vel.z * moveVerticalIA);
		

	}
}
