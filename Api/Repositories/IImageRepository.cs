using System.Runtime.InteropServices;
using Api.Models.Domains;
using Microsoft.AspNetCore.Identity;

namespace Api.Repositories;
public interface IImageRepository
{
    Task<Image> Upload(Image image);
}