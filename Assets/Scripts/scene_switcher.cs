using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_switcher : MonoBehaviour
{
    // 用于碰撞检测 完成场景转换的index 和下面index一个意思
    public int scene_trans;

    // 直接载入某个场景 场景编号scene_index
    // scene_index 在Unity File-build settings中自行定义
    public void OpenScene(int scene_index)
    {
        SceneManager.LoadScene(scene_index);
    }

    // 碰撞检测的场景切换
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            SceneManager.LoadScene(scene_trans);
        }
        
    }


}
