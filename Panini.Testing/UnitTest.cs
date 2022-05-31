using Xunit;
using System;
using System.IO;
using Xunit.Priority;

namespace Panini.Testing
{
    public class UnitTest1
    {

        string content = @"
                [DocCustomer]
                IsEnabled = 0

                [DocSupplier]
                IsEnabled = 0

                [OrdSupplier]
                IsEnabled = 1
                EnabledDepots = 0003,0006,0019
                InternalTypes = 6

                [OrdCustomer]
                IsEnabled = 0
                DefaultUM = 0
                UMSettingsLock = 1
                AutoShopCodes = 3:F
                EnabledTypes = 3,4,5
        ";

        [Fact]
        public void TestReadIniFromStream()
        {
            // Deletes file if it exists

            IniFile f = new IniFile();

            using StringReader sr = new(content);

            f.Parse(sr);

            Assert.Equal("0003,0006,0019", f.GetSectionByName("OrdSupplier").TryGet("EnabledDepots"));
            Assert.Equal("0", f.GetSectionByName("OrdCustomer").TryGet("DefaultUM"));

            f.GetSectionByName("DocCustomer").Set("IsEnabled", "1");
            f.RemoveSectionByName("DocSupplier").RemoveSectionByName("OrdSupplier").RemoveSectionByName("OrdCustomer");

            using StringWriter sw = new StringWriter();

            f.Save(sw);

            var newIni = sw.ToString();

            using StringReader sr2 = new(newIni);
            var f2 = new IniFile();
            f2.Parse(sr2);

            Assert.Equal("1", f2.GetSectionByName("DocCustomer").TryGet("IsEnabled"));
            Assert.Null(f2.GetSectionByName("OrdCustomer"));

        }

        [Fact]
        public void TestReadIniFromFileStream()
        {
            // Deletes file if it exists

            IniFile f = new IniFile();

            using StreamReader sr = new("./test.ini");

            f.Parse(sr);

            Assert.Equal("Value1", f.GetSectionByName("Test").TryGet("Key1"));

            f.GetSectionByName("Test2").Set("Key3", "AnotherValue3");

            using StringWriter sw = new StringWriter();

            f.Save(sw);

            var newIni = sw.ToString();

            using StringReader sr2 = new(newIni);
            var f2 = new IniFile();
            f2.Parse(sr2);

            Assert.Equal("AnotherValue3", f2.GetSectionByName("Test2").TryGet("Key3"));
            Assert.Null(f2.GetSectionByName("NoSection"));

        }

    }
}
