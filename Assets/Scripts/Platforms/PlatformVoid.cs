using System.Collections;
using UnityEngine;

public class PlatformVoid : MonoBehaviour
{
    private bool isOnce = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isOnce)
        {
            StartCoroutine("Delay");
        }
    }
    private IEnumerator Delay()
    {
        isOnce = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(76 / 255f, 49 / 255f, 95 / 255f, 0.8f);

        yield return new WaitForSeconds(5);
        gameObject.GetComponentInChildren<Collider2D>().enabled = false;
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(76 / 255f, 49 / 255f, 95 / 255f, 0.3f);

        yield return new WaitForSeconds(2);
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(76 / 255f, 49 / 255f, 95 / 255f, 1f);

        isOnce = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
