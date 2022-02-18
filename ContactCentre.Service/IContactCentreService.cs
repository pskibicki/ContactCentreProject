using System.Collections.Generic;
using System.Threading.Tasks;
using ContactCentre.Models;
using ContactCentre.Service.HelperObjects;

namespace ContactCentre.Service
{
    public  interface IContactCentreService
    {
        Task<RegisterResult> RegisterInteractionsAsync(List<Interaction> interactions);
    }
}