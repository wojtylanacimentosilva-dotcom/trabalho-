using UnityEngine;

public class espinho : MonoBehaviour
{
    public Vector3 respawnPosition = new Vector3(-14f, 2f, 4f);

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            collision.transform.position = respawnPosition;

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }
}