using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cubodeouro player = collision.gameObject.GetComponent<cubodeouro>();

            if (player != null)
            {
                player.respawnPosition = transform.position + new Vector3(0, 1, 0);
                Debug.Log("Checkpoint salvo!");
            }
        }
    }
}