using UnityEngine;

public class cubodeouro : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityMultiplier = 3f;
    public float jumpForce = 5f;
    public float moveSpeed = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("d"))
        {
            transform.Translate(new Vector3(moveSpeed, 0, 0));
        }
        
        if(Input.GetKey("a"))
        {
            transform.Translate(new Vector3(-moveSpeed, 0, 0));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
    }

    // Apply additional gravity in FixedUpdate so it affects physics properly
    void FixedUpdate()
    {
        if (rb != null && gravityMultiplier != 1f)
        {
            // Apply extra gravity force so total gravity = Physics.gravity * gravityMultiplier
            Vector3 extraGravity = Physics.gravity * (gravityMultiplier - 1f) * rb.mass;
            rb.AddForce(extraGravity, ForceMode.Force);
        }
    }

}