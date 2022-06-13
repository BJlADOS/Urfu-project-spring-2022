using System;
using Workshop.Core.Domain.Model.User;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Dtos.Admin.ApiKey
{
    public class ApiKeyDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string KeyString { get; set; }
        public long EventId { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }
    }
}