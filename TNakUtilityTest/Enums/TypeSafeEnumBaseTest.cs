using System.Linq;
using ChainingAssertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TNakUtility.Enums;

namespace TNakUtilityTest.Enums
{
    /// <summary>TypeSafeEnumTest</summary>
    [TestClass]
    public class TypeSafeEnumTest
    {
        /// <summary>CountTest</summary>
        [TestMethod]
        public void CountTest()
        {
			ProgRockBands.Count.Is(8);
        }

        /// <summary>Valueses the test.</summary>
        [TestMethod]
        public void ValuesTest()
        {
            // All
            ProgRockBands.Values.Is(
                ProgRockBands.MoonSafari,
                ProgRockBands.GentleGiant,
                ProgRockBands.Genesis,
                ProgRockBands.CameliasGarden,
                ProgRockBands.IQ,
                ProgRockBands.PFM,
                ProgRockBands.YoninBayashi,
                ProgRockBands.TheFlowerKings);

            // Filter by country
            ProgRockBands.Values.Where(v => v.Country == Country.Sweden)
                .Is(ProgRockBands.MoonSafari, ProgRockBands.TheFlowerKings);
            ProgRockBands.Values.Where(v => v.Country == Country.Italy)
                .Is(ProgRockBands.CameliasGarden, ProgRockBands.PFM);
            ProgRockBands.Values.Where(v => v.Country == Country.UK)
                .Is(ProgRockBands.GentleGiant, ProgRockBands.Genesis, ProgRockBands.IQ);
            ProgRockBands.Values.Where(v => v.Country == Country.Japan).Is(ProgRockBands.YoninBayashi);
        }

        /// <summary>FromKeyTest</summary>
        [TestMethod]
        public void FromKeyTest()
        {
            ProgRockBands.FromKey(1).Is(ProgRockBands.MoonSafari);
            ProgRockBands.FromKey(8).Is(ProgRockBands.TheFlowerKings);

            // no match value => default
            ProgRockBands.FromKey(9).IsNull();
            ProgRockBands.FromKey(10, ProgRockBands.YoninBayashi).Is(ProgRockBands.YoninBayashi);
        }

        /// <summary>ProgRockBands for TypeSafeEnumBase Demo</summary>
        /// <seealso cref="TNakUtility.Enums.TypeSafeEnumBase{TNakUtilityTest.Enums.TypeSafeEnumTest.ProgRockBands, System.Int32}" />
        private class ProgRockBands : TypeSafeEnumBase<ProgRockBands, int>
        {
            public static readonly ProgRockBands MoonSafari = new ProgRockBands(1, "Moon Safari", Country.Sweden);
            public static readonly ProgRockBands GentleGiant = new ProgRockBands(2, "Gentle Giant", Country.UK);
            public static readonly ProgRockBands Genesis = new ProgRockBands(3, "Genesis", Country.UK);
            public static readonly ProgRockBands CameliasGarden = new ProgRockBands(4, "Camelias Garden", Country.Italy);
            public static readonly ProgRockBands IQ = new ProgRockBands(5, "IQ", Country.UK);
            public static readonly ProgRockBands PFM = new ProgRockBands(6, "PFM", Country.Italy);
            public static readonly ProgRockBands YoninBayashi = new ProgRockBands(7, "四人囃子(Yonin Bayashi)", Country.Japan);
            public static readonly ProgRockBands TheFlowerKings = new ProgRockBands(8, "The Flower Kings", Country.Sweden);

            /// <summary>Initializes a new instance of the <see cref="ProgRockBands"/> class.</summary>
            /// <param name="key">The key.</param>
            /// <param name="name">The name.</param>
            private ProgRockBands(int key, string name, Country country) 
                : base(key, name)
            {
                Country = country;
            }

            /// <summary>Country</summary>
            public Country Country { get; }
        }

        private enum Country
        {
			UK,
			Italy,
			Sweden,
			Japan
        }
    }
}