using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Auto.Website.Models
{
    public class OwnerDto
    {
        public OwnerDto(string Firstname, string Midname,string Lastname, string Email, string OwnersVehicle = null)
        {
            this.Firstname = Firstname;
            this.Midname = Midname;
            this.Lastname = Lastname;
            this.Email = Email;
            this.OwnersVehicle = OwnersVehicle;
        }
        public string Firstname { get; set; }
        public string Midname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string? OwnersVehicle { get; set; }
    }
}

