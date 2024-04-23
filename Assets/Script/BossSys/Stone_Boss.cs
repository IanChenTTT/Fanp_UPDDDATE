using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// REQUIREMENT !!!
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Stone_Boss : MonoBehaviour
{
    public static int Health = 30;
    Rigidbody2D rb2D;
    public float hAmount = 3f;
    public float vAmount = 6f;
    [SerializeField]
    private bool MoveMent = true;
    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private LayerMask WallLayer;

    private BoxCollider2D boxCollider2D;
    [SerializeField]
    private BoxCollider2D RightColl;

    [SerializeField]
    private BoxCollider2D LeftColl;


    [SerializeField]
    private GameObject DeadOBJ;
    Dictionary<string, Collider2D> Hitenters;
    [SerializeField] private GameObject deadOBJColl;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        Hitenters = new Dictionary<string, Collider2D>();
    }

    void FixedUpdate()
    {
        if (IsGround(true) && MoveMent)
        {
            rb2D.velocity = new Vector2(hAmount, vAmount);
        }
        if(Stone_Boss.Health <= 0)
        {
            DeadOBJ.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            DeadOBJ.SetActive(true);
            

        }
        // StartCoroutine(DebugInfo());
       
    }
    IEnumerator DebugInfo(){

            yield return new WaitForSeconds(2f);
            foreach(KeyValuePair<string, Collider2D> hitKvp in Hitenters){
                Debug.Log("key: "+hitKvp.Key+ " val: " + hitKvp.Value);
            }
    }

    private bool IsGround(bool isDev)
    {
        /**
         * CAST ON TARGET ITSELF
         * Use bound size
        */
        RaycastHit2D boxRayDown = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, -Vector2.up, 0.65f, GroundLayer);
        if (isDev) DebugDrawDownBox();
        if (boxRayDown.collider != null)
        {
            return true;
        }
        return false;

    }
    void OnTriggerEnter2D(Collider2D collision){
     if (collision.gameObject.CompareTag("Wall")) 
        { 
            // Debug.Log("It hit"+collision.gameObject.name);
            Hitenters.TryAdd(collision.gameObject.tag, collision);
            hAmount *= -1;
            rb2D.velocity = new Vector2(hAmount,vAmount*2/3);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            Hitenters.TryAdd(collision.gameObject.name, collision);
            foreach(KeyValuePair<string, Collider2D> hitKvp in Hitenters){
                // Debug.Log("key: "+hitKvp.Key+ " val: " + hitKvp.Value);
                if(hitKvp.Key.Contains("Wall") ){
                   Debug.Log("player should die");
                }
            }
        } 
    }
    void OnTriggerExit2D(Collider2D collision){
        // Debug.Log("It leave "+collision.gameObject.name);
        if(Hitenters.Count <= 0)return;
        // foreach(KeyValuePair<string, Collider2D> hitKVP in Hitenters)
        // {
        //     if (hitKVP.Key.Contains(collision.gameObject.name))
        //         Hitenters.Remove(collision.gameObject.name);
        // }
    }
    //Debug draw Box collidder Simple
    public void DebugDrawDownBox()
    {

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + 0.5f), Color.yellow);

        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + 0.5f), Color.yellow);

        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + 0.5f), Vector2.right * (boxCollider2D.bounds.extents.x * 2f), Color.yellow);
    }
    // DEBUG draw Box collider Advance
    public void DrawBox(Color color,Vector2 direction)
    {
        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = boxCollider2D.bounds.size.x * 0.5f;
        float h = boxCollider2D.bounds.size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += (Vector2)boxCollider2D.bounds.center;
        p2 += (Vector2)boxCollider2D.bounds.center;
        p3 += (Vector2)boxCollider2D.bounds.center;
        p4 += (Vector2)boxCollider2D.bounds.center;

        Vector2 realDistance =direction.normalized * 0.5f;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = color;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
    }

}
