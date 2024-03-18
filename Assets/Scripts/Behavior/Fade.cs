
using UnityEngine;

public class Fade : MonoBehaviour
{   public GameObject player;
    public int resistance = 4;

    void Update(){
        if (GetplayerScore() >= resistance)
        {
            Destroy(gameObject);
        }
    }
    int GetplayerScore()
    {
        PlayerController playerScript = player.GetComponent<PlayerController>();
        if (playerScript != null)
        {
            return playerScript.count;
        }
        return 0;
    }
}


