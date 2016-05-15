using System;

namespace ds_agent_oriented_simulation.Generators
{
    class EmpiricGenerator
    {
        // generator nasad
        private static  Random seeder;
        private static double number;

        public static void InitializeSeed(Random seed)
        {
            seeder = seed;
            
        }

        public int GetSampleA()
        {
            number = seeder.NextDouble();

            if (number >= 0 &&  number < 0.0033085)
            {
                return 5;
            } else if (number >= 0.0033085 && number < 0.0128205)
            {
                return 6;
            }
            else if (number >= 0.0128205 && number < 0.0384615)
            {
                return 7;
            }
            else if (number >= 0.0384615 && number < 0.0500414)
            {
                return 8;
            }
            else if (number >= 0.0500414 && number < 0.0624483)
            {
                return 9;
            }
            else if (number >= 0.0624483 && number < 0.0744417)
            {
                return 10;
            }
            else if (number >= 0.0744417 && number < 0.0959471)
            {
                return 11;
            }
            else if (number >= 0.0959471 && number < 0.1066998)
            {
                return 12;
            }
            else if (number >= 0.1066998 && number < 0.1306865)
            {
                return 13;
            }
            else if (number >= 0.1306865 && number < 0.1488834)
            {
                return 14;
            }
            else if (number >= 0.1488834 && number < 0.1600496)
            {
                return 15;
            }
            else if (number >= 0.1600496 && number < 0.115798)
            {
                return 16;
            }
                return 19;
        }
        public int GetSampleB()
        {
            number = seeder.NextDouble();

            if (number >= 0 && number < 0.0003509)
            {
                return 6;
            }
            else if (number >= 0.0003509 && number < 0.0010526)
            {
                return 7;
            }
            else if (number >= 0.0010526 && number < 0.0028070)
            {
                return 8;
            }
            else if (number >= 0.0028070 && number < 0.0031579)
            {
                return 9;
            }
            else if (number >= 0.0031579 && number < 0.0045614)
            {
                return 10;
            }
            else if (number >= 0.0045614 && number < 0.0073684)
            {
                return 11;
            }
            else if (number >= 0.0073684 && number < 0.0070175)
            {
                return 12;
            }
            else if (number >= 0.0070175 && number < 0.0077193)
            {
                return 13;
            }
            else if (number >= 0.0077193 && number < 0.0115789)
            {
                return 14;
            }
            else if (number >= 0.0115789 && number < 0.0105263)
            {
                return 15;
            }
            else if (number >= 0.0105263 && number < 0.0638596)
            {
                return 16;
            }
            else if (number >= 0.0638596 && number < 0.1828070)
            {
                return 17;
            }
            else if (number >= 0.1828070 && number < 0.1940351)
            {
                return 18;
            }
            else if (number >= 0.1940351 && number < 0.2098246)
            {
                return 19;
            }
            else if (number >= 0.2098246 && number < 0.2224561)
            {
                return 20;
            }
                return 21;
        }
        public int GgetSampleC()
        {
            return 0;
        }

    }
}
