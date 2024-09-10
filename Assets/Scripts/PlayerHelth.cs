using System;
using UnityEngine;

public class PlayerHelth : MonoBehaviour
{
    public int health = 100;
    public static bool death  = false;
    public float force = 150f, forceTorque = 100f;
    public RectTransform healthBar;

    [NonSerialized] public static bool isGoods = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        if (!isGoods)
        {
            health -= damage;
            healthBar.offsetMax = new Vector2(-1f * 430f * (100f - health) / 100f, 0f);
            if (health <= 0 && !death)
            {
                death = true;
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(Vector3.up * force);
                rb.AddTorque(Vector3.back * forceTorque);
                gameObject.tag = "Finish";
            }
        }
    }
}
