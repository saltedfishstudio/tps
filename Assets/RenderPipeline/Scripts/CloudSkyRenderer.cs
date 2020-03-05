using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class CloudSkyRenderer : SkyRenderer
{
	public static readonly int _cubeMap = Shader.PropertyToID("_Cubemap");
	public static readonly int _skyParam = Shader.PropertyToID("_SkyParam");
	public static readonly int _pixelCoordToViewDirWS = Shader.PropertyToID("_PixelCoordToViewDirWS");

	Material m_newSkyMaterial;
	MaterialPropertyBlock m_propertyBlock = new MaterialPropertyBlock();

	static int m_renderCubemapID = 0;
	static int m_renderFullscreenSkyID = 1;
	
	
	public override void Build()
	{
		m_newSkyMaterial = CoreUtils.CreateEngineMaterial(GetNewSkyShader());
	}

	public override void Cleanup()
	{
		CoreUtils.Destroy(m_newSkyMaterial);
	}

	protected override bool Update(BuiltinSkyParameters builtinParams)
	{
		return false;
	}

	public override void RenderSky(BuiltinSkyParameters builtinParams, bool renderForCubemap, bool renderSunDisk)
	{
		using (new ProfilingSample(builtinParams.commandBuffer, "Draw sky"))
		{
			var newSky = builtinParams.skySettings as CloudSky;

			int passID = renderForCubemap ? m_renderCubemapID : m_renderFullscreenSkyID;

			float intensity = GetSkyIntensity(newSky, builtinParams.debugSettings);
			float phi = -Mathf.Deg2Rad * newSky.rotation.value; // -rotation to match Legacy
			
			m_propertyBlock.SetTexture(_cubeMap, newSky.hdriSky.value);
			m_propertyBlock.SetVector(_skyParam, new Vector4(intensity, 0.0f, Mathf.Cos(phi), Mathf.Sin(phi)));
			m_propertyBlock.SetMatrix(_pixelCoordToViewDirWS, builtinParams.pixelCoordToViewDirMatrix);

			CoreUtils.DrawFullScreen(builtinParams.commandBuffer, m_newSkyMaterial, m_propertyBlock, passID);
		}
	}

	Shader GetNewSkyShader()
	{
		// implementation
		return Shader.Find("Hidden/HDRP/Sky/CloudSky");
	}
}