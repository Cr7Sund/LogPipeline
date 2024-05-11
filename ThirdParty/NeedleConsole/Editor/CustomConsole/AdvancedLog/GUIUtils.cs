using UnityEngine;

namespace Needle.Console
{
	internal static class GUIUtils
	{
		private static Material _simpleColored;
		private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
		private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
		private static readonly int Cull = Shader.PropertyToID("_Cull");
		private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");
		internal static Material SimpleColored
		{
			get {
				if (!_simpleColored)
				{
					var shader = Shader.Find("Hidden/Internal-Colored");
					_simpleColored = new Material(shader)
					{
						hideFlags = HideFlags.HideAndDontSave
					};
					_simpleColored.SetInt(SrcBlend, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
					_simpleColored.SetInt(DstBlend, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					_simpleColored.SetInt(Cull, (int)UnityEngine.Rendering.CullMode.Off);
					_simpleColored.SetInt(ZWrite, 0);
				}
				return _simpleColored;
			}
		}
	}
}