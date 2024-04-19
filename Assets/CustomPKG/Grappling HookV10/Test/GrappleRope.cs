using UnityEngine;

public class GrappleRope : MonoBehaviour
{
     [Header("General refrences:")]
     public GrapplingGun grapplingGun; // 引用到 GrapplingGun 腳本的實例，用於取得資訊和呼叫功能
     [SerializeField] LineRenderer m_lineRenderer; // 引用到 LineRenderer 元件，用於繪製繩索

     [Header("General Settings:")]
     [SerializeField] private int percision = 20; // 繩索點的數量，用於繪製繩索的精度
     [Range(0, 100)][SerializeField] private float straightenLineSpeed = 4; // 繩子拉直的速度

     [Header("Animation:")]
     public AnimationCurve ropeAnimationCurve; // 用於繪製繩索動畫的曲線
     [SerializeField] [Range(0.01f, 4)] private float WaveSize = 20; // 繩索波動的大小
     float waveSize;

     [Header("Rope Speed:")]
     public AnimationCurve ropeLaunchSpeedCurve; // 用於繩索發射速度的曲線
     [SerializeField] [Range(1, 50)] private float ropeLaunchSpeedMultiplayer = 4; // 繩索發射速度的倍數

     float moveTime = 0; // 繩索移動的時間

     [SerializeField] public bool isGrappling = false; // 指示是否正在使用繩索

     bool drawLine = true; // 指示是否需要繪製繩線
     bool straightLine = true; // 指示繩子是否為直線狀態

     private void Awake()
     {
         m_lineRenderer = GetComponent<LineRenderer>(); // 取得 LineRenderer 元件的引用
         m_lineRenderer.enabled = false; // 停用 LineRenderer 元件
         m_lineRenderer.positionCount = percision; // 設定繩索點的數量
         waveSize = WaveSize; // 設定初始波動大小
     }

     private void OnEnable()
     {
         moveTime = 0; // 重置繩索運動時間
         m_lineRenderer.enabled = true; // 啟用 LineRenderer 元件
         m_lineRenderer.positionCount = percision; // 設定繩索點的數量
         waveSize = WaveSize; // 設定波動大小
         straightLine = false; // 將繩索狀態設為非直線狀態
         LinePointToFirePoint(); // 將繩索的起點設定為發射點
     }

     private void OnDisable()
     {
         m_lineRenderer.enabled = false; // 停用 LineRenderer 元件
         isGrappling = false; // 指示不再使用繩索
     }

     void LinePointToFirePoint()
     {
         for (int i = 0; i < percision; i++)
         {
             m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position); // 將繩子的所有點位置設定為發射點的位置
         }
     }

     void Update()
     {
         moveTime += Time.deltaTime; // 更新繩索運動時間

         if (drawLine)
         {
             DrawRope(); // 繪製繩索
         }
     }

     void DrawRope()
     {
         if (!straightLine)
         {
             if (m_lineRenderer.GetPosition(percision - 1).x != grapplingGun.grapplePoint.x)
             {
                 DrawRopeWaves(); // 繪製帶有波動的繩索
             }
             else
             {
                 straightLine = true; // 如果繩子已經拉直，將繩索狀態設為直線狀態
             }
         }
         else
         {
             if (!isGrappling)
             {
                 grapplingGun.Grapple(); // 呼叫 GrapplingGun 的 Grapple 方法，開始使用繩索
                 isGrappling = true; // 指示正在使用繩索
             }
             if (waveSize > 0)
             {
                 waveSize -= Time.deltaTime * straightenLineSpeed; // 縮小波動大小，使繩索逐漸變直
                 DrawRopeWaves(); // 繪製帶有波動的繩索
             }
             else
             {
                 waveSize = 0; // 波動大小為零，繩子變直
                 DrawRopeNoWaves(); // 繪製無波動的繩索
             }
         }
     }

     void DrawRopeWaves()
     {
         for (int i = 0; i < percision; i++)
         {
             float delta = (float)i / ((float)percision - 1f); // 計算插值參數
             Vector2 offset = Vector2.Perpendicular(grapplingGun.DistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize; // 計算波浪偏移量
             Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset; // 計算目標位置
             Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(moveTime) * ropeLaunchSpeedMultiplayer); // 計算目前位置

             m_lineRenderer.SetPosition(i, currentPosition); // 設定繩索點的位置
         }
     }

     void DrawRopeNoWaves()
     {
         m_lineRenderer.positionCount = 2; // 只設定兩點來繪製直線
         m_lineRenderer.SetPosition(0, grapplingGun.grapplePoint); // 設定繩子的終點位置
         m_lineRenderer.SetPosition(1, grapplingGun.firePoint.position); // 設定繩子的起點位置
     }
}