Shader "Unlit/TilemapShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GridWidth("Grid width", Int) = 16
        _GridHeight("Grid height", Int) = 8
        _CellTypeCount("Cell type count", Int) = 72
        _CellTypeRowCount("Cell type row count", Int) = 9
        _CellTypeColumnCount("Cell type column count", Int) = 8
        _DrawingAccuracy("Drawing accuracy", Float) = 0.1
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True"}
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _GridWidth;
            int _GridHeight;
            int _CellTypeCount;
            int _CellTypeRowCount;
            int _CellTypeColumnCount;
            float _DrawingAccuracy;
            Buffer<int> _GridValuesBuffer;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Get grid value
                uint xFloor = floor(i.uv[0] * _GridWidth);
                uint yFloor = floor(i.uv[1] * _GridHeight);
                uint valueIndexFloor = xFloor + yFloor * _GridWidth;
                uint value = _GridValuesBuffer[valueIndexFloor]; // value € [1]

                // Get sprite UV offset
                float xOffset = clamp(i.uv[0] * _GridWidth - xFloor, _DrawingAccuracy, 1 - _DrawingAccuracy); // xOffset € [0;1[
                float yOffset = clamp(i.uv[1] * _GridHeight - yFloor, _DrawingAccuracy, 1 - _DrawingAccuracy); // yOffset € [0;1[

                // Get sprite UV for grid value, value=0 => type=None
                uint type = value - 1;
                float spriteXUV = value <= 0 ? 0 : (type % _CellTypeRowCount + xOffset) * (1.0 / _CellTypeRowCount);
                float spriteYUV = value <= 0 ? 0 : (type / _CellTypeRowCount + (1 - yOffset)) * (1.0 / _CellTypeColumnCount);

                // sample the texture
                fixed4 col = tex2D(_MainTex, float2(spriteXUV, 1 - spriteYUV));
                if (value <= 0) col[3] = 0; // value=0 => type=None => transparent

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
