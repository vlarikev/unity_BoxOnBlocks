using System.Collections;
using UnityEngine;

public class PlatformIce : MonoBehaviour
{
    private bool isDelay = false;
    private int AttackToCrash = 3;

    private void Update()
    {
        if (AttackToCrash == 2)
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(112 / 255f, 173 / 255f, 1, 0.75f);
        if (AttackToCrash == 1)
            gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(112 / 255f, 173 / 255f, 1, 0.5f);
        if (AttackToCrash <= 0)
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isDelay)
        {
            StartCoroutine("Delay");
            AttackToCrash--;
        }
    }
    
    private IEnumerator Delay()
    {
        isDelay = true;
        yield return new WaitForSeconds(.3f);
        isDelay = false;
    }
}
