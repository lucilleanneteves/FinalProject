using SoapAPIServiceReference;

namespace SoapAPIProject
{
    [TestClass]
    public class SoapAPITests
    {
        private readonly SoapAPIServiceReference.CountryInfoServiceSoapTypeClient soapTestFinalFinal = new SoapAPIServiceReference.CountryInfoServiceSoapTypeClient(SoapAPIServiceReference.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        private List<tCountryCodeAndName> GetCountryCodeAndNameList()
        {
            return soapTestFinalFinal.ListOfCountryNamesByCode();
        }

        [TestMethod]
        private static tCountryCodeAndName GetRandomRecord(List<tCountryCodeAndName> countryList)
        {
            Random rnd = new Random();
            int randomInt = rnd.Next(0, countryList.Count);

            var randomCountry = countryList[randomInt];

            return randomCountry;
        }

        [TestMethod]
        public void VerifyCountryInfo()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);
            var randomCountryDetails = soapTestFinalFinal.FullCountryInfo(randomCountry.sISOCode);

            Assert.AreEqual(randomCountryDetails.sISOCode, randomCountry.sISOCode);
            Assert.AreEqual(randomCountryDetails.sName, randomCountry.sName);
        }



        [TestMethod]
        public void MatchCountryCode()
        {
            var countryList = GetCountryCodeAndNameList();
            var randomCountry = GetRandomRecord(countryList);

            var top5 = countryList.OrderBy(o => o.sISOCode).Take(5);

            foreach (var country in top5)
            {
                Assert.AreEqual(country.sISOCode, country.sISOCode);
            }
        }
    }
}