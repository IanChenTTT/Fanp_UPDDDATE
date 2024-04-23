using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }
       IEnumerator Fade()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
