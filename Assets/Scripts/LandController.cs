using UnityEngine;

public class LandController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerGFX;
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject[] platformGreenList;
    [SerializeField] private GameObject[] platformRedList;
    [SerializeField] private GameObject[] platformBlueList;
    [SerializeField] private GameObject[] platformPurpleList;
    [SerializeField] private GameObject[] platformYellowList;

    private float platformPos = 15;
    private int randomTemp;
    private int randomPrevious;

    private int currentBiomeGFX;
    private int currentBiomeGen;
    private int previousBiomeGen;
    private int playerTracker = 5;

    private void Start()
    {
        currentBiomeGen = 0;

        Instantiate(platformGreenList[3], new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(platformGreenList[1], new Vector3(0, 5, 0), Quaternion.identity);
        Instantiate(platformGreenList[2], new Vector3(0, 10, 0), Quaternion.identity);
    }

    private void Update()
    {
        // Biome changer.
        if (player.transform.position.y > playerTracker)
        {
            do
                currentBiomeGen = Random.Range(0, 5);
            while (currentBiomeGen == previousBiomeGen);
            previousBiomeGen = currentBiomeGen;
            playerTracker += Random.Range(30, 50);
        }

        // Camera biome.
        if (currentBiomeGFX == 0)
        {
            cam.backgroundColor = new Color(142 / 255f, 176 / 255f, 85 / 255f);
            playerGFX.GetComponent<SpriteRenderer>().color = new Color(237 / 255f, 253 / 255f, 209 / 255f);
        }
        if (currentBiomeGFX == 1)
        {
            cam.backgroundColor = new Color(148 / 255f, 1 / 255f, 23 / 255f, 1);
            playerGFX.GetComponent<SpriteRenderer>().color = new Color(245 / 255f, 195 / 255f, 203 / 255f);
        }
        if (currentBiomeGFX == 2)
        {
            cam.backgroundColor = new Color(76 / 255f, 153 / 255f, 255 / 255f, 1);
            playerGFX.GetComponent<SpriteRenderer>().color = new Color(214 / 255f, 232 / 255f, 255 / 255f);
        }
        if (currentBiomeGFX == 3)
        {
            cam.backgroundColor = new Color(179 / 255f, 115 / 255f, 222 / 255f, 1);
            playerGFX.GetComponent<SpriteRenderer>().color = new Color(237 / 255f, 227 / 255f, 244 / 255f);
        }
        if (currentBiomeGFX == 4)
        {
            cam.backgroundColor = new Color(247 / 255f, 198 / 255f, 0f, 1);
            playerGFX.GetComponent<SpriteRenderer>().color = new Color(253 / 255f, 245 / 255f, 212 / 255f);
        }

        // Platforms + Biomes spawn.
        if (player.transform.position.y + 15 > platformPos)
        {
            if (currentBiomeGen == 0)
            {
                do
                    randomTemp = Random.Range(0, platformGreenList.Length);
                while (randomTemp == randomPrevious);

                Instantiate(platformGreenList[randomTemp], new Vector3(0, Random.Range(platformPos - 1.5f, platformPos + 1.5f), 0), Quaternion.identity);
                platformPos += 5;
            }
            if (currentBiomeGen == 1)
            {
                do
                    randomTemp = Random.Range(0, platformRedList.Length);
                while (randomTemp == randomPrevious);

                Instantiate(platformRedList[randomTemp], new Vector3(0, Random.Range(platformPos - 1f, platformPos + 1f), 0), Quaternion.identity);
                platformPos += 6.5f;
            }
            if (currentBiomeGen == 2)
            {
                do
                    randomTemp = Random.Range(0, platformBlueList.Length);
                while (randomTemp == randomPrevious);

                Instantiate(platformBlueList[randomTemp], new Vector3(0, Random.Range(platformPos - 2f, platformPos + 2f), 0), Quaternion.identity);
                platformPos += 5.5f;
            }
            if (currentBiomeGen == 3)
            {
                do
                    randomTemp = Random.Range(0, platformPurpleList.Length);
                while (randomTemp == randomPrevious);

                Instantiate(platformPurpleList[randomTemp], new Vector3(0, Random.Range(platformPos - 0.5f, platformPos + 1f), 0), Quaternion.identity);
                platformPos += 4.5f;
            }
            if (currentBiomeGen == 4)
            {
                do
                    randomTemp = Random.Range(0, platformYellowList.Length);
                while (randomTemp == randomPrevious);

                GameObject clone = Instantiate(platformYellowList[randomTemp], new Vector3(0, Random.Range(platformPos - 1f, platformPos + 1f), 0), Quaternion.identity);
                clone.SetActive(true);
                platformPos += 4.5f;
            }
            randomPrevious = randomTemp;
        }
    }

    public void BiomeGFX(int value)
    {
        currentBiomeGFX = value;
    }
}
