using UnityEngine;

public class Cleaner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag != "Player")
            Destroy(collision.gameObject);
    }
}
