using System;
using UnityEngine;

namespace TPS
{
	public class SkySphere : MonoBehaviour
	{
		[Header("Sky Sphere")]
		[SerializeField] MeshRenderer skyRenderer = default;

		[Header("Override Settings")] 
		[SerializeField] float sunHeight = 0;
		[SerializeField] float horizonFallOff = 0;
		[SerializeField] Color zenithColor;
		[SerializeField] Color horizonColor;
		[SerializeField] Color cloudColor;
		[SerializeField] Color overallColor;


		[Header("Parameters")]
		[SerializeField] Material skyMaterial;

		[SerializeField] bool refreshMaterial;
		[SerializeField] Light directionalLight;
		[SerializeField] bool determineColorsBySunPosition = true;
		[SerializeField] float sunBrightness;
		[SerializeField] float cloudSpeed;
		[SerializeField] float cloudOpacity;
		[SerializeField] float starsBrightness;
		[SerializeField] Gradient horizonColorCurve;
		[SerializeField] Gradient zenithColorCurve;
		[SerializeField] Gradient cloudColorCurve;

		[HideInInspector] [SerializeField] Material instancedMaterial;

		[ContextMenu("Construct")]
		void OnConstruction()
		{
			instancedMaterial = new Material(skyMaterial);
			skyRenderer.sharedMaterial = instancedMaterial;

			if (refreshMaterial)
			{
				refreshMaterial = false;
				RefreshMaterial();
			}
		}

		[ContextMenu("Update Sun Direction")]
		void UpdateSunDirection()
		{
			float yDirection = directionalLight.transform.rotation.eulerAngles.y;
			instancedMaterial.SetVector("Vector4_3CF69D23", new Vector4(0, yDirection, 0));
			instancedMaterial.SetColor("Color_F34AAAA", directionalLight.color);
			sunHeight = MapRangeUnclamped(0, -90, 0, 1, directionalLight.transform.position.y);

			// curves
			
		}

		[ContextMenu("Refresh")]
		void RefreshMaterial()
		{
			
		}

		float MapRangeUnclamped(float value, float inRangeA, float inRangeB, float outRangeA, float outRangeB)
		{
			return GetMappedRangeValueUnclamped(new Vector2(inRangeA, inRangeB), new Vector2(outRangeA, outRangeB),
				value);
		}

		float GetMappedRangeValueUnclamped(Vector2 inputRange, Vector2 outputRange, float Value)
		{
			return GetRangeValue(outputRange, GetRangePct(inputRange, Value));
		}

		float GetRangePct(Vector2 inputRange, float value)
		{
			float divisor = inputRange.y - inputRange.x;
			if (Mathf.Epsilon > divisor)
			{
				return value > inputRange.y ? 1f : 0;
			}

			return (value - inputRange.x) / divisor;
		}

		float GetRangeValue(Vector2 outputRange, float getRangePct)
		{
			return Mathf.Lerp(outputRange.x, outputRange.y, getRangePct);
		}
	}
}