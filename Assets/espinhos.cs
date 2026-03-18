using UnityEngine;

public class Espinho : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cubodeouro player = collision.gameObject.GetComponent<cubodeouro>();

            if (player != null)
            {
                // Teleporta pro último checkpoint salvo
                player.transform.position = player.respawnPosition;

                // Reseta velocidade
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                }
            }
        }
    }
}