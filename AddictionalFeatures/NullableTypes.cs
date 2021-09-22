using System;

namespace AddictionalFeatures
{
    public class NullableTypes
    {
        
        static public void LocalMain()
        {
            int? z1 = 5;
            z1 = null;
            bool? enabled1 = null;
            Double? d1 = 3.3;
            //is the same as
            Nullable<int> z2 = null;
            Nullable<bool> enabled2 = null;
            Nullable<Double> d2 = 3.3;

            // Nullable<Country> country = null;//Он и так nullable, ERROR
            Nullable<State> country = null; //ok
            Console.WriteLine(z2 == z1);
        }
    }

    class Country
    {
        public string Name { get; set; }
    }

    struct State
    {
        public string Name { get; set; }
    }
}