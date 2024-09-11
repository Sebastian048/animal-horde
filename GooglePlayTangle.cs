#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("ZNZVdmRZUl1+0hzSo1lVVVVRVFccjIVFL+KfX7lAINd8F4pz96GtRLQjcHy2T1VcBF5KLEQ2ASZxp+B9dHZydvR4uA62HvnrYoF7TnrU4wVaDrb6iBNdqie5Td/pBpkbKNpL89ZVW1Rk1lVeVtZVVVTWDis76P5VRx+0x3nRgFeuQ63WtuYM0t+RBXlJRuGmvPjXoTz/8bL2VpihbCe6cEPhGT3hi7i7KBIWNetWNNjfXXcuLghcr3VRmb0yyutTkjzFaf8vkTv1mXi+w4dLdXOgg9LschKRGFOjwzI5hIHLK6u0nYOxLlS5RSiD0N5g+7rNjFtIRgoKAlw1EYFz0kKttFgnH6HJnzl/sP2M9gtBjSH8Gp79Uc2S1RjQVcZfFVZXVVRV");
        private static int[] order = new int[] { 0,4,7,13,11,11,12,11,12,12,12,11,13,13,14 };
        private static int key = 84;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
