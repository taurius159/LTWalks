using System.Runtime.InteropServices;
using Api.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace Api.Repositories;
public interface ITokenRepository
{
    string CreateJWTToken(IdentityUser identityUser, List<string> roles);
}