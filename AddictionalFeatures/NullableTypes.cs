using System;

namespace AddictionalFeatures
{
    public class NullableTypes
    {
        public static void LocalMain()
        {
            int? z1 = 5;
            z1 = null;
            bool? enabled1 = null;
            double? d1 = 3.3;
            //is the same as
            Nullable<int> z2 = null;
            Nullable<bool> enabled2 = null;
            Nullable<double> d2 = 3.3;

            // Nullable<Country> country = null;//Он и так nullable, ERROR
            Nullable<State> country = null; //ok
            Console.WriteLine(z2 == z1);
        }
    }

    internal class Country
    {
        public string Name { get; set; }
    }

    internal struct State
    {
        public string Name { get; set; }
    }
}