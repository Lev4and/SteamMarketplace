namespace SteamMarketplace.Model.Cryptography
{
    public static class RC4
    {
        public static byte[] Apply(byte[] data, byte[] key)
        {
            int[] S = new int[256];

            for (int _ = 0; _ < 256; _++)
            {
                S[_] = _;
            }

            int[] T = new int[256];

            if (key.Length == 256)
            {
                Buffer.BlockCopy(key, 0, T, 0, key.Length);
            }
            else
            {
                for (int _ = 0; _ < 256; _++)
                {
                    T[_] = key[_ % key.Length];
                }
            }

            int i = 0;
            int j = 0;

            for (i = 0; i < 256; i++)
            {
                j = (j + S[i] + T[i]) % 256;

                int temp = S[i];

                S[i] = S[j];
                S[j] = temp;
            }

            i = j = 0;

            byte[] result = new byte[data.Length];

            for (int iteration = 0; iteration < data.Length; iteration++)
            {
                i = (i + 1) % 256;
                j = (j + S[i]) % 256;

                int temp = S[i];

                S[i] = S[j];
                S[j] = temp;

                int K = S[(S[i] + S[j]) % 256];

                result[iteration] = Convert.ToByte(data[iteration] ^ K);
            }

            return result;
        }
    }
}
