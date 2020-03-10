Shader "Custom/sample"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white"{}//   _MainTex ("Albedo (RGB)", 2D) = "white" {}
    
    }
    SubShader
    {
	   Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade//fullforwardshadows
		#pragma target 3.0



        struct Input
        {
            float2 uv_MainTex;
        };
		sampler2D _MainTex;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = (c.r*0.3 + c.g*0.6 + c.b*0.1 < 0.2) ? 1 : 0.7;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

		}
        ENDCG
    }
    FallBack "Diffuse"
}
