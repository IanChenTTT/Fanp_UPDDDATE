using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportNext : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            SceneManager.LoadSceneAsync("end");
    }
}
