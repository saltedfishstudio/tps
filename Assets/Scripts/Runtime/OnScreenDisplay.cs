using UnityEngine;
using UnityEngine.UI;

namespace TPS
{
	public class OnScreenDisplay : MonoBehaviour
	{
		[SerializeField] Text text;
		static OnScreenDisplay instance;

		void Awake()
		{
			instance = this;
		}

		public static void Display(string str)
		{
			instance.text.text = str;
		}
	}
}