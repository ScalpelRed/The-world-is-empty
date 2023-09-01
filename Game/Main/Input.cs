using System.Numerics;
using Silk.NET.Input;

using Game.Graphics;

namespace Game.Main
{
	public class Input
	{
		
		private readonly IInputContext InputContext;

		public event Action<Key>? KeyDown;
		public event Action<Key>? KeyUp;
		public event Action<char>? CharDown;
		public event Action<Vector2>? MouseMove;
		public event Action<MouseButton, Vector2>? MouseDown;
		public event Action<MouseButton, Vector2>? MouseUp;

		private Vector2 MousePosition;

		private readonly OpenGL Gl;

		public Input(OpenGL gl)
		{
			Gl = gl;
			InputContext = gl.View.CreateInput();
			RefreshDevices();
		}

		private void RefreshDevices()
        {
			foreach (IKeyboard v in InputContext.Keyboards)
			{
				v.KeyDown += (IKeyboard _, Key key, int _) => KeyDown?.Invoke(key);
				v.KeyUp += (IKeyboard _, Key key, int _) => KeyUp?.Invoke(key);
				v.KeyChar += (IKeyboard _, char c) => CharDown?.Invoke(c);
			}

			foreach (IMouse v in InputContext.Mice)
			{
				v.MouseMove += (IMouse _, Vector2 pos) =>
				{
					MousePosition = pos - Gl.ScreenSize * 0.5f;
					MouseMove?.Invoke(MousePosition);
				};
				v.MouseDown += (IMouse _, MouseButton button) => MouseDown?.Invoke(button, MousePosition);
				v.MouseUp += (IMouse _, MouseButton button) => MouseUp?.Invoke(button, MousePosition);
			}
		}
	}
}
