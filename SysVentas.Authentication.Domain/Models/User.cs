﻿namespace SysVentas.Authentication.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Token { get; set; }
    public string Nombre { get; set; }
    public void SetToken(string token)
    {
        Token = token;
    }
}
