﻿using EninasHotel.Application.Common.Interfaces;
using EninasHotel.Application.Common.Utility;
using EninasHotel.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EninasHotel.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin@sa.com",
                        Email = "admin@sa.com",
                        Name = "Bhrugen Patel",
                        NormalizedUserName = "ADMIN@sa.COM",
                        NormalizedEmail = "ADMIN@sa.COM",
                        PhoneNumber = "1112223333",
                    }, "Admin123*").GetAwaiter().GetResult();

                    ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@sa.com");
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }

                if (!_roleManager.RoleExistsAsync(SD.Role_User).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).Wait();
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "user@sa.com",
                        Email = "user@sa.com",
                        Name = "User",
                        NormalizedUserName = "USER@sa.COM",
                        NormalizedEmail = "USER@sa.COM",
                        PhoneNumber = "1112223333",
                    }, "User123*").GetAwaiter().GetResult();

                    ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "user@sa.com");
                    _userManager.AddToRoleAsync(user, SD.Role_User).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}