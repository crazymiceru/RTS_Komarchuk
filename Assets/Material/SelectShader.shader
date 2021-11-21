Shader "0/Select"
{
    Properties{
        _myTex("Example Texture", 2D) = "white" {}
        _myBump("Bump Texture", 2D) = "bump" {}
        _IntensityBump("Intensity Bump", Range(0,10)) = 1
        _selectColor("Color for Select", Color) = (1,1,1,1)
        _widthSelect("Width Select", Range(0,10)) = 1
        
    } 
        SubShader{

          CGPROGRAM
            #pragma surface surf Lambert

            sampler2D _myTex;
            sampler2D _myBump;
            samplerCUBE _myCube;
            fixed _IntensityBump;
            fixed _widthSelect;
            fixed4 _selectColor;

            struct Input {
                float2 uv_myTex;
                float2 uv_myBump;
                float3 viewDir;
            };

            void surf(Input IN, inout SurfaceOutput o) {

                fixed difinition = dot(IN.viewDir, o.Normal); 
                if (difinition < _widthSelect) o.Emission = _selectColor;
                else {
                    o.Albedo = (tex2D(_myTex, IN.uv_myTex)).rgb;
                    o.Normal = UnpackNormal(tex2D(_myBump, IN.uv_myBump));
                    o.Normal *= float3(_IntensityBump, _IntensityBump, 1);
                }
            }

          ENDCG
        }
            Fallback "Diffuse"
}
