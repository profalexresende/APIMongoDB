using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace APIMongoDB.Model
{
    public class Aluno
    {
        [BsonId] // Indica que este campo será o _id do documento
        [BsonRepresentation(BsonType.ObjectId)] // Trata o ObjectId como string no C#
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; } = null!;

        public int Idade { get; set; }

        public string Curso { get; set; } = null!;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
