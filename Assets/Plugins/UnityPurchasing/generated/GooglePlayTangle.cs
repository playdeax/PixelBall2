#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Orm3uIg6ubK6Orm5uAjndR7UlbtzbWNPyuCFP64TwLvd1GmlwGE/REMtkl5z+bsJi0f6WW4OnqyI2eQHGr/oeO8VtDqhHZ/USThzWe+ODmfbAxGay5xAFyJGkF6q1uvuZI7h749//3tWm84l6S9KYr6uhbb5gkV9AOHmDVi7VzvbrudXAtYS4s4KFRojWOj1ZGWWF0nW9eTnAFpZpKMX/in2Mi1KwKphAr0PR4KsJT2bXOvjBOt0x0tloQH+QBvGeBVWBOUmcIE6B0+dF2ICJq8Vc6pwYIO8LYcHuBn9I69oJYBoUdUxh5f5Yx6xwjEkTQ1RuYihIZUKO95yYq+wc+2BVwGIOrmaiLW+sZI+8D5Ptbm5ub24u1tFxZl3L04RC7q7ubi5");
        private static int[] order = new int[] { 1,6,4,12,10,5,9,13,12,10,13,12,12,13,14 };
        private static int key = 184;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
