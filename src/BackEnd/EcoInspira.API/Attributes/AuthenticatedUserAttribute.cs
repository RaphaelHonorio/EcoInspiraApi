using EcoInspira.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EcoInspira.API.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
     public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter)) 
    {

    }
}