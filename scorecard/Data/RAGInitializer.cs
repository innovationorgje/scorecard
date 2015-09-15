using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using scorecard.Models;

namespace scorecard.Data
{
    public class RAGInitializer : System.Data.Entity.DropCreateDatabaseAlways<RAGContext>
    {
        protected override void Seed(RAGContext context)
        {
            var groups = new List<Group>
            {
                new Group{Title="Group 1"},
                new Group{Title="Group 2"},
                new Group{Title="Group 3"}
            };

            groups.ForEach(g => context.Groups.Add(g));
            context.SaveChanges();

            var criteria = new List<Criteria>
            {
                new Criteria{Reference="1",Title="Criteria 1", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red, Group=groups[0]},
                new Criteria{Reference="2",Title="Criteria 2", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber, Group=groups[0]},
                new Criteria{Reference="3",Title="Criteria 3", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green, Group=groups[0]},
                new Criteria{Reference="4",Title="Criteria 4", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red, Group=groups[1]},
                new Criteria{Reference="5",Title="Criteria 5", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber, Group=groups[1]},
                new Criteria{Reference="6",Title="Criteria 6", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green, Group=groups[1]},
                new Criteria{Reference="7",Title="Criteria 7", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Red, Group=groups[2]},
                new Criteria{Reference="8",Title="Criteria 8", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Amber, Group=groups[2]},
                new Criteria{Reference="9",Title="Criteria 9", Summary="ABCDEFGHIJKLMNOPQRSTUVWXYZ", State=CriteriaState.Green, Group=groups[2]}
            };

            criteria.ForEach(c => context.Criteria.Add(c));
            context.SaveChanges();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Administrator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
            }

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Password@123");
                    
            var users = new List<ApplicationUser>
            {
                new ApplicationUser{FullName="John Smith",UserName="john@smith.com",PasswordHash=password,PhoneNumber="777888"}
            };

            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            var statuses = new List<StatusUpdate>
            {
                new StatusUpdate{Text="Test Update 1",StateFrom=CriteriaState.Red,StateTo=CriteriaState.Red,Stamp=DateTime.Now,Criteria=criteria[0],User=users[0]},
                new StatusUpdate{Text="",StateFrom=CriteriaState.Red,StateTo=CriteriaState.Amber,Stamp=DateTime.Now,Criteria=criteria[0],User=users[0]},
                new StatusUpdate{Text="Test Update 2",StateFrom=CriteriaState.Amber,StateTo=CriteriaState.Amber,Stamp=DateTime.Now,Criteria=criteria[0],User=users[0]},
                new StatusUpdate{Text="",StateFrom=CriteriaState.Amber,StateTo=CriteriaState.Red,Stamp=DateTime.Now,Criteria=criteria[0],User=users[0]}
            };

            statuses.ForEach(s => context.StatusUpdates.Add(s));
            context.SaveChanges();
        }
    }
}