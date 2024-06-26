﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBones
{
    static class RandomGen
    {
        private static Random random;

        static RandomGen()
        {
            random = new Random();
        }

        public static int GetRandomInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public static int GetRandomInt()
        {
            return random.Next();
        }

        public static float GetRandomFloat()
        {
            return (float)random.NextDouble();
        }
    }
}
