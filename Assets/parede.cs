using UnityEngine;

public class parede : MonoBehaviour
{
    private BoxCollider col;

    void Start()
    {
        col = GetComponent<BoxCollider>();

        if (col == null)
        {
            col = gameObject.AddComponent<BoxCollider>();
        }
    }
}