Shader "Custom/GrayScaleWithBackground"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        // 灰度参数
        _GrayStrength ("Gray Strength", Range(0, 1)) = 1.0
        _GrayMode ("Gray Mode", Float) = 0
        
        // 背景颜色（新增）
        _BackgroundColor ("Background Color", Color) = (0.5, 0.5, 0.5, 1)
        [Toggle] _ShowBackground ("Show Background", Float) = 1
        
        // Mask 相关
        [Toggle] _UseMask ("Use Sprite Mask", Float) = 1
        
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
    }

    SubShader
    {
        Tags 
        { 
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            Name "GrayScaleWithMask"
            
            Stencil
            {
                Ref 1
                Comp Equal
                Pass Keep
                Fail Keep
                ZFail Keep
            }

            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment GrayScaleFrag
            #pragma target 2.0

            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA

            #include "UnitySprites.cginc"

            // 自定义属性
            float _GrayStrength;
            float _GrayMode;
            float4 _BackgroundColor;
            float _ShowBackground;

            // 灰度计算函数
            float3 ConvertToGray(float3 color, float mode)
            {
                float gray = dot(color.rgb, float3(0.333, 0.333, 0.333));
                
                if (mode < 0.5) // 标准灰度
                {
                    return gray.rrr;
                }
                else if (mode < 1.5) // 保留红色
                {
                    return float3(color.r, gray, gray);
                }
                else if (mode < 2.5) // 保留绿色
                {
                    return float3(gray, color.g, gray);
                }
                else if (mode < 3.5) // 保留蓝色
                {
                    return float3(gray, gray, color.b);
                }
                else // 负片
                {
                    return 1.0 - gray.rrr;
                }
            }

            fixed4 GrayScaleFrag(v2f IN) : SV_Target
            {
                // 获取原始颜色
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                
                // 如果像素完全透明，且启用背景填充
                if (c.a < 0.01 && _ShowBackground > 0.5)
                {
                    // 返回灰色背景（注意保持透明度为1）
                    float gray = dot(_BackgroundColor.rgb, float3(0.333, 0.333, 0.333));
                    c.rgb = lerp(_BackgroundColor.rgb, gray.rrr, _GrayStrength);
                    c.a = 1.0; // 背景不透明
                    return c;
                }
                
                c.rgb *= c.a;

                // 应用灰度
                float3 grayColor = ConvertToGray(c.rgb, _GrayMode);
                c.rgb = lerp(c.rgb, grayColor, _GrayStrength);
                
                return c;
            }
            ENDCG
        }
    }

    Fallback "Sprites/Default"
}