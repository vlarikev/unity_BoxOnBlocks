using UnityEngine;

public class BiomeChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PlatformGreen")
            gameObject.GetComponentInParent<LandController>().BiomeGFX(0);
        if (collision.gameObject.name == "PlatformRed")
            gameObject.GetComponentInParent<LandController>().BiomeGFX(1);
        if (collision.gameObject.name == "PlatformBlue")
            gameObject.GetComponentInParent<LandController>().BiomeGFX(2);
        if (collision.gameObject.name == "PlatformPurple")
            gameObject.GetComponentInParent<LandController>().BiomeGFX(3);
        if (collision.gameObject.name == "PlatformYellow")
            gameObject.GetComponentInParent<LandController>().BiomeGFX(4);
    }
}
