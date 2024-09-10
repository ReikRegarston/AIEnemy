using UnityEngine;
using System.Collections;

namespace AstronautPlayer
{

	public class AstronautPlayer : MonoBehaviour {

		private Animator _anim;
		private Rigidbody _rb;

		public float speed = 600.0f;
		public float turnSpeed = 400.0f;
		public float jumpForce = 500f;
		public float radius = 5f;
		public int damage = 20;


        void Start () {
			_rb = GetComponent <Rigidbody>();
			_anim = gameObject.GetComponentInChildren<Animator>();
		}

		void Update (){
			if (PlayerHelth.death)
				return;

			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
				_anim.SetInteger ("AnimationPar", 1);
			else 
				_anim.SetInteger ("AnimationPar", 0);

			float turn = Input.GetAxis("Horizontal");
			transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);

			DetectorColliion();
		}

        private void FixedUpdate()
        {
			if(PlayerHelth.death)
				return;

			if(_rb.velocity.y == 0)
				_anim.SetBool("isJump", false);

			if (Input.GetKeyDown(KeyCode.Space) && _rb.velocity.y == 0)
			{ 
				_rb.AddForce(Vector3.up * jumpForce);
				_anim.SetTrigger("Jumping");
				_anim.SetBool("isJump", true);
			}
            float v = Input.GetAxis("Vertical") * speed * Time.deltaTime;
			_rb.MovePosition(transform.position + transform.forward * v);
        }
        private void DetectorColliion()
        {
            Collider[] hitCollider = Physics.OverlapSphere(transform.position, radius);

			foreach (var el in hitCollider)
			{
				if (el.CompareTag("Enemy") && Input.GetMouseButtonUp(0))
				{
					_anim.SetTrigger("IsAttack");
					el.GetComponent<EnemyHealth>().TakeDamage(damage);

				}

			}
        }
    }
}
