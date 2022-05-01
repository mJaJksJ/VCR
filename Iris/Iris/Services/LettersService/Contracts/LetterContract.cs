using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Iris.Services.LettersService.Contracts
{
    /// <summary>
    /// Котракт письма
    /// </summary>
    [XmlRoot("Letter")]
    [Serializable]
    public class LetterContract
    {
        /// <summary>
        /// Id письма
        /// </summary>
        [JsonProperty("Id")]
        [XmlElement("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        [JsonProperty("Sender")]
        [XmlElement("Sender")]
        public PersonContract Sender { get; set; }

        /// <summary>
        /// Получатели
        /// </summary>
        [JsonProperty("Receivers")]
        [XmlElement("Receiver")]
        public List<PersonContract> Receivers { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        [JsonProperty("Subject")]
        [XmlAttribute("Subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Дата получения/отправки
        /// </summary>
        [JsonProperty("Date")]
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [JsonProperty("Text")]
        [XmlElement("Text")]
        public string Text { get; set; }

        /// <summary>
        /// Вложения
        /// </summary>
        [JsonProperty("Attachmetns")]
        [XmlElement("Attachmetns")]
        public List<AttachmentContract> Attachments { get; set; }

        /// <summary>
        /// Id учетной записи
        /// </summary>
        [JsonProperty("Account")]
        [XmlElement("Account")]
        public int AccountId { get; set; }
    }
}
