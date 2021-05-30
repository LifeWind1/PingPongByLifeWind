using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference m_scene;

        public void LoadScene()
        {
            m_scene.LoadSceneAsync();
        }
    }
}