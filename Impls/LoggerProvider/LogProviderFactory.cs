using UnityEngine;

namespace Cr7Sund.Logger
{
    public static class LogProviderFactory
    {
        public static ILogProvider Create()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                return new UnityEditorLogProvider();
            }
#endif

            return new SerilogProvider();
        }
    }
}