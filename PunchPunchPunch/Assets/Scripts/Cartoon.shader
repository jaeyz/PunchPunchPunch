Shader "custom/OutlineBase" {
    Properties {
        _NotVisibleColor ("Invisible Color (RGB)", Color) = (0.3,0.3,0.3)
        _OutlineColor ("Outline Color", Color) = (0,0,0)  
        _MainTex("Base (RGB)", 2D) = "white" {}
        _Outline ("Extrusion Amount", Range(0,0.05)) = 0.05
    }
    SubShader {
    
        Tags { "Queue" = "Transparent" }
            Lighting Off
            Zwrite off

        Pass {
            ZTest Less
            
            CGPROGRAM
                
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
                
                fixed _Outline;
                fixed4 _OutlineColor;
                
                struct a2v
                {
                    fixed4 vertex : POSITION;
                    fixed3 normal : NORMAL;
                };
 
                struct v2f
                {
                    fixed4 pos : POSITION;
                };
 
                fixed4 frag (v2f IN) : COLOR
                {
                    return _OutlineColor;
                }
                
                v2f vert (a2v v)
                {
                    v2f o;
                    o.pos = mul( UNITY_MATRIX_MVP, v.vertex + ( fixed4(v.normal, 0) * _Outline));
                    return o;
                }
            ENDCG
        }
        
        
        Pass
        {
        	Zwrite On
            SetTexture[_MainTex]
        }
        
        
        
//        Tags { "Queue" = "Opaque" }
//        ZTest LEqual
//        ZWrite On
//        Lighting On
//        
//        CGPROGRAM
//        #pragma surface surf
//        #pragma target 3.0
//
//        sampler2D _MainTex;
//
//        struct Input {
//            fixed2 uv_MainTex;
//        };
//    
//        void surf (Input IN, inout SurfaceOutput o) {
//            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
//        }
//        ENDCG  
    } 
    FallBack "VertexLit"
}