using System.Xml.Serialization;

namespace ProjetEchecDAL.Enum
{
    public enum Genre
    {
        [XmlEnum("0")]Homme,
        [XmlEnum("1")] Femme,
        [XmlEnum("2")] Autre
    }
}
