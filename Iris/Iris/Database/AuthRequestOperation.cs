using Microsoft.EntityFrameworkCore;

namespace Iris.Database
{
    /// <summary>
    /// Запрос авторизации
    /// </summary>
    public class AuthRequestOperation
    {
        /// <summary>
        ///  Id запроса
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Дата и время создания запроса
        /// </summary>
        public DateTime IssuedDateTime { get; set; }

        /// <summary>
        /// Установить параметры таблицы
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void Setup(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<AuthRequestOperation>();

            entity.ToTable("auth_requests");

            entity.HasKey(x => x.Id);
            entity.HasIndex(ar => ar.Id);
            entity.HasIndex(ar => ar.IssuedDateTime);
        }
    }
}
