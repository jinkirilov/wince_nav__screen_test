using System;

public class SHA256ManagedSimple
{
    private static readonly uint[] K = {
        0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
        0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
        0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
        0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
        0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
        0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
        0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
        0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
    };

    public static byte[] ComputeHash(byte[] input)
    {
        uint h0 = 0x6a09e667, h1 = 0xbb67ae85, h2 = 0x3c6ef372, h3 = 0xa54ff53a;
        uint h5 = 0x9b05688c, h4 = 0x510e527f, h6 = 0x1f83d9ab, h7 = 0x5be0cd19;

        long totalBits = input.Length * 8L;
        int paddingBytes = (int)((56 - (input.Length + 1) % 64 + 64) % 64);
        byte[] padded = new byte[input.Length + 1 + paddingBytes + 8];

        Array.Copy(input, 0, padded, 0, input.Length);
        padded[input.Length] = 0x80;

        for (int i = 0; i < 8; i++)
            padded[padded.Length - 1 - i] = (byte)(totalBits >> (i * 8));

        uint[] w = new uint[64];
        for (int chunk = 0; chunk < padded.Length; chunk += 64)
        {
            for (int t = 0; t < 16; t++)
            {
                int pos = chunk + t * 4;
                w[t] = ((uint)padded[pos] << 24) | ((uint)padded[pos + 1] << 16) | ((uint)padded[pos + 2] << 8) | padded[pos + 3];
            }

            for (int t = 16; t < 64; t++)
            {
                uint s0 = ((w[t - 15] >> 7) | (w[t - 15] << 25)) ^ ((w[t - 15] >> 18) | (w[t - 15] << 14)) ^ (w[t - 15] >> 3);
                uint s1 = ((w[t - 2] >> 17) | (w[t - 2] << 15)) ^ ((w[t - 2] >> 19) | (w[t - 2] << 13)) ^ (w[t - 2] >> 10);
                w[t] = w[t - 16] + s0 + w[t - 7] + s1;
            }

            uint a = h0, b = h1, c = h2, d = h3, e = h4, f = h5, g = h6, h = h7;

            for (int t = 0; t < 64; t++)
            {
                uint S1 = ((e >> 6) | (e << 26)) ^ ((e >> 11) | (e << 21)) ^ ((e >> 25) | (e << 7));
                uint ch = (e & f) ^ ((~e) & g);
                uint temp1 = h + S1 + ch + K[t] + w[t];
                uint S0 = ((a >> 2) | (a << 30)) ^ ((a >> 13) | (a << 19)) ^ ((a >> 22) | (a << 10));
                uint maj = (a & b) ^ (a & c) ^ (b & c);
                uint temp2 = S0 + maj;

                h = g; g = f; f = e; e = d + temp1; d = c; c = b; b = a; a = temp1 + temp2;
            }

            h0 += a; h1 += b; h2 += c; h3 += d; h4 += e; h5 += f; h6 += g; h7 += h;
        }

        byte[] result = new byte[32];
        uint[] hashes = { h0, h1, h2, h3, h4, h5, h6, h7 };
        for (int i = 0; i < 8; i++)
        {
            result[i * 4] = (byte)(hashes[i] >> 24);
            result[i * 4 + 1] = (byte)(hashes[i] >> 16);
            result[i * 4 + 2] = (byte)(hashes[i] >> 8);
            result[i * 4 + 3] = (byte)hashes[i];
        }
        return result;
    }
}
