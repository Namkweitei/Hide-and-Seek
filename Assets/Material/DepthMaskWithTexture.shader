Shader "Custom/DepthMaskWithTexture"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }
   SubShader
    {
         Tags {"Queue" = "Transparent+1" }

         Pass{
             Blend Zero One
             }
    }
}