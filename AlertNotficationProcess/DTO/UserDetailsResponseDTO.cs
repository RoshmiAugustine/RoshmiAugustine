using System;
using System.Collections.Generic;
using System.Text;

namespace AlertNotificationProcess.DTO
{
    public class UserDetailsResponseDTO
    {
        public string version { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public UserResponseDTO result { get; set; }
    }

    public class UserResponseDTO
    {
        public string ResponseStatus { get; set; }

        public int ResponseStatusCode { get; set; }

        public UserDetailsDTO UserDetails { get; set; }
    }

    public class UserDetailsDTO
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public int HelperID { get; set; }
        public string HelperEmail { get; set; }
        public string HelperExternalID { get; set; }
    }
}
