using ServiceReference1;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServiceReference1;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;


namespace ServiceReference1
{
    [TestClass]
    public class UnitTest1
    {
        private ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoServiceSoapType;

        [TestInitialize]
        public async Task Initialize()
        {
            countryInfoServiceSoapType = new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void Validate_CountryNameAndIsoCode()
        {
            var country = ListOfCountryNamesByCode();
            var randomCountry = GetRandomRecord(country);

            var fullCountryInfo = countryInfoServiceSoapType.FullCountryInfo(randomCountry.sISOCode);

            Assert.AreEqual(fullCountryInfo.sISOCode, randomCountry.sISOCode);
            Assert.AreEqual(fullCountryInfo.sName, randomCountry.sName);



        }

        [TestMethod]
        public void Validate_CountryISOCode()
        {
            var country = ListOfCountryNamesByCode();
            List<tCountryCodeAndName> countryRecords = new List<tCountryCodeAndName>();

            for (int record = 0; record < 5; record++)
            {
                countryRecords.Add(GetRandomRecord(country));
            }

            foreach (var countryRecord in countryRecords)
            {
                var isoCode = countryInfoServiceSoapType.CountryISOCode(countryRecord.sName);
                Assert.AreEqual(isoCode, countryRecord.sISOCode);
            }



        }

        private tCountryCodeAndName GetRandomRecord(tCountryCodeAndName[] data)
        {
            var random = new Random();
            int next = random.Next(data.Length);

            var country = data[next];

            return country;

        }

        private List<tCountryCodeAndName> ListOfCountryNamesByCode()
        {
            var country = countryInfoServiceSoapType.ListOfCountryNamesByCode();

            return country;
        }


    }
}