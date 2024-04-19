using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts:")]
    public GrappleRope grappleRope; // 引用另一個腳本的變數，用於處理繩索的功能。
    
    [Header("Layer Settings:")]
    [SerializeField] private bool grappleToAll = false; // 是否可以抓取所有物件的布林值。
    [SerializeField] private int grappableLayerNumber = 9; // 可抓取的物件所在的圖層編號。

    [Header("Main Camera")]
    public Camera m_camera; // 主攝影機的引用。

    [Header("Transform References:")]
    public Transform gunHolder; // 抓取槍的持有者的位置。
    public Transform gunPivot; // 抓取槍的底座的位置。
    public Transform firePoint; // 抓取槍的發射點的位置。

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true; // 是否允許旋轉，並且是否以時間進行平滑旋轉。
    [Range(0, 80)] [SerializeField] private float rotationSpeed = 4; // 旋轉速度的範圍。

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = true; // 是否有最大距離的布林值。
    [SerializeField] private float maxDistance = 4; // 最大抓取距離。

    [Header("Launching")]
    [SerializeField] private bool launchToPoint = true; // 是否將角色發射到抓取點的布林值。
    [SerializeField] private LaunchType Launch_Type = LaunchType.Transform_Launch; // 發射的方式，可能是Transform或Physics方式。
    [Range(0, 5)] [SerializeField] private float launchSpeed = 5; // 發射速度。

    [Header("No Launch To Point")]
    [SerializeField] private bool autoCongifureDistance = false; // 是否自動配置距離的布林值。
    [SerializeField] private float targetDistance = 3; // 目標距離。
    [SerializeField] private float targetFrequency = 3; // 目標頻率。

    // 定義了一個列舉型別，表示發射的方式。
    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch,
    }

    [Header("Component References:")]
    public SpringJoint2D m_springJoint2D; // 彈簧關節的引用。

    [HideInInspector] public Vector2 grapplePoint; // 抓取點的位置。
    [HideInInspector] public Vector2 DistanceVector; // 距離向量。
    Vector2 Mouse_FirePoint_DistanceVector; // 滑鼠位置和發射點之間的距離向量。

    public Rigidbody2D ballRigidbody; // 球的剛體。

    // 在物件啟動時執行的函數。
    private void Start()
    {
        grappleRope.enabled = false; // 關閉繩索功能。
        m_springJoint2D.enabled = false; // 關閉彈簧關節功能。
        ballRigidbody.gravityScale = 1; // 設定球的重力。
    }

    // 在每一幀更新時執行的函數。
    private void Update()
    {
        // 計算滑鼠位置和發射點之間的距離向量。
        Mouse_FirePoint_DistanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SetGrapplePoint(); // 設定抓取點。
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false); // 如果正在使用繩索，則旋轉抓取槍。
            }
            else
            {
                RotateGun(m_camera.ScreenToWorldPoint(Input.mousePosition), false); // 否則，根據滑鼠位置旋轉抓取槍。
            }

            if (launchToPoint && grappleRope.isGrappling)
            {
                if (Launch_Type == LaunchType.Transform_Launch)
                {
                    gunHolder.position = Vector3.Lerp(gunHolder.position, grapplePoint, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grappleRope.enabled = false; // 關閉繩索功能。
            m_springJoint2D.enabled = false; // 關閉彈簧關節功能。
            ballRigidbody.gravityScale = 1; // 恢復球的重力。
        }
        else
        {
            RotateGun(m_camera.ScreenToWorldPoint(Input.mousePosition), true); // 如果沒有按下滑鼠按鈕，則根據滑鼠位置旋轉抓取槍。
        }
    }

    // 用於旋轉抓取槍的函數。
    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            Quaternion startRotation = gunPivot.rotation;
            gunPivot.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // 設定抓取點的函數。
    void SetGrapplePoint()
    {
        if (Physics2D.Raycast(firePoint.position, Mouse_FirePoint_DistanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, Mouse_FirePoint_DistanceVector.normalized);
            if ((_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll) && ((Vector2.Distance(_hit.point, firePoint.position) <= maxDistance) || !hasMaxDistance))
            {
                grapplePoint = _hit.point;
                DistanceVector = grapplePoint - (Vector2)gunPivot.position;
                grappleRope.enabled = true; // 啟用繩索功能。
            }
        }
    }

    // 執行抓取操作的函數。
    public void Grapple()
    {
        if (!launchToPoint && !autoCongifureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }

        if (!launchToPoint)
        {
            if (autoCongifureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }
            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            if (Launch_Type == LaunchType.Transform_Launch)
            {
                ballRigidbody.gravityScale = 0;
                ballRigidbody.velocity = Vector2.zero;
            }
            if (Launch_Type == LaunchType.Physics_Launch)
            {
                m_springJoint2D.connectedAnchor = grapplePoint;
                m_springJoint2D.distance = 0;
                m_springJoint2D.frequency = launchSpeed;
                m_springJoint2D.enabled = true;
            }
        }
    }

    // 在Scene視圖中繪製Gizmos的函數，以可視化最大抓取距離。
    private void OnDrawGizmos()
    {
        if (hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
