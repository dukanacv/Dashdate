using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        public string Description { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}