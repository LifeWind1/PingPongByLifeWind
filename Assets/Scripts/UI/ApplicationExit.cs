using UnityEngine;

namespace UI
{
    public class ApplicationExit : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}