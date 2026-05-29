// Made with Amplify Shader Editor v1.9.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Roughness"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		[Normal]_NormalMap("NormalMap", 2D) = "bump" {}
		_Metallic("Metallic", 2D) = "white" {}
		_Occlusion("Occlusion", 2D) = "white" {}
		_Roughness("Roughness", 2D) = "white" {}
		_MetallicStrenght("MetallicStrenght", Range( 0 , 1)) = 1
		_NormalStrenght("NormalStrenght", Range( 0 , 1)) = 1
		_OcclusionStrenght("OcclusionStrenght", Range( 0 , 1)) = 1
		_RoughnessStrenght("RoughnessStrenght", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform float _NormalStrenght;
		uniform sampler2D _Albedo;
		uniform sampler2D _Metallic;
		uniform float _MetallicStrenght;
		uniform sampler2D _Roughness;
		uniform float _RoughnessStrenght;
		uniform sampler2D _Occlusion;
		uniform float _OcclusionStrenght;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = UnpackScaleNormal( tex2D( _NormalMap, i.uv_texcoord ), _NormalStrenght );
			o.Albedo = tex2D( _Albedo, i.uv_texcoord ).rgb;
			o.Metallic = ( tex2D( _Metallic, i.uv_texcoord ) * _MetallicStrenght ).r;
			o.Smoothness = ( 1.0 - ( tex2D( _Roughness, i.uv_texcoord ).r * _RoughnessStrenght ) );
			o.Occlusion = ( tex2D( _Occlusion, i.uv_texcoord ).r * _OcclusionStrenght );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19200
Node;AmplifyShaderEditor.SamplerNode;7;-542.0334,-14.00001;Inherit;True;Property;_Occlusion;Occlusion;3;0;Create;True;0;0;0;False;0;False;-1;None;fa708e901a58d8e4cb322f1f358444d3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-540.0334,179;Inherit;False;Property;_OcclusionStrenght;OcclusionStrenght;7;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-529.5338,-111.7997;Inherit;False;Property;_RoughnessStrenght;RoughnessStrenght;8;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-977.4191,-557.6351;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-537.0998,-590.8999;Inherit;True;Property;_Metallic;Metallic;2;0;Create;True;0;0;0;False;0;False;-1;None;ab116f6f011452d48a3f59ac3304df19;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-235.0332,-15.00001;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-363.1,-1039.7;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;0;False;0;False;-1;None;7290e0edc9f65eb41854b172db09f81e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-531.5338,-303.7995;Inherit;True;Property;_Roughness;Roughness;4;0;Create;True;0;0;0;False;0;False;-1;None;d39604db04640e14c9fb22241662e71f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-239.5334,-314.7995;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;17;-41.00305,-321.6001;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-529.8998,-390.0999;Inherit;False;Property;_MetallicStrenght;MetallicStrenght;5;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-229.8998,-584.0997;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;777.8999,-572.0995;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Roughness;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SamplerNode;3;-367.2001,-853.5991;Inherit;True;Property;_NormalMap;NormalMap;1;1;[Normal];Create;True;0;0;0;False;0;False;-1;None;fff5c312c32bb884aad5bc48cdc89925;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-789.8605,-850.303;Inherit;False;Property;_NormalStrenght;NormalStrenght;6;0;Create;True;0;0;0;False;0;False;1;0.5;0;1;0;1;FLOAT;0
WireConnection;7;1;14;0
WireConnection;4;1;14;0
WireConnection;9;0;7;1
WireConnection;9;1;8;0
WireConnection;1;1;14;0
WireConnection;10;1;14;0
WireConnection;11;0;10;1
WireConnection;11;1;12;0
WireConnection;17;0;11;0
WireConnection;5;0;4;0
WireConnection;5;1;6;0
WireConnection;24;0;1;0
WireConnection;24;1;3;0
WireConnection;24;3;5;0
WireConnection;24;4;17;0
WireConnection;24;5;9;0
WireConnection;3;1;14;0
WireConnection;3;5;25;0
ASEEND*/
//CHKSM=4D37BC719422FD5F401910435EE9154B3EAEAD60