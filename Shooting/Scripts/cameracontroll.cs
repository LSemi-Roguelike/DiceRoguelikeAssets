using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroll : MonoBehaviour
{
    public GameObject bulletPrefab; //弾のプレハブ
    public float power = 300.0f; //弾の威力
    public float speed = 0.05f; //カメラアングルの移動速度

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //rotateCameraの呼び出し
        //rotateCamera();
    }

    void Update()
    {
        //左クリック
        if (Input.GetMouseButtonDown (0)) 
        {
            //弾を生成
            GameObject bullet = Instantiate(bulletPrefab,this.transform.position,transform.rotation) as GameObject;
            //弾が前方に飛ぶように力を加える
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * power);
        }
    }

    private void rotateCamera()
    {
        //マウスカーソルの位置を取得
        Vector3 mousePos = Input.mousePosition;

        //中央の位置に合わせる(マウス座標の開始位置は画面左下なので調整が必要)
        mousePos.x = mousePos.x - ( Screen.width / 2 );
        mousePos.y = mousePos.y - ( Screen.height / 2 );

        //カメラのアングルを取得
        Vector3 cameraAngle = transform.eulerAngles;

        cameraAngle.x = -mousePos.y * speed; //マウスを横に動かしたとき
        cameraAngle.y = mousePos.x * speed; //マウスを縦に動かしたとき
        cameraAngle.z = 0; //ｚは変化しないので0にする

        //動かした変化をカメラのアングルに適用させる
        transform.eulerAngles = cameraAngle;
    }
}
