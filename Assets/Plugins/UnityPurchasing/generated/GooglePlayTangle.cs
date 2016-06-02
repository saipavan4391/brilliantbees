#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("T/1+XU9yeXZV+Tf5iHJ+fn56f3z9fnB/T/1+dX39fn5/kmGzbGho3Yu0+rWjwZepO7wHgBOEqOd9wdogAwyNSh0k7UEs0F9mpoJN20ujjJkYIKhq+VRgz97qHcGAFZ8OM4xt0VZSkgJ1qpcIF6mDSBOEIcx8oi9l+SKQ2ty8ulatLFMJlWv7RgR3QHr52Dv7yoiIepJwP5mq0Tpvc9452B1IezEUkyKUb/UjGdXQTkcjpiU0EoDb1VU4kx4TnH7Lbt2WeDb7OCRbvfJcVWob9nU8P/cNMtteI/ClyQUcVLZtaUMftp5FMQ8TlWXZekgnbIzMkH1PXDuuPiNRXiTZS5Npg16JaeIKvJA7GRiV7h8ZD6DjeZHLHyBgTxKrOtgzVn18fn9+");
        private static int[] order = new int[] { 0,1,7,9,4,7,6,12,10,13,12,13,13,13,14 };
        private static int key = 127;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
