using UnityEditor;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void PutDown()
    {
#if UNITY_EDITOR
        // 编辑器模式下：停止 Play Mode
        EditorApplication.isPlaying = false;
#else
            // 打包后：退出应用程序
            Application.Quit();
#endif
    }
}
