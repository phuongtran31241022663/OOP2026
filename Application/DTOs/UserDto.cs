using System;

namespace Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
    }
}