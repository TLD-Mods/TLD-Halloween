namespace TLDHalloween
{
	internal class Skeletons
	{
		internal static string[] skulls = { "Head_Candle_A", "Head_Candle_B", "Head_Candle_C", "Head_Candle_D", "Skeleton_head" };
		internal static string[] limbs = { "Skeleton_left_arm_A", "Skeleton_left_arm_B", "Skeleton_left_leg_A", "Skeleton_left_leg_B", "Skeleton_right_arm_A", "Skeleton_right_arm_B", "Skeleton_right_leg_A", "Skeleton_right_leg_B" };
		internal static string[] body = { "Skeleton", "Skeleton_pelvis_ribs" };


		public static string RandomSkull()
		{
			return skulls[UnityEngine.Random.Range(0, skulls.Length)];
		}
		public static string RandomLimb()
		{
			return limbs[UnityEngine.Random.Range(0, limbs.Length)];
		}

		public static string Random()
		{
			string[] combined = skulls.Union(limbs).Union(body).ToArray();
			return combined[UnityEngine.Random.Range(0, combined.Length)];
		}

	}
}
