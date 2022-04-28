using Iris.Enums;
using Iris.Services.LettersService.Contracts;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Iris.Services.FormatLettersService
{
    /// <inheritdoc cref="IFormatLettersSevice"/>
    public class FormatLettersSevice : IFormatLettersSevice
    {
        /// <inheritdoc/>
        public ResponseFormat GetFormat(string format)
        {
            return format.ToLower() switch
            {
                "xml" => ResponseFormat.XML,
                "json" => ResponseFormat.Json,
                _ => throw new Exception()
            };
        }

        /// <inheritdoc/>
        public string FormatLetter(LetterContract letterContract, ResponseFormat responseFormat)
        {
            var formatted = "";
            switch (responseFormat)
            {
                case ResponseFormat.XML:
                    formatted = FormatToXml(letterContract);
                    break;

                case ResponseFormat.Json:
                    formatted = FormatToJson(letterContract);
                    break;
            }

            return formatted;
        }

        /// <inheritdoc/>
        public string FormatLetters(IEnumerable<LetterContract> letterContracts, ResponseFormat responseFormat)
        {
            var formatted = "";
            switch (responseFormat)
            {
                case ResponseFormat.XML:
                    formatted = FormatToXml(letterContracts);
                    break;

                case ResponseFormat.Json:
                    formatted = FormatToJson(letterContracts);
                    break;
            }

            return formatted;
        }

        private static string FormatToJson(LetterContract letterContract)
        {
            var jObject = JObject.FromObject(letterContract);
            return jObject.ToString();
        }

        private static string FormatToXml(LetterContract letterContract)
        {
            var xmlSerializer = new XmlSerializer(typeof(LetterContract));
            var xDoc = new XDocument();
            using (var writer = xDoc.CreateWriter())
            {
                xmlSerializer.Serialize(writer, letterContract);
            }

            return xDoc.ToString();
        }

        private static string FormatToJson(IEnumerable<LetterContract> letterContracts)
        {
            var jObject = new JObject(new JProperty("letters", JArray.FromObject(letterContracts)));

            return jObject.ToString();
        }

        private static string FormatToXml(IEnumerable<LetterContract> letterContracts)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<LetterContract>));
            var xDoc = new XDocument();
            using (var writer = xDoc.CreateWriter())
            {
                xmlSerializer.Serialize(writer, letterContracts);
            }

            return xDoc.ToString();
        }
    }
}
