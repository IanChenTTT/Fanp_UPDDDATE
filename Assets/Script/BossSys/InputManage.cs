using UnityEngine;

public class InputManage : MonoBehaviour
{
    public static InputManage Instance { get; private set;}
    private bool isPressSpcae;
    private void Start(){
        if (Instance != null) //SingleTon 唯一物件
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    private void Update(){
        isPressSpcae = Input.GetKey(KeyCode.Space);
    }
    public bool OnPressSpcae(){
        return isPressSpcae;
    }
}
