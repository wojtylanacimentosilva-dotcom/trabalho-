using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string mensagem = "Checkpoint ativado!";
    private float tempo = 0f;

    void Start()
    {
        // garante que é trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cubodeouro player = other.GetComponent<cubodeouro>();

            if (player != null)
            {
                player.respawnPosition = transform.position + new Vector3(0, 1, 0);

                Debug.Log(mensagem);

                // ativa mensagem na tela
                tempo = 2f;
            }
        }
    }

    void OnGUI()
    {
        if (tempo > 0)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(
                new Rect(0, Screen.height / 2 - 50, Screen.width, 100),
                mensagem,
                style
            );

            tempo -= Time.deltaTime;
        }
    }
}