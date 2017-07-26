Shader "Tang/test"{


	SubShader{
		pass {
		CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

		void vert(in float2 objPos:POSITION,out float4 pos:POSITION) {
			pos = float4(objPos,0,1);
		}

		void frag(inout float4 col:COLOR) {
		   col = float4(0,0,1,1);
		}
		ENDCG
	  }
	}


}