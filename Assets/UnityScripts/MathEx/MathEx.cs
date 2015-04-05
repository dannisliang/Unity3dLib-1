using UnityEngine;
using System.Collections;

namespace ZUtil
{
	public class MathEx {

		
		public static Matrix4x4 TranslationMatrix(float x, float y, float z) {
			Matrix4x4 mat = new Matrix4x4();
			mat = Matrix4x4.identity;
			mat.m03 = x;
			mat.m13 = y;
			mat.m23 = z;
			return mat;
		}
		
		public static Matrix4x4 ScalingMatrix(float x, float y, float z) {
			Matrix4x4 mat = new Matrix4x4();
			mat = Matrix4x4.identity;
			mat.m00 = x;
			mat.m11 = y;
			mat.m22 = z;
			return mat;
		}
		
		public static Matrix4x4 AxisRotationMatrix(float ax, float ay, float az, float degreeAngle) {
			float radianAngle = degreeAngle * ((float)System.Math.PI / 180.0f);
			Matrix4x4 mat = new Matrix4x4();
			mat = Matrix4x4.identity;
			
			float cos = Mathf.Cos(radianAngle);
			float sin = Mathf.Sin(radianAngle);
			
			mat.m00 = cos + ((ax * ax) * (1.0f - cos));
			mat.m01 = ((ax * ay) * (1.0f - cos)) - (az * sin);
			mat.m02 = ((ax * az) * (1.0f - cos)) + (ay * sin);
			
			mat.m10 = ((ay * ax) * (1.0f - cos)) + sin;
			mat.m11 = cos + ((ay * ay) * (1.0f - cos));
			mat.m12 = ((ay * az) * (1.0f - cos)) - (ax * sin);
			
			mat.m20 = ((az * ax) * (1.0f - cos)) - (ay * sin);
			mat.m21 = ((az * ay) * (1.0f - cos)) + (ax * sin);
			mat.m22 = cos + ((az * az) * (1.0f - cos));
			
			return mat;
		}
	}

}

