using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GunTrapState
{
    TIME,
    FOLLOW,
}
public class GunTrap : MonoBehaviour
{
    [SerializeField] private GunTrapState state;
    [SerializeField] private float eachShotDuration = 1f;

    [SerializeField] GameObject TrapFirePoint;
    [SerializeField] GameObject MyPrefab;
    private int counter;
    private float timer;
    void Update(){
        timer += Time.deltaTime;
        if(timer > eachShotDuration)
        {
            counter++;
            if(counter >= 2)
            {
                counter=0;
            }
            else
            {
                StartCoroutine(ShootTime(Random.Range(.1f, .2f)));
            }
            timer = 0;
        }
    }
    IEnumerator ShootTime(float RndNum){
        yield return new WaitForSeconds(RndNum);
        GameObject obj = Instantiate(MyPrefab, TrapFirePoint.transform.position, Quaternion.identity);
        obj.transform.parent = transform;
        obj.GetComponent<Rigidbody2D>().velocity = Vector2.right * 5;
    }
    // Simple rayCast 2d hit player
    private void GunFollow()
    {

    }
}

