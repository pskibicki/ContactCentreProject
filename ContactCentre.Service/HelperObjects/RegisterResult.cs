using System.Collections.Generic;
using ContactCentre.Models;

namespace ContactCentre.Service.HelperObjects
{
    public class RegisterResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Interaction> Interactions { get; set; }
        
        public static RegisterResult Fail => new();
        public static RegisterResult FailMessage(string message) => new() { Success = false, Message = message };
        public static RegisterResult Ok(List<Interaction> interactions) => new() { Success = true, Interactions = interactions};
    }
}