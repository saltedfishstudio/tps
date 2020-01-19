using UnityEngine;

namespace Epic.Engine.GameFramework
{
	public abstract class Movement : MonoBehaviour
	{
		/** Current velocity of updated component. */
		public Vector3 velocity = default;

		protected Vector3 planeConstraintNormal = default;
		
		protected Vector3 planeConstraintOrigin = default;

		/// <summary>
		/// Helper to compute the plane constraint axis from the current setting.
		/// </summary>
		/// <param name="axisSetting">AxisSetting Setting to use when computing the axis.</param>
		/// <returns>Plane constraint axis/normal.</returns>
		protected Vector3 GetPlaneConstraintNormalFromAxisSetting(EPlaneConstraintAxisSetting axisSetting)
		{
			return default;
		}
	}
}