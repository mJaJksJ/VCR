using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Котракт вложения
    /// </summary>
    [Serializable]
    public class AttachmentContract
    {
        /// <summary>
        /// Id вложения
        /// </summary>
        [JsonProperty("Id")]
        [XmlElement("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [JsonProperty("Name")]
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [JsonProperty("Blob")]
        [XmlAttribute("Blob")]
        public byte[] Blob { get; set; }
    }
}
